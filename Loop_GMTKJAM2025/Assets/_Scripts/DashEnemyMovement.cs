using UnityEngine;

public class DashEnemyMovement : EnemyMovement
{
    public bool dashing;

    private void FixedUpdate()
    {
        if(dashing)
        {
            canMove = false;
            Dash();
        }
    }
    public void Dash()
    {

    }

    private void InitiateDash()
    {

    }
}
