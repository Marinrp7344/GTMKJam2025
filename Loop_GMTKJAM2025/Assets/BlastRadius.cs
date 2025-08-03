using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class BlastRadius : MonoBehaviour
{
    [SerializeField] private float disappearTime;
    private void Awake()
    {
        StartCoroutine(TimeTilDeletion());
    }

    private IEnumerator TimeTilDeletion()
    {
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            enemyHealth.TakeDamage(10);
        }

    }
}
