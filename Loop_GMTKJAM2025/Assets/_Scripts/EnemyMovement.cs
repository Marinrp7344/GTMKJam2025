using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]public GameObject player;
    public float enemySpeed;
    public bool canMove;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    } 
    private void FixedUpdate()
    {
        if(player != null && canMove)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * enemySpeed);
    }
}
