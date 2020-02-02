using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxSpawner : MonoBehaviour
{
    private GameState state = GameState.INIT;

    [SerializeField] private Seagull[] Seagulls = null;

    [SerializeField] private KeyCode StartKey = KeyCode.Space;

    [SerializeField] private GameObject[] SpawnPrefabs = null;
    [SerializeField] private SpriteRenderer SpawnObjectSprite = null;

    private GameObject NextObjectToSpawn = null;

    [SerializeField] private float SpawnDelay = 3.0f;

    [SerializeField] private TextMeshPro Text = null;
    [SerializeField] private int SpawnMax = 7;

    private float SpawnTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = SpawnDelay;
        NextObjectToSpawn = SpawnPrefabs[Random.Range(0, SpawnPrefabs.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(StartKey))
        {
            state = GameState.RUNNING;
            SpawnBox();
        }

        if (state == GameState.INIT)
        {
            return;
        }
        GameObject[] allBoxes = GameObject.FindGameObjectsWithTag("Box");

        Text.text = Mathf.RoundToInt(SpawnTimer).ToString();

        SpawnTimer -= Time.deltaTime;
        if(SpawnTimer < 0
            && allBoxes.Length < SpawnMax)
        {
            SpawnBox();
        }
    }

    private void SpawnBox()
    {
        GameObject newBox = Instantiate(NextObjectToSpawn, transform);
        foreach(Seagull s in Seagulls)
        {
            if(!s.IsActive)
            {
                s.AttachNewBox(newBox.GetComponent<Rigidbody2D>());
                break;
            }
        }
        NextObjectToSpawn = SpawnPrefabs[Random.Range(0, SpawnPrefabs.Length)];
        SpawnObjectSprite.sprite = NextObjectToSpawn.GetComponent<SpriteRenderer>().sprite;
        SpawnTimer = SpawnDelay;
    }
}
