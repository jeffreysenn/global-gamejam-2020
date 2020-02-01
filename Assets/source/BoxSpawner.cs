using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject BoxPrefab = null;

    [SerializeField] private float SpawnDelay = 3.0f;
    [SerializeField] private int SpawnMax = 7;

    private float SpawnTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] allBoxes = GameObject.FindGameObjectsWithTag("Box");

        SpawnTimer -= Time.deltaTime;
        if(SpawnTimer < 0
            && allBoxes.Length < SpawnMax)
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
