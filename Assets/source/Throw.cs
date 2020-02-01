using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickUp))]
[RequireComponent(typeof(InputID))]
public class Throw : MonoBehaviour
{
    [SerializeField] Vector2 throwImpulse = new Vector2(500, 0);

    InputID inputID = null;
    PickUp pickup = null;
    // Start is called before the first frame update
    void Start()
    {
        pickup = GetComponent<PickUp>();
        inputID = GetComponent<InputID>();
    }

    // Update is called once per frame
    void Update()
    {
        var throwButton = inputID.GetActionName(InputID.Action.FIRE);
        if (Input.GetButtonDown(throwButton) && pickup.CanThrow()){
            var pickupable = pickup.GetPickupable();
            pickupable.transform.parent = null;
            var rgBody = pickupable.GetComponent<Rigidbody2D>();
            rgBody.isKinematic = false;
            var impulse = transform.localScale.x * throwImpulse;
            rgBody.AddForce(impulse, ForceMode2D.Impulse);
            pickup.ResetPickupable();
        }
    }
}
