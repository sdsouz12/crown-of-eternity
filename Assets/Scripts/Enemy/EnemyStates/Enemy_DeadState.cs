using UnityEngine;

public class Enemy_DeadState : EnemyState
{

    private Collider2D col;
   
    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        col = enemy.GetComponent<Collider2D>();
    }

     public override void Enter()
    {
        anim.enabled = false;
        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);
        col.enabled = false;
        //Debug.Log("Enter Dead State");
        stateMachine.SwitchOffStateMachine();
    }
}
