using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    private Transform rig;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        rig = transform.GetChild(0);
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void StartWalking() {
        animator.SetBool("isWalking", true);
    }
}
