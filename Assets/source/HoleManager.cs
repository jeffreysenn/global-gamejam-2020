using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    [SerializeField] private float HoleSpawnDelay = 5.0f;

    [SerializeField] private Hole HolePrefab = null;
    [SerializeField] private Transform UnActivatedHoles = null;
    private List<Hole> ActiveHoles = new List<Hole>();

    private BoxCollider2D Collider = null;
    private Rigidbody2D RB = null;

    private List<Hole> Holes = new List<Hole>();

    private float HoleSpawnTimer = 0.0f;

    [SerializeField] private float SinkingVelocity = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in UnActivatedHoles)
        {
            Holes.Add(t.GetComponent<Hole>());
        }
        Collider = GetComponent<BoxCollider2D>();
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HoleSpawnTimer < 0)
        {
            ActivateHole();
        }

        Sink();

        HoleSpawnTimer -= Time.deltaTime;
    }

    private void ActivateHole()
    {
        int holeIterator = Random.Range(0, Holes.Count);
        Holes[holeIterator].gameObject.SetActive(true);
        ActiveHoles.Add(Holes[holeIterator]);
        Holes.RemoveAt(holeIterator);
        //float randomX = Random.Range(Collider.bounds.min.x, Collider.bounds.max.x);
        //Vector3 spawnPos = new Vector3(randomX, transform.position.y);
        
        //Hole newHole = Instantiate(HolePrefab, spawnPos, transform.rotation, transform);
        //Holes.Add(newHole);

        HoleSpawnTimer = HoleSpawnDelay;
    }

    private void Sink()
    {
        int unpluggedHoles = 0;
        foreach(Hole h in ActiveHoles)
        {
            if(!h.IsPlugged)
            {
                unpluggedHoles++;
            }
        }
        Vector2 movement = new Vector2(transform.position.x, transform.position.y - (unpluggedHoles * SinkingVelocity * Time.deltaTime));

        RB.MovePosition(movement);
    }
}
