using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PickUp : MonoBehaviour
{
    [SerializeField] float pickupRange = 1.0f;
    [SerializeField] Vector2 pickupOffset = new Vector2(0, 2);

    CapsuleCollider2D capsuleCollider;
    GameObject pickedUpObject = null;

    public bool hasPickupable() { return pickedUpObject != null; }
    public void resetPickupable() { pickedUpObject = null; }
    public GameObject getPickupable() { return pickedUpObject; }

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !hasPickupable())
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
                    }
                }
            }

        }
    }
}
