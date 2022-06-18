using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAimer : MonoBehaviour
{

    [SerializeField] float distance;
    [SerializeField] Vector3 _mousePosition;
    [SerializeField] Vector3 _direction;
    [SerializeField] Camera _mainCamera;

    public float RotationSpeed;
    public AnimationCurve velocityMultiplier;
    public float maxDistance;

    void Start() {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        // Get mouse position
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
            _mousePosition = raycastHit.point;
        }

        distance = Vector3.Distance(_mousePosition, transform.position);
        float actualMouseYPosition = _mousePosition.y;
        _mousePosition.y = distance * velocityMultiplier.Evaluate(Mathf.Clamp(distance / maxDistance, .1f, 1f)) + actualMouseYPosition; // add mouse height divided by two
        
        transform.LookAt(_mousePosition);
    }
}
