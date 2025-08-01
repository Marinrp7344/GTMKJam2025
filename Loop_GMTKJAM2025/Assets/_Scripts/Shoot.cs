using System;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] private Transform shootingPoint;

    [Header("Gun Editable Fields")]
    [SerializeField] private GameObject bulletType;
    [Range(0, 45)]
    [SerializeField] private float spread;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletsPerShot;
    [SerializeField] private int damagePerBullet;

    [Header("Gun Debug")]
    [SerializeField] private float xDirection;
    [SerializeField] private float yDirection;

    public void ActivateShoot()
    { 
        for (int i = 0; i < bulletsPerShot; i++)
        {
            SpawnBullet();
        }
        
    }

    public void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletType, shootingPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = damagePerBullet;
        bulletScript.ShootDirection(CalculateBulletDirection(), bulletSpeed);
    }

    public Vector2 CalculateBulletDirection()
    {
        //Find Pointer Direction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (shootingPoint.position - player.position).normalized;
        float cursorDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Math for limiting the angles between shots
        float angleDifference = spread / 2;
        float outerAngle = cursorDirection - angleDifference;
        float innerAngle = cursorDirection + angleDifference;

        //Chose from the provided angles
        float chosenAngle = UnityEngine.Random.Range((int)outerAngle, (int)innerAngle);
        float chosenAngleToRandian = chosenAngle * Mathf.Deg2Rad;
        Vector2 chosenDirection = new Vector2(Mathf.Cos(chosenAngleToRandian), Mathf.Sin(chosenAngleToRandian));
        return chosenDirection;
    }



}
