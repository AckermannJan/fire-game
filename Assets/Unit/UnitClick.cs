using UnityEngine;
using UnityEngine.EventSystems;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    private float doubleClickTime = .2f, lastClickTime;
    EventSystem eventSystem;

    public LayerMask hitLayers;
    
    void Start() {
        myCam = Camera.main;
        eventSystem = FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) && !eventSystem.IsPointerOverGameObject()) {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            Debug.Log("click");
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayers)) {
                Debug.Log("click");
                float timeSinceLastClick = Time.time - lastClickTime;
                if (timeSinceLastClick <= doubleClickTime) {
                    // [DOUBLECLICK] - Select all units on screen
                    foreach(var unit in UnitSelections.Instance.unitList) {
                        // if unit is wihtin the bounds of the screen
                        Vector3 viewPos = myCam.WorldToViewportPoint(unit.transform.GetChild(0).position);
                        if(viewPos.x > 0f && viewPos.y > 0f) {
                            // Select unit
                            UnitSelections.Instance.DragSelect(unit);
                        }
                    }
                    // Todo: Select only same type () Check by Tag
                } else {
                    Debug.Log("testest");
                    // [SINGLECLICK] - Select single unit
                    GameObject parent = hit.collider.gameObject;
                    if (Input.GetKey(KeyCode.LeftShift)) {
                        UnitSelections.Instance.ShiftSelect(parent);
                    } else {
                        UnitSelections.Instance.ClickSelect(parent);
                    }
                }
            } else {
                if(!Input.GetKey(KeyCode.LeftShift)) {
                    UnitSelections.Instance.DeselectAll();
                }
            }

            lastClickTime = Time.time;
        }
    }
}
