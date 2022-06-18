using UnityEngine;
using UnityEngine.AI;

public class MovePositionPathfinding : MonoBehaviour, IMovePosition {

    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Vector3 movePosition;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetMovePosition(Vector3 movePosition) {
        Debug.Log(movePosition);
        this.movePosition = movePosition;
    }

    private void Update() {
        navMeshAgent.destination = movePosition;
    }
}