using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform shootingPoint;
    public LineRenderer line;
    Transform laserTransform;
    public float maxDistance = 100f;
    public LayerMask wall;
    public LayerMask enemy;
    public int damage;
    public float fadeDuration = 0.1f; 
    private Coroutine fadeRoutine;
    private void Awake()
    {
        laserTransform = GetComponent<Transform>();
    }

  

    public void ShootLaser()
    {
        Vector2 origin = shootingPoint.position;
        Vector2 direction = shootingPoint.up;
        float laserRadius = 1f;
        
        
        RaycastHit2D wallHit = Physics2D.CircleCast(origin, laserRadius, direction, maxDistance, wall);
        float laserLength = maxDistance;

        if(wallHit.collider != null)
        {
            laserLength = wallHit.distance;
        }


        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, laserRadius, direction, laserLength, enemy);
        foreach(var hit in hits)
        {
            if(hit.collider != null)
            {
                Health enemyHealth = hit.collider.gameObject.GetComponent<Health>();
                enemyHealth.TakeDamage(damage);
            }
        }

        Vector3 endPos = origin + direction * laserLength;
        line.positionCount = 2;
        line.SetPosition(0, origin);
        line.SetPosition(1, endPos);
        
    }

}
