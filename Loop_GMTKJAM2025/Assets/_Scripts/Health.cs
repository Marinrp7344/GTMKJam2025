using UnityEngine;

public class Health : MonoBehaviour
{
    public enum EntityType { Player, CommonEnemy, Dasher }
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
        Debug.Log("Die");
    }
}
