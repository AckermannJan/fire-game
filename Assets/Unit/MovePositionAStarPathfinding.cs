using UnityEngine;
using Pathfinding;

public class MovePositionAStarPathfinding : MonoBehaviour, IMovePosition {

    private RichAI richAI;		
    private Transform tr;
    private bool isAtDestination;
    private animationStateController animationStateController;
    private Animator anim;
    private int randomIdleType;

    private void Start() {
        InvokeRepeating("RandomizeIdleType", 0f, 5f);
    }

    protected void Awake() {
        richAI = GetComponent<RichAI>();
        tr = GetComponent<Transform>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }
    
    protected Vector3 lastTarget;

    private void OnTargetReached () {
        if (Vector3.Distance(tr.position, lastTarget) > 1) {
            lastTarget = tr.position;
            Debug.Log("reached");
        }
    }

    private void Update() {
        if (richAI.reachedEndOfPath) {
            if (!isAtDestination) OnTargetReached();
            isAtDestination = true;
        } else isAtDestination = false;

        // Calculate the velocity relative to this transform's orientation
        Vector3 relVelocity = tr.InverseTransformDirection(richAI.velocity);
        relVelocity.y = 0;
        
        anim.SetFloat("Velocity X", relVelocity.x / anim.transform.lossyScale.x);
        anim.SetFloat("Velocity Z", relVelocity.z / anim.transform.lossyScale.z);
        anim.SetFloat("IdleType", (float)randomIdleType, .4f, Time.deltaTime);
    }

    public void SetMovePosition(Vector3 movePosition) {
        richAI.destination = movePosition;
    }

    public void RandomizeIdleType() {
        randomIdleType = Random.Range(1, 2 + 1);
    }
}