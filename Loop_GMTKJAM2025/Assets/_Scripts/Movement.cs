using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float xDirection;
    [SerializeField] private float yDirection;
    private Rigidbody2D playerRB;
    public Menu menu;
    public bool canMove;
    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
        else
        {
            playerRB.linearVelocity = new Vector2(0,0);
        }
    }


    private void Move()
    {
        playerRB.linearVelocity = new Vector2(xDirection * speed, yDirection * speed);
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();

        xDirection = inputVector.x;
        yDirection = inputVector.y;


        
    }

    public void OnToggleMenu(InputValue value)
    {
        if(value.isPressed)
        {
            Menu.Instance.ToggleMenu();
        }
    }
}
