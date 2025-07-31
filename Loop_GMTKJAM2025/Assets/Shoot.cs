using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootingPoint;
    public void ActivateShoot()
    {
        Debug.Log("Shoot");
    }
}
