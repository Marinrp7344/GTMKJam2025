using UnityEngine;

public class Health : MonoBehaviour
{
    public enum EntityType { Player, CommonEnemy, Dasher, Crazy }
    public EntityType entityType;
    [SerializeField] private int health;
    public int scoreWorth;

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
        if(entityType == EntityType.Player)
        {
            RestartMenu.Instance.gameObject.SetActive(true);
        }
        
        Destroy(gameObject);
    }
}
