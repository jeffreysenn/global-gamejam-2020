using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject BoxPrefab = null;

    [SerializeField] private float SpawnDelay = 3.0f;

    private float SpawnTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if(SpawnTimer < 0)
        {
            SpawnBox();
        }
    }

    private void SpawnBox()
    {
        Instantiate(BoxPrefab, transform);
        SpawnTimer = SpawnDelay;
    }
}
