using UnityEngine;

public class Speeder : MonoBehaviour
{
    [SerializeField] DashEnemyMovement movement;

    public void SpeedUp()
    {
        movement.enemySpeed *= 1.5f;
    }
}
