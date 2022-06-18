using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISelectedUnitGui : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        List<GameObject> selectedUnits = UnitSelections.Instance.unitsSelected;
        textMeshPro.text = selectedUnits[0].name;
    }
}
