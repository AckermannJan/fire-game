using UnityEngine;

public class FireFighterUnit : MonoBehaviour {
    // Start is called before the first frame update


    void Start() {
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update() {
        if(UnitSelections.Instance.unitsSelected.Contains(this.gameObject)) {
            this.transform.GetChild(1).gameObject.SetActive(true);
        } else {
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
