using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickUp))]
[RequireComponent(typeof(InputID))]
public class Throw : MonoBehaviour
{
    [SerializeField] Vector2 maxThrowImpulse = new Vector2(1000, 0);
    [SerializeField] Vector2 minThrowImpulse = new Vector2(100, 0);
    [SerializeField] float maxHoldTime = 1.0f;
    [SerializeField] float throwAfterPickupTime = 0.1f;

    InputID inputID = null;
    PickUp pickup = null;
    float holdButtonTime = -1.0f;
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
        Charge(throwButton);
        ChargeThrow(throwButton);
    }

    private void ChargeThrow(string throwButton)
    {
        if (Input.GetButtonUp(throwButton) 
            && holdButtonTime >= 0.0f)
        {
            var pickupable = pickup.GetPickupable();
            pickupable.transform.parent = null;
            var rgBody = pickupable.GetComponent<Rigidbody2D>();
            rgBody.isKinematic = false;
            var fraction = holdButtonTime > maxHoldTime ? 1.0f : holdButtonTime / maxHoldTime;
            holdButtonTime = -1.0f;
            var impulse = Vector2.Lerp(minThrowImpulse, maxThrowImpulse, fraction);
            impulse *= transform.localScale.x;
            rgBody.AddForce(impulse, ForceMode2D.Impulse);
            pickup.ResetPickupable();
        }
    }

    private void Charge(string throwButton)
    {
        if (Input.GetButtonDown(throwButton) 
            && pickup.HasPickupable()
            && Time.time - pickup.GetPickupTime() >= throwAfterPickupTime)
        {
            holdButtonTime = 0.0f;
        }

        if(Input.GetButton(throwButton) &&  holdButtonTime >= 0.0f)
        {
            holdButtonTime += Time.deltaTime;
        }
    }
}
