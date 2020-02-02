using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState
{
    INIT,
    RUNNING,
    ENDED
}

public class HoleManager : MonoBehaviour
{
    private enum Team
    {
        PIRATES,
        VIKINGS
    }

    private GameState state;

    [SerializeField] private KeyCode StartKey = KeyCode.Space;

    [SerializeField] private float HoleSpawnDelay = 5.0f;

    [SerializeField] private Transform UnActivatedHoles = null;
    private List<Hole> ActiveHoles = new List<Hole>();

    private Rigidbody2D RB = null;

    private List<Hole> Holes = new List<Hole>();

    private float HoleSpawnTimer = 0.0f;

    private bool GameIsOver = false;

    [SerializeField] private Team team;

    [SerializeField] private float SinkingVelocity = 20.0f;

    void Start()
    {
        state = GameState.INIT;

        foreach(Transform t in UnActivatedHoles)
        {
            Holes.Add(t.GetComponent<Hole>());
        }
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(StartKey))
        {
            state = GameState.RUNNING;
        }

        if (state == GameState.INIT)
        {
            return;
        }
        if (!GameIsOver)
        {
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

            Invoke("LoadWinScene", 2.0f);

            GameIsOver = true;
        }
    }

    private void LoadWinScene()
    {
        if (team == Team.PIRATES)
        {
            SceneManager.LoadScene(1);
        }
        if (team == Team.VIKINGS)
        {
            SceneManager.LoadScene(2);
        }
    }
}
