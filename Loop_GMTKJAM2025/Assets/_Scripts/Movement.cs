using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float xDirection;
    [SerializeField] private float yDirection;
    private Rigidbody2D playerRB;
    public Menu menu;
    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        playerRB.linearVelocity = new Vector2(xDirection * speed, yDirection * speed);
    }

    public void OnMove(InputValue value)
    {
        Vector2 movementDirections = value.Get<Vector2>();

        if(movementDirections.x != 0)
        {
            xDirection = Mathf.Sign(movementDirections.x);
        }
        else
        {
            xDirection = 0;
        }

        if (movementDirections.y != 0)
        {
            yDirection = Mathf.Sign(movementDirections.y);  
        }
        else
        {
            yDirection = 0;
        }

        
    }

    public void OnToggleMenu(InputValue value)
    {
        if(value.isPressed)
        {
            menu.ToggleMenu();
        }
    }
}
