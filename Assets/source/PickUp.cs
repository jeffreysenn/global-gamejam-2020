using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(InputID))]
public class PickUp : MonoBehaviour
{
    [SerializeField] float pickupRange = 1.0f;
    [SerializeField] Vector2 pickupOffset = new Vector2(0, 2);
    [SerializeField] float throwCooldown = 0.1f;

    CapsuleCollider2D capsuleCollider = null;
    InputID inputID = null;
    GameObject pickedUpObject = null;
    util.Timer throwTimer = new util.Timer(0.0f);


    public bool HasPickupable() { return pickedUpObject != null; }
    public void ResetPickupable() { pickedUpObject = null; }
    public GameObject GetPickupable() { return pickedUpObject; }

    public bool CanThrow() { return throwTimer.IsTimeUp() && HasPickupable(); }

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        inputID = GetComponent<InputID>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasPickupable())
        {
            throwTimer.Countdown(Time.deltaTime);
        }

        var pickUp = inputID.GetActionName(InputID.Action.FIRE);
        if (Input.GetButtonDown(pickUp) && !HasPickupable())
        {
            var boxWidth = capsuleCollider.bounds.size.x / 2 + pickupRange;
            var boxSize = new Vector2(boxWidth, capsuleCollider.bounds.size.y);
            var boxCenter = capsuleCollider.bounds.center;
            boxCenter.x = capsuleCollider.bounds.center.x + transform.localScale.x * boxWidth / 2;
            Collider2D[] hits = Physics2D.OverlapBoxAll(
                boxCenter,
                boxSize,
                0);
            foreach (var hit in hits)
            {
                if (!HasPickupable())
                {
                    var otherObject = hit.transform.gameObject;
                    var areSameObject = GameObject.ReferenceEquals(otherObject, gameObject);
                    if (!areSameObject)
                    {
                        if (otherObject.GetComponent<Pickable>() != null)
                        {
                            var rgBody = otherObject.GetComponent<Rigidbody2D>();
                            rgBody.isKinematic = true;
                            otherObject.transform.parent = gameObject.transform;
                            otherObject.transform.localPosition = pickupOffset;
                            pickedUpObject = otherObject;
                            throwTimer.SetCountdown(throwCooldown);
                        }
                    }
                }
            }

        }
    }
}
