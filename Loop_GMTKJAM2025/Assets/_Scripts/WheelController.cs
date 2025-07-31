using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private Transform weaponWheel;
    [SerializeField] private float rotation;

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowCursor();
    }

    public void FollowCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = rotZ - 90;
        weaponWheel.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
