using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class MovementComponent : MonoBehaviour
{
    [SerializeField] float jumpImpulse = 5;
    [SerializeField] float maxWalkSpeed = 9;
    [SerializeField] float airControl = 0.5f;
    [SerializeField] float groundCheckOvershoot = .3f;
    [SerializeField] float jumpCooldown = .1f;

    Rigidbody2D rgBody;
    CapsuleCollider2D capsuleCollider;
    bool shouldJump = false;
    util.Timer jumpTimer = new util.Timer(0.0f);

    private void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        jumpTimer.Countdown(Time.deltaTime);
        if (Input.GetButtonDown("Jump") && jumpTimer.IsTimeUp() && IsGrounded())
        {
            shouldJump = true;
            jumpTimer.SetCountdown(jumpCooldown);
        }
    }

    private void FixedUpdate()
    {
        Walk();
        Jump();
    }

    private void Jump()
    {
        if (shouldJump)
        {
            rgBody.AddForce(transform.up * jumpImpulse, ForceMode2D.Impulse);
            shouldJump = false;
        }
    }

    private void Walk()
    {
        var hAxis = Input.GetAxis("Horizontal");
        var walkSpeed = hAxis * maxWalkSpeed;
        if (!IsGrounded()) { walkSpeed *= airControl; }
        rgBody.velocity = new Vector2(walkSpeed, rgBody.velocity.y);
    }

    private bool IsGrounded()
    {
        float raycastDistance = capsuleCollider.bounds.size.y / 2 + groundCheckOvershoot;
        RaycastHit2D[] hits = Physics2D.RaycastAll(capsuleCollider.bounds.center, -Vector2.up, raycastDistance);
        foreach (RaycastHit2D hit in hits)
        {
            if (!hit.transform.tag.Equals("Player"))
                return true;
        }
        return false;
    }

}