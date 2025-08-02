using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public enum EntityType { Player, CommonEnemy, Dasher, Crazy }
    public EntityType entityType;
    [SerializeField] private int health;
    public int scoreWorth;

    public UnityEvent death;

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
        death.Invoke();

        Destroy(gameObject);
    }
}
