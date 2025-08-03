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
    public float currentAlpha = 1f;
    public float decayRate;
    public bool startFadingLaser;
    private void Awake()
    {
        laserTransform = GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        if(startFadingLaser)
        {
            if (currentAlpha > 0)
            {
                currentAlpha -= decayRate * Time.deltaTime;
                SetAlpha();
            }
            else
            {
                currentAlpha = 0;
                SetAlpha();
                line.enabled = false;
            }

            
        }
    }

    public void SetAlpha()
    {

        Gradient lineGradient = line.colorGradient;
        GradientColorKey[] colorKeys = lineGradient.colorKeys;
        GradientAlphaKey[] alphaKeys = lineGradient.alphaKeys;


        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = currentAlpha;
        }

        Gradient newLinegradient = new Gradient();
        newLinegradient.SetKeys(colorKeys, alphaKeys);
        line.colorGradient = newLinegradient;
        /*
        Color startColor = line.startColor;
        Color endColor = line.endColor;

        startColor.a = currentAlpha;
        endColor.a = currentAlpha;

        line.startColor = startColor;
        line.endColor = endColor;
        */

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
                PlayerStats.Instance.IncreaseScore(enemyHealth.scoreWorth);
            }
        }

        line.enabled = true;
        Vector3 endPos = origin + direction * laserLength;
        line.positionCount = 2;
        line.SetPosition(0, origin);
        line.SetPosition(1, endPos);

        startFadingLaser = true;
        currentAlpha = 1;
        SetAlpha();
        
    }


}
