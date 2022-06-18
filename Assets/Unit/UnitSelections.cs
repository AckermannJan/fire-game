using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }
    
    private void Awake() {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void SelectByName(string unitName) {
        GameObject temp = unitList.Where(unit => unit.name == unitName).SingleOrDefault();
        // DeselectAll();
        unitsSelected.Add(temp);
        GameEvents.current.TriggerUnitSelected(temp);
    }

    public void ClickSelect(GameObject unitToSelect) {
        DeselectAll();
        unitsSelected.Add(unitToSelect);
        GameEvents.current.TriggerUnitSelected(unitToSelect);
    }

    public void ShiftSelect(GameObject unitToSelect) {
        if(!unitsSelected.Contains(unitToSelect)) {
            unitsSelected.Add(unitToSelect);
            GameEvents.current.TriggerUnitSelected(unitToSelect);
        } else {
            unitsSelected.Remove(unitToSelect);
            GameEvents.current.TriggerUnitDeselected(unitToSelect);
        }
    }

    public void DragSelect(GameObject unitToSelect) {
        if(!unitsSelected.Contains(unitToSelect)) {
            unitsSelected.Add(unitToSelect);
            GameEvents.current.TriggerUnitSelected(unitToSelect);
        }
    }

    public void DeselectAll() {
        List<GameObject> tmpUnitsSelected = new List<GameObject>(unitsSelected);
        unitsSelected.Clear();
        foreach(var unit in tmpUnitsSelected) {
            GameEvents.current.TriggerUnitDeselected(unit);
        }
        tmpUnitsSelected.Clear();
    }

    public void Deselect(GameObject unitToDeselect) {
        unitsSelected.Remove(unitToDeselect);    
        GameEvents.current.TriggerUnitDeselected(unitToDeselect);
    }
}
