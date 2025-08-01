using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerInput))]
public class WheelController : MonoBehaviour
{
    

    [SerializeField] private Transform weaponWheel;
    [SerializeField] private float rotation;

    PlayerInput input;

    PlayerControls playerControls;

    bool gamepad = false;
    [SerializeField] float controllerDeadzone = 0.1f;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        playerControls = new PlayerControls();
    }

    public void FollowCursor(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = rotZ - 90;
        weaponWheel.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void Update()
    {
        // no gamepad, always point at mouse
        if (!gamepad) { FollowCursor(GetVectorToMousePos()); }
       
    }

    void OnControlsChanged(PlayerInput input)
    {
        gamepad = input.currentControlScheme.Equals("Gamepad");
    }

    void OnLook(InputValue input)
    {

        Vector2 aimInput = input.Get<Vector2>();

        Vector2 aimDirection = Vector2.zero;

        if (gamepad)
        {

            if (Mathf.Abs(aimInput.x) > controllerDeadzone || Mathf.Abs(aimInput.y) > controllerDeadzone)
            {
                aimDirection = aimInput;
                FollowCursor(aimDirection);
            }
        }
        else
        {
            aimDirection = GetVectorToMousePos();
            FollowCursor(aimDirection);
        }

    }

    private Vector2 GetVectorToMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        return direction;
    }
}
