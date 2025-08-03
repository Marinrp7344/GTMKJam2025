using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject rotationObj;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RotateSprite();
    }

    private void RotateSprite()
    {
        Vector2 direction = rb.linearVelocity.normalized;
        float rotationDirection = Mathf.Atan2(direction.x,direction.y) * Mathf.Rad2Deg;
        rotationObj.transform.localRotation = Quaternion.Euler(0, 0, rotationDirection-180);
    }
}
