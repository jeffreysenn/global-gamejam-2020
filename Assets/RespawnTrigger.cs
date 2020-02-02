using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 RespawnPointTeamOne = new Vector3(10, 13);
    [SerializeField] private Vector3 RespawnPointTeamTwo = new Vector3(10, 13);

    void Start()
    {

    }

    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D pCollision)
    {
        if(pCollision.CompareTag("PlayerOne"))
        {
            pCollision.transform.position = RespawnPointTeamOne;
        }
        if (pCollision.CompareTag("PlayerTwo"))
        {
            pCollision.transform.position = RespawnPointTeamTwo;
        }
    }
}
