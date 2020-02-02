using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PickUp))]
[RequireComponent(typeof(InputID))]
public class Throw : MonoBehaviour
{
    [SerializeField] Vector2 maxThrowImpulse = new Vector2(1000, 800);
    [SerializeField] Vector2 minThrowImpulse = new Vector2(100, 0);
    [SerializeField] Vector2 minThrowImpulsePlayer = new Vector2(50, 0);
    [SerializeField] Vector2 maxThrowImpulsePlayer = new Vector2(200, 0);
    [SerializeField] float forceDropPlayerTime = 2.0f;
    [SerializeField] Vector2 dropOffset = new Vector2(1, 0);
    [SerializeField] float tossThreshold = 0.2f;
    [SerializeField] float maxHoldTime = 1.0f;
    [SerializeField] float throwAfterPickupTime = 0.1f;

    public UnityEvent dropEvent { get; } = new UnityEvent();
    public UnityEvent tossEvent { get; } = new UnityEvent();

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
        ForceDropPlayer();
    }

    private void ChargeThrow(string throwButton)
    {
        if (Input.GetButtonUp(throwButton) 
            && holdButtonTime >= 0
            && pickup.HasPickupable())
        {
            var pickupable = pickup.GetPickupable();
            if (holdButtonTime >= tossThreshold) { Toss(pickupable); }
            else { Drop(pickupable); }
            holdButtonTime = -1.0f;
        }
    }

    void Charge(string throwButton)
    {
        if (Input.GetButtonDown(throwButton)
            && pickup.HasPickupable()
            && Time.time - pickup.GetPickupTime() >= throwAfterPickupTime)
        {
            holdButtonTime = 0.0f;
        }

        if (Input.GetButton(throwButton) && holdButtonTime >= 0.0f)
        {
            holdButtonTime += Time.deltaTime;
        }
    }

    void Drop(GameObject pickupable)
    {
        pickupable.transform.parent = null;
        var offset = dropOffset;
        offset.x *= transform.localScale.x;
        pickupable.transform.position = ((Vector2)transform.position + offset);
        var rgBody = pickupable.GetComponent<Rigidbody2D>();
        rgBody.isKinematic = false;
        pickup.ResetPickupable();
        dropEvent.Invoke();
    }

    void Toss(GameObject pickupable)
    {
        pickupable.transform.parent = null;
        var fraction = holdButtonTime > maxHoldTime ? 1.0f : holdButtonTime / maxHoldTime;
        var minImpulse = Vector2.zero;
        var maxImpulse = Vector2.zero;
        if (IsPlayer(pickupable))
        {
            minImpulse = minThrowImpulsePlayer;
            maxImpulse = maxThrowImpulsePlayer;
        }
        else
        {
            minImpulse = minThrowImpulse;
            maxImpulse = maxThrowImpulse;
        }
        var impulse = Vector2.Lerp(minImpulse, maxImpulse, fraction);
        impulse.x *= transform.localScale.x;
        var rgBody = pickupable.GetComponent<Rigidbody2D>();
        rgBody.isKinematic = false;
        rgBody.AddForce(impulse, ForceMode2D.Impulse);
        pickup.ResetPickupable();
        tossEvent.Invoke();
    }

    void ForceDropPlayer()
    {
        if (!pickup.HasPickupable()) { return; }
        var pickupable = pickup.GetPickupable();
        if (IsPlayer(pickupable)
            && Time.time - pickup.GetPickupTime() >= forceDropPlayerTime)
        {
            Drop(pickupable);
        }
    }

    bool IsPlayer(GameObject obj)
    {
        return obj.layer == LayerMask.NameToLayer("Player");
    }
}
