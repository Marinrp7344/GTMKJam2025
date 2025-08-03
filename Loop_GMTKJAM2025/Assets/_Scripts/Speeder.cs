using UnityEngine;

public class Speeder : MonoBehaviour
{
    [SerializeField] DashEnemyMovement movement;
    [SerializeField] float speedMult = 1.1f;

    public void SpeedUp()
    {
        movement.enemySpeed *= speedMult;
    }
}
