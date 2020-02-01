using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] float pickupRange = 1.0f;
    [SerializeField] Vector2 pickupOffset = new Vector2(0, 2);

    CapsuleCollider2D capsuleCollider;
    GameObject pickedUpObject = null;

    bool hasPickupable() { return pickedUpObject != null; }

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
            var raycastDistance = capsuleCollider.bounds.size.y / 2 + pickupRange;
            RaycastHit2D[] hits = Physics2D.RaycastAll(capsuleCollider.bounds.center, Vector2.right, raycastDistance);
            foreach (var hit in hits)
            {
                var otherObject = hit.transform.gameObject;
                var areSameObject = GameObject.ReferenceEquals(otherObject, gameObject);
                if (!areSameObject)
                {
                    if (otherObject.GetComponent<Pickable>() != null)
                    {
                        hit.rigidbody.isKinematic = true;
                        otherObject.transform.parent = gameObject.transform;
                        otherObject.transform.localPosition = pickupOffset;
                        pickedUpObject = otherObject;
                    }
                }
            }

        }
    }
}
