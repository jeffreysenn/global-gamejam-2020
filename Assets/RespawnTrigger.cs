using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    //[SerializeField] private Transform RespawnPoint = null;
    [SerializeField] private Vector3 RespawnPoint = new Vector3(10, 13);

    void Start()
    {

    }

    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D pCollision)
    {
        if (RespawnPoint == null)
            return;
        if(pCollision.CompareTag("Player"))
        {
            pCollision.transform.position = RespawnPoint;
        }
    }
}
