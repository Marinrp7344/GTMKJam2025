using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] public int damage;
    public float distanceToDisappear;
    public bool startedBullet;
    public Vector2 startingPosition;

    public UnityEvent destroyed;
    public bool isBlastBullet;
    public bool isMine;
    public bool isCannon;
    public GameObject blastRadius;
    public BeatDelay beatDelay;
    private void Start()
    {
        if(isMine)
        {
            beatDelay.StartDelay(new BeatBasedDuration(2,0,0));
        }
    }

    public void CreateExplosion()
    {
        Destroy(gameObject);
        Instantiate(blastRadius, transform.position, Quaternion.identity);
        
    }


    private void Update()
    {
        if (distanceToDisappear != 0 && startedBullet)
        {
            if (DistanceFromStart() > distanceToDisappear)
            {
                destroyed.Invoke();
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!isMine)
            {
                if (!isBlastBullet && !isCannon)
                {
                    Health enemyHealth = collision.gameObject.GetComponent<Health>();
                    enemyHealth.TakeDamage(damage);
                    PlayerStats.Instance.IncreaseScore(enemyHealth.scoreWorth);
                    destroyed.Invoke();
                    Destroy(gameObject);
                }
                else if(isBlastBullet && !isCannon)
                {
                    Instantiate(blastRadius, transform.position, Quaternion.identity);
                    destroyed.Invoke();
                    Destroy(gameObject);
                }
                else if(!isBlastBullet && isCannon)
                {
                    Health enemyHealth = collision.gameObject.GetComponent<Health>();
                    enemyHealth.TakeDamage(damage);
                    PlayerStats.Instance.IncreaseScore(enemyHealth.scoreWorth);
                }
            }

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (!isMine)
            {
                if (!isBlastBullet)
                {
                    destroyed.Invoke();
                    Destroy(gameObject);
                }
                else
                {
                    Instantiate(blastRadius, transform.position, Quaternion.identity);
                    destroyed.Invoke();
                    Destroy(gameObject);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (isCannon)
            {
                Health enemyHealth = collision.gameObject.GetComponent<Health>();
                enemyHealth.TakeDamage(damage);
                PlayerStats.Instance.IncreaseScore(enemyHealth.scoreWorth);
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            destroyed.Invoke();
            Destroy(gameObject);
        }
    }
}
