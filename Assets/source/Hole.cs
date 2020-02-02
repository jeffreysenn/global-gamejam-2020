using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [HideInInspector]
    public bool IsPlugged = false;


    [SerializeField] private MeshRenderer WaterSpout = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D pCollider)
    {
        if (pCollider.CompareTag("Box"))
        {
            IsPlugged = true;
            WaterSpout.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D pCollider)
    {
        if (pCollider.CompareTag("Box"))
        {
            IsPlugged = false;
            WaterSpout.enabled = false;
        }
    }
}
