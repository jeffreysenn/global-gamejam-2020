using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour
{
    [SerializeField] private HingeJoint2D Hinge = null;

    [SerializeField] private Vector3 BoxOffset = new Vector2(0, -2);
    [SerializeField] private List<Transform> Waypoints = new List<Transform>();

    [SerializeField] private float FlightSpeed = 5.0f;

    [SerializeField] private SpriteRenderer SR = null;
    
    private List<Transform> CurrentFlight = new List<Transform>();
    private List<Transform> NextFlight = new List<Transform>();

    [HideInInspector]
    public bool IsActive = false;

    void Start()
    {
        CurrentFlight = Waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsActive)
        {
            FlyToNextWaypoint();
        }
    }

    public void AttachNewBox(Rigidbody2D pAttachmentObject)
    {
        Hinge.enabled = true;
        pAttachmentObject.transform.position = transform.position + BoxOffset;
        Hinge.connectedBody = pAttachmentObject;

        IsActive = true;
    }

    private void FlyToNextWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, CurrentFlight[0].transform.position, FlightSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, CurrentFlight[0].position) < 0.5f)
        {
            NextFlight.Add(CurrentFlight[0]);
            CurrentFlight.RemoveAt(0);
            if (CurrentFlight.Count < 2)
            {
                DropCargo();
            }
        }
        if (CurrentFlight.Count <= 0)
        {
            NextFlight.Reverse();
            for(int i = 0; i < 3; i++)
            {
                CurrentFlight.Add(NextFlight[i]);
            }
            NextFlight.Clear();
            if (SR.flipX == true)
            {
                SR.flipX = false;
            }
            else
            {
                SR.flipX = true;
            }
            IsActive = false;
        }
    }

    private void DropCargo()
    {
        Hinge.connectedBody = null;
        Hinge.enabled = false;
    }
}
