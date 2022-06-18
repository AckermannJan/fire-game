using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerMovementMouse : MonoBehaviour {

    RecastGraph graph;
    List<Vector3> currentAgentPositions = new List<Vector3>();

    private void Awake() {
        graph = AstarPath.active.data.recastGraph;
    }

    void Update() {
        if(Input.GetMouseButtonDown(1) && UnitSelections.Instance.unitsSelected.Contains(gameObject)) {
            Vector3 movePosition = GetMouseWorldPositionWithZ();
            List<GameObject> selectedUnits = UnitSelections.Instance.unitsSelected;

            for (var i = 0; i < UnitSelections.Instance.unitsSelected.Count; i++) {
                currentAgentPositions.Add(UnitSelections.Instance.unitsSelected[i].transform.position);
            }

            // The final points will be at most this distance from the groupDestination.
            float maxDistance = 2.0f;
            // Should be around the same size as the agent radii, possibly slightly smaller
            float clearanceRadius = 1.5f;

            PathUtilities.GetPointsAroundPointWorld(movePosition, graph, currentAgentPositions, maxDistance, clearanceRadius);

            for (var i = 0; i < UnitSelections.Instance.unitsSelected.Count; i++) {
                UnitSelections.Instance.unitsSelected[i].GetComponent<IMovePosition>().SetMovePosition(currentAgentPositions[i]);
            }
        }
    }

    private static Vector3 GetMouseWorldPositionWithZ() {
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Vector3 worldPosition = new Vector3(0,0,0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out distance)){
            worldPosition = ray.GetPoint(distance);
        }
        return worldPosition;
    }
}
