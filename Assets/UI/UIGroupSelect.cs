using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroupSelect : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        GameEvents.current.onTriggerUnitSelected += OnTriggerUnitSelected;
        GameEvents.current.onTriggerUnitDeselected += OnTriggerUnitDeselected;
    }

    private void OnTriggerUnitSelected (GameObject gameObject) {
        List<GameObject> selectedUnits = UnitSelections.Instance.unitsSelected;
        Debug.Log(selectedUnits.Count);
        if(selectedUnits.Count > 1) {
            foreach (Transform child in transform)
                child.gameObject.SetActive(true);
        } else {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);
        }
    }

    private void OnTriggerUnitDeselected (GameObject gameObject) {
        List<GameObject> selectedUnits = UnitSelections.Instance.unitsSelected;
        Debug.Log(selectedUnits.Count);
        if(selectedUnits.Count < 2) {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        GameEvents.current.onTriggerUnitSelected -= OnTriggerUnitSelected;
        GameEvents.current.onTriggerUnitDeselected -= OnTriggerUnitDeselected;
    }
}
