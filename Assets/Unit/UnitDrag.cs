using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;
    [SerializeField]
    RectTransform boxVisual;

    Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;
    
    void Start() {
            myCam = Camera.main;
            startPosition = Vector2.zero;    
            endPosition = Vector2.zero;    
            DrawVisual();
    }

    // Update is called once per frame
    void Update() {
        //when clicked
        if(Input.GetMouseButtonDown(0)) {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
        }

        //when dragging#
        if(Input.GetMouseButton(0)) {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        //when release click
        if(Input.GetMouseButtonUp(0)) {
            SelectUnits();
            startPosition = Vector2.zero;    
            endPosition = Vector2.zero;   
            DrawVisual();
        }
    }

    void DrawVisual() {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection() {
        Vector2 boxCenter = (startPosition + endPosition) / 2.0f;
        Vector2 boxSize = new Vector2(Mathf.Abs(startPosition.x - endPosition.x), Mathf.Abs(startPosition.y - endPosition.y));
        Vector2 extents = boxSize / 2.0f;

        selectionBox.min = boxCenter - extents;
        selectionBox.max = boxCenter + extents;
    }

    void SelectUnits() {
        // loop trough all units
        foreach(var unit in UnitSelections.Instance.unitList) {
            // if unit is wihtin the bounds of the selection box
            if(selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position))) {
                // if any unit is within the selection add the to selection
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }

}
