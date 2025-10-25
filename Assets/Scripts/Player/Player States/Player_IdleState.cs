using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("[IdleState] Enter - zeroing horizontal velocity");

        if (player.moveInput.x != 0)
            stateMachine.ChangeState(player.moveState);
        
    }
}
