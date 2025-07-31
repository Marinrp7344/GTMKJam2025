using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private Rigidbody2D bulletRB;

    public void ShootDirection(Vector2 bulletDirection, float bulletSpeed)
    {
        bulletRB.linearVelocity = bulletDirection * bulletSpeed;
    }
}
