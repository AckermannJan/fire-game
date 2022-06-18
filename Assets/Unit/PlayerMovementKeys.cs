using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKeys : MonoBehaviour {
    void Update() {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ = +1f;
        if (Input.GetKey(KeyCode.A)) moveZ = -1f;
        if (Input.GetKey(KeyCode.S)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = +1f;

        Vector3 moveVector = new Vector3(moveX, 0, moveZ).normalized;
        GetComponent<IMoveVelocity>().SetVelocity(moveVector);
    }
}
