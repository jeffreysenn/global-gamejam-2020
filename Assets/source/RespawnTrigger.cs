using UnityEngine;
using UnityEngine.Events;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 RespawnPointTeamOne = new Vector3(10, 13);
    [SerializeField] private Vector3 RespawnPointTeamTwo = new Vector3(10, 13);

    public UnityEvent triggerEvent { get; } = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D pCollision)
    {
        var rgBody = pCollision.GetComponent<Rigidbody2D>();
        if (rgBody)
        {
            rgBody.velocity = Vector2.zero;
        }
        if (pCollision.CompareTag("PlayerOne"))
        {
            pCollision.transform.position = RespawnPointTeamOne;
        }
        if (pCollision.CompareTag("PlayerTwo"))
        {
            pCollision.transform.position = RespawnPointTeamTwo;
        }

        triggerEvent.Invoke();
    }
}
