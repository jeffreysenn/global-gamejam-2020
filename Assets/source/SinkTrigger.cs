using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider2D Collider = null;

    void Start()
    {
        if(Collider == null)
        {
            GetComponent<BoxCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
