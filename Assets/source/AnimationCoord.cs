using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCoord : MonoBehaviour
{
    PickUp pickUp = null;
    Rigidbody2D rgBody = null;
    Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        var parent = transform.parent.gameObject;
        pickUp = parent.GetComponent<PickUp>();
        rgBody = parent.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("hasBox", pickUp.HasPickupable());
        animator.SetFloat("Speed", rgBody.velocity.x);
    }
}
