using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] public int damage;
    public void ShootDirection(Vector2 bulletDirection, float bulletSpeed)
    {
        bulletRB.linearVelocity = bulletDirection * bulletSpeed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
