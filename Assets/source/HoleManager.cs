using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class HoleManager : MonoBehaviour
{
    [SerializeField] private string TeamName = "Pirates";

    [SerializeField] private string WinText = " won!";
 

    [SerializeField] private float HoleSpawnDelay = 5.0f;

    [SerializeField] private Transform UnActivatedHoles = null;
    private List<Hole> ActiveHoles = new List<Hole>();

    private Rigidbody2D RB = null;

    private List<Hole> Holes = new List<Hole>();

    private float HoleSpawnTimer = 0.0f;

    private bool GameIsOver = false;

    [SerializeField] private float SinkingVelocity = 20.0f;

    [SerializeField] private GameObject GameOverCanvas = null;


    void Start()
    {
        foreach(Transform t in UnActivatedHoles)
        {
            Holes.Add(t.GetComponent<Hole>());
        }
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!GameIsOver)
        {
            ValidateGameState();

            if (HoleSpawnTimer < 0)
            {
                ActivateHole();
            }

            Sink();

            HoleSpawnTimer -= Time.deltaTime;
        }
    }

    private void ActivateHole()
    {
        int holeIterator = Random.Range(0, Holes.Count);
        Holes[holeIterator].gameObject.SetActive(true);
        ActiveHoles.Add(Holes[holeIterator]);
        Holes.RemoveAt(holeIterator);

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

    private void ValidateGameState()
    {
        if(GameOverCanvas.activeInHierarchy)
        {
            GameIsOver = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D pCollision)
    {
        if(GameIsOver)
        {
            return;
        }
        if (pCollision.CompareTag("SinkTrigger"))
        {
            RB.isKinematic = false;
            RB.gravityScale = 0.5f;
            GameOverCanvas.gameObject.SetActive(true);
            GameObject.Find("WinText").GetComponent<TextMeshProUGUI>().text = TeamName + WinText;
            GameIsOver = true;
        }
    }
}
