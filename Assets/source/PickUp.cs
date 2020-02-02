using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(InputID))]
public class PickUp : MonoBehaviour
{
    [SerializeField] float pickupRange = 1.0f;
    [SerializeField] Vector2 pickupOffset = new Vector2(0, 2);
    [SerializeField] float releasePlayerFreezeTime= 0.3f;

    public UnityEvent pickupEvent { get; } = new UnityEvent();

    CapsuleCollider2D capsuleCollider = null;
    InputID inputID = null;
    GameObject pickedUpObject = null;
    float pickupTime = 0.0f;

    public bool HasPickupable() { return pickedUpObject != null; }
    public void ResetPickupable() { 
        if(pickedUpObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(EnablePlayerComponents(pickedUpObject));
        }
        pickedUpObject = null; 
    }

    IEnumerator EnablePlayerComponents(GameObject player)
    {
        yield return new WaitForSeconds(releasePlayerFreezeTime);
        player.GetComponent<MovementComponent>().enabled = true;
        player.GetComponent<PickUp>().enabled = true;
        yield return null;
    }

    public GameObject GetPickupable() { return pickedUpObject; }
    public float GetPickupTime() { return pickupTime; }

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        inputID = GetComponent<InputID>();
    }

    // Update is called once per frame
    void Update()
    {
        var pickUp = inputID.GetActionName(InputID.Action.FIRE);
        if (Input.GetButtonDown(pickUp) && !HasPickupable())
        {
            Collider2D[] colliders = GetOverlappingColliders();
            var pickupableList = GetPickupable(colliders);

            if(pickupableList.Count > 0)
            {
                GameObject otherPlayer = null;
                foreach (var pickable in pickupableList)
                {
                    if(pickable.layer == LayerMask.NameToLayer("Player"))
                    {
                        otherPlayer = pickable;
                        otherPlayer.GetComponent<MovementComponent>().enabled = false;
                        otherPlayer.GetComponent<PickUp>().enabled = false;
                        break;
                    }
                }

                if(otherPlayer != null) { PickupObject(otherPlayer); }
                else { PickupObject(pickupableList[0]); }
            }
        }
    }

    Collider2D[] GetOverlappingColliders()
    {
        var boxWidth = capsuleCollider.bounds.size.x / 2 + pickupRange;
        var boxSize = new Vector2(boxWidth, capsuleCollider.bounds.size.y);
        var boxCenter = capsuleCollider.bounds.center;
        boxCenter.x = capsuleCollider.bounds.center.x + transform.localScale.x * boxWidth / 2;
        return Physics2D.OverlapBoxAll(
            boxCenter,
            boxSize,
            0);
    }

    List<GameObject> GetPickupable(Collider2D[] colliders)
    {
        var list = new List<GameObject>();
        foreach (var collider in colliders)
        {
            var otherObject = collider.transform.gameObject;
            var areSameObject = GameObject.ReferenceEquals(otherObject, gameObject);
            if (!areSameObject)
            {
                if (otherObject.GetComponent<Pickable>() != null)
                {
                    list.Add(otherObject);
                }
            }
        }
        return list;
    }

    void PickupObject(GameObject obje)
    {
        var rgBody = obje.GetComponent<Rigidbody2D>();
        rgBody.isKinematic = true;
        rgBody.velocity = Vector2.zero;
        obje.transform.parent = gameObject.transform;
        obje.transform.localRotation = Quaternion.identity;
        obje.transform.localPosition = pickupOffset;
        pickedUpObject = obje;
        pickupTime = Time.time;
    }
}
