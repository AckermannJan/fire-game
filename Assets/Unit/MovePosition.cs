using UnityEngine;

public class MovePosition : MonoBehaviour, IMoveVelocity {

    [SerializeField]
    private float moveSpeed;

    private Vector3 velocityVector;
    private Rigidbody rigidbodyUnit;

    private void Awake() {
        rigidbodyUnit = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 velocityVector) {
        this.velocityVector = velocityVector;
    }

    private void FixedUpdate() {
        rigidbodyUnit.MovePosition(transform.position + (velocityVector * moveSpeed * Time.deltaTime));
    }
}