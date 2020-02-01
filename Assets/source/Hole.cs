using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [HideInInspector]
    public bool IsPlugged = false;

    private SpriteRenderer SR = null;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D p_Collider)
    {
        if (p_Collider.CompareTag("Box"))
        {
            IsPlugged = true;
            SR.color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D p_Collider)
    {
        if (p_Collider.CompareTag("Box"))
        {
            IsPlugged = false;
            SR.color = Color.black;
        }
    }
}
