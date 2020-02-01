using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private KeyCode StartKey = KeyCode.Space;

    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(StartKey))
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            //gameObject.
        }
    }
}
