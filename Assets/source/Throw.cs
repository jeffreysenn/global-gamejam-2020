using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickUp))]
public class Throw : MonoBehaviour
{
    [SerializeField] Vector2 throwImpulse = new Vector2(500, 0);

    PickUp pickup = null;
    // Start is called before the first frame update
    void Start()
    {
        pickup = GetComponent<PickUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && pickup.hasPickupable()){
            var pickupable = pickup.getPickupable();
            pickupable.transform.parent = null;
            var rgBody = pickupable.GetComponent<Rigidbody2D>();
            rgBody.isKinematic = false;
            var impulse = transform.localScale.x * throwImpulse;
            rgBody.AddForce(impulse, ForceMode2D.Impulse);
            pickup.resetPickupable();
        }
    }
}
