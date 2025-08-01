using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] public int damage;
    public float distanceToDisappear;
    public bool startedBullet;
    public Vector2 startingPosition;

    private void Update()
    {
        if (distanceToDisappear != 0 && startedBullet)
        {
            if (DistanceFromStart() > distanceToDisappear)
            {
                Destroy(gameObject);
            }
        }
    }

    public float DistanceFromStart()
    {
        return Vector2.Distance(transform.position, startingPosition);
    }

    public void ShootDirection(Vector2 bulletDirection, float bulletSpeed)
    {
        startedBullet = true;
        startingPosition = transform.position;
        bulletRB.linearVelocity = bulletDirection * bulletSpeed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            enemyHealth.TakeDamage(damage);
            PlayerStats.Instance.IncreaseScore(enemyHealth.scoreWorth);
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
