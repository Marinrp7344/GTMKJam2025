using UnityEngine;

public class DashEnemyMovement : MonoBehaviour
{
    public bool dashing;
    public float dashSpeed;
    [SerializeField] private int direction;
    [SerializeField] private bool dash;
    [SerializeField] private Rigidbody2D enemyRB;
    [SerializeField] private WheelController wheelController;
    [SerializeField] public GameObject player;
    public int damage;
    [Range(0, 1)]
    [SerializeField] private float tiltAmount;
    public float enemySpeed;
    public bool canMove;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            CalculateAngle();
            if (dash)
            {
                Dash();
                dash = false;
            }

            if (canMove)
            {
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * enemySpeed);
    } 

    public void Dash()
    {
        if (player != null)
        {
            if (!dashing)
            {
                DashPerpendicular();
                dashing = true;
                canMove = false;
            }
            else
            {
                canMove = true;
                dashing = false;
                enemyRB.linearVelocity = Vector2.zero;
            }
        }
    }

    public void StartDash()
    {
        if (player != null)
        {

            DashPerpendicular();
            dashing = true;
            canMove = false;

        }
    }

    public void EndDash()
    {
        if (player != null)
        {
            canMove = true;
            dashing = false;
            enemyRB.linearVelocity = Vector2.zero;
        }
    }
    private void DashPerpendicular()
    {
        Vector2 enemyDir = (transform.position - player.transform.position).normalized;
        Vector2 perpendicularDirection = Vector2.zero;
        if(direction == -1)
        {
            perpendicularDirection = new Vector2(enemyDir.y, -enemyDir.x);
        }
        else
        {
            perpendicularDirection = new Vector2(-enemyDir.y, enemyDir.x);
        }
        Vector2 curevedDirection = (Vector2)(player.transform.position - transform.position).normalized + perpendicularDirection * tiltAmount;


        enemyRB.linearVelocity = curevedDirection * dashSpeed;
    }

    private void CalculateAngle()
    {
        Vector2 cursorDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).normalized;
        Vector2 enemyDir = (transform.position - player.transform.position).normalized;
        float signedAngle = Vector2.SignedAngle(cursorDir, enemyDir);
        //Left
        if (signedAngle > 0)
        {
            direction = 1;
        }
        //Right
        else
        {
            direction = -1;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
