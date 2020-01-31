using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    [SerializeField] private float HoleSpawnDelay = 5.0f;

    [SerializeField] private GameObject HolePrefab = null;

    private BoxCollider2D Collider = null;

    private List<GameObject> Holes = new List<GameObject>();

    private float HoleSpawnTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HoleSpawnTimer < 0)
        {
            SpawnHole();
        }
        HoleSpawnTimer -= Time.deltaTime;
    }

    private void SpawnHole()
    {
        float randomX = Random.Range(Collider.bounds.min.x, Collider.bounds.max.x);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y);
        
        GameObject newHole = Instantiate(HolePrefab, spawnPos, transform.rotation/*, transform*/);
        Holes.Add(newHole);

        HoleSpawnTimer = HoleSpawnDelay;
    }
}
