using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    [SerializeField] private float rotationValue;

    public void ShowShield()
    {
        shield.transform.rotation = Quaternion.Euler(0, 0, rotationValue);
        shield.SetActive(true);
        rotationValue += 90;
    }

    public void HideShield()
    {
        shield.SetActive(false);
    }
}
