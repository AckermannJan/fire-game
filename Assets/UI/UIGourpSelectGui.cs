using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGourpSelectGui : MonoBehaviour
{
    public GameObject gridElement;

    public List<GameObject> visibleUnits = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        gameObject.transform.parent.gameObject.SetActive(false);
        GameEvents.current.onTriggerUnitSelected += OnTriggerUnitSelected;
        GameEvents.current.onTriggerUnitDeselected += OnTriggerUnitDeselected;
    }

    private void OnTriggerUnitSelected (GameObject unit) {
        visibleUnits.Add(unit);
        GameObject fireFighterIcon = Instantiate(gridElement, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;
        fireFighterIcon.transform.name = unit.transform.name;
        fireFighterIcon.transform.SetParent(gameObject.transform, false);
    }

    private void OnTriggerUnitDeselected (GameObject unit) {
        visibleUnits.Remove(unit);
        Destroy(gameObject.transform.Find(unit.transform.name).gameObject);
    }

    private void OnDestroy() {
        GameEvents.current.onTriggerUnitSelected -= OnTriggerUnitSelected;
        GameEvents.current.onTriggerUnitDeselected -= OnTriggerUnitDeselected;
    }
}
