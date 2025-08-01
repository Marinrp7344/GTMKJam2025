using UnityEngine;

public class Health : MonoBehaviour
{
    public enum EntityType { Player, CommonEnemy, Dasher, Crazy }
    public EntityType entityType;
    [SerializeField] private int health;
    

    public void TakeDamage(int damage)
    {
        health -= damage;
        DeathCheck();
    }

    public void DeathCheck()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
