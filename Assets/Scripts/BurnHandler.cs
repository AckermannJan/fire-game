using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class BurnHandler : MonoBehaviour
{
    [System.Serializable]
    public class Burn {
        public float radius = 0;
        public Vector3 position = new Vector3(0, 0, 0);
    }

    public float burnTime;
    public bool burning;
    public AnimationCurve chanceToSpread;
    public Burn []burnList;

    // Water variables
    private bool hitByWater;
    private float hitByWaterResetTime = 1f;
    private float elapsedWaterTime;

    private bool burnedDown;
    private float burnTimePassed;
    private float timeInCurve;
    private float chanceToIgnite;
    private float originalBurnTime;
    private int _amountBurningItems;

    private void OnDrawGizmos() {
        foreach (var burnItem in burnList) {
            var newPosition = RotatePointAroundPivot(burnItem.position + transform.position, transform.position, transform.rotation.eulerAngles);
            Gizmos.DrawWireSphere(newPosition, burnItem.radius);
        }
    }

    void Start() {
        // Events
        GameEvents.current.onIgniteGameObject += OnIgniteGameObject;
        GameEvents.current.onTriggerGameObjectHit += onTriggerGameObjectHit;

        InvokeRepeating("tryToIgnite", 0f, 1.0f);
        originalBurnTime = burnTime;
        hitByWater = false;
    }

    void Update() {
        checkIfBurned();
        if(burning) {
            continueBurn();
            updateHealthBar();
            particleHandler();
            checkIfWaterHits();
            AmountBurningItems = getAmountOfBurningItems();
            if (transform.name == "SM_Env_Tree_01 (2)") {
                Debug.Log(AmountBurningItems);
            }
        }


        // TODO-future: increase burn time when water hits
        // TODO-future: kill fire when water hit time is as big as burnTime
    }

    // Same for water hits but reverse
    [System.ComponentModel.DefaultValue(0)]
    public int AmountBurningItems
    {
        get
        {
            return _amountBurningItems;
        }
        set
        {
            if(AmountBurningItems != value) {
                changeBurnTimeByPercent(Mathf.Clamp(value * 3, 0, 100));
                _amountBurningItems = value;
            }
        }
    }



    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    private void updateHealthBar() {
        Transform bar = transform.GetChild(0).transform.Find("Bar");
        bar.localScale = new Vector3(timeInCurve, 1f);
    }

    private void continueBurn() {
        burnTimePassed += Time.deltaTime;
    }

    private void tryToIgnite() {
        if(burning && !hitByWater) {
            float test = Random.Range(0, 100);
            timeInCurve = burnTimePassed / burnTime;
            chanceToIgnite = chanceToSpread.Evaluate(timeInCurve) * 100;
            
            if(test < chanceToIgnite) {
                igniteSurroundings();
            }
        }
    }

    private void checkIfBurned() {
        if(timeInCurve > 1f || timeInCurve < 0f) {
            burning = false;
            burnedDown = true;
            timeInCurve = 0;
            particleHandler();
        }
    }

    private void igniteSurroundings() {
        int layer_mask = LayerMask.GetMask("Burnable") | LayerMask.GetMask("Damageable");
        
        foreach (var burnItem in burnList) {
            var newPosition = RotatePointAroundPivot(burnItem.position + transform.position, transform.position, transform.rotation.eulerAngles);
            Collider[] hitColliders = Physics.OverlapSphere(newPosition, burnItem.radius, layer_mask);
            hitColliders = hitColliders.OrderBy(c => (newPosition - c.transform.position).sqrMagnitude).ToArray();
            bool ignited = false;

            foreach (var hitCollider in hitColliders) {
                if(hitCollider.gameObject != gameObject && !hitCollider.gameObject.GetComponent<BurnHandler>().burning) {
                    ignited = true;
                    GameEvents.current.IgniteObject(hitCollider.gameObject.GetInstanceID());
                    // hitCollider.SendMessage("dealDamage");
                    break;
                }
            }
            if(ignited) {
                break;
            }
        }
    }

    private int getAmountOfBurningItems() {
        List<GameObject> burningItems = new List<GameObject>();
        if(burning) {
            int layer_mask = LayerMask.GetMask("Burnable");
            foreach (var burnItem in burnList) {
                var newPosition = RotatePointAroundPivot(burnItem.position + transform.position, transform.position, transform.rotation.eulerAngles);
                Collider[] hitColliders = Physics.OverlapSphere(newPosition, burnItem.radius, layer_mask);
                hitColliders = hitColliders.OrderBy(c => (newPosition - c.transform.position).sqrMagnitude).ToArray();

                foreach (var hitCollider in hitColliders) {
                    if(
                        hitCollider.gameObject != gameObject &&
                        hitCollider.gameObject.GetComponent<BurnHandler>().burning &&
                        !burningItems.Contains(hitCollider.gameObject)
                    ) {
                        burningItems.Add(hitCollider.gameObject);
                    }
                }
            }
            return burningItems.Count();
        }
        return burningItems.Count();
    }

    public void changeBurnTimeByPercent(float percent) {
        burnTime = originalBurnTime + (originalBurnTime * percent / 100);
    }

    private void OnIgniteGameObject(int id) {
        if(burnTimePassed < burnTime && id == transform.gameObject.GetInstanceID()) {
            burning = true;
        }
    }

    private void onTriggerGameObjectHit(GameObject gameObject) {
        if(gameObject.transform == transform) {
            Debug.Log("Hit");
            burnTimePassed -= 0.01f;
            timeInCurve = burnTimePassed / burnTime;
            elapsedWaterTime = 0;
            hitByWater = true;
        }
    }

    private void checkIfWaterHits() {
        elapsedWaterTime += Time.deltaTime;
        if(elapsedWaterTime > hitByWaterResetTime) {
            Debug.Log("Out");
            hitByWater = false;
        }
    }

    private void particleHandler() {
        GameObject fires = transform.GetChild(1).gameObject;
        foreach (Transform fire in fires.transform) {
            fire.gameObject.GetComponent<FireParticle>().progress = timeInCurve;
        }
    }

    private void OnDestroy() {
        GameEvents.current.onIgniteGameObject -= OnIgniteGameObject;
        GameEvents.current.onTriggerGameObjectHit -= onTriggerGameObjectHit;
    }
}
