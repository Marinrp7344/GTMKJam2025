using System;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] private Transform shootingPoint;

    [Header("Gun Editable Fields")]
    [SerializeField] private GameObject bulletType;
    [Range(0, 15)]
    [SerializeField] private float spread;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletsPerShot;

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
        bulletScript.ShootDirection(CalculateBulletDirection(), bulletSpeed);
    }

    public Vector2 CalculateBulletDirection()
    {
        //Find Pointer Direction
        Vector2 mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        Vector2 shootDirection = mousePos - (Vector2)player.position;
        float cursorDirection = Mathf.Atan2(shootDirection.x, shootDirection.y) * Mathf.Rad2Deg + 180;

        //Math for limiting the angles between shots
        float angleDifference = spread / 2;
        float outerAngle = cursorDirection - angleDifference;
        float innerAngle = cursorDirection + angleDifference;

        //Chose from the provided angles
        float chosenAngle = UnityEngine.Random.Range((int)outerAngle, (int)innerAngle);
        xDirection = (float)(Math.Cos((Math.PI / 180) * chosenAngle));
        yDirection = (float)(Math.Sin((Math.PI / 180) * chosenAngle));

        Vector2 chosenDirection = new Vector2(xDirection, yDirection);
        return chosenDirection;
    }



}
