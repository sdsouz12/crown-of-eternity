using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;
    private float lastTimeAttacked;

    private int attackDir;
    private int comboIndex = FirstComboIndex;

    // Fallback limit if player.attackVelocity is null/empty.
    private int comboLimit = 3;

    private const int FirstComboIndex = 1; // Animator uses 1-based combo indices.

    // Convenience to keep combo length in sync with the configured velocity array.
    private int MaxCombo =>
        (player.attackVelocity != null && player.attackVelocity.Length > 0)
            ? player.attackVelocity.Length
            : comboLimit;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ResetComboIndexIfNeeded();

        // Define attack direction according to input (use Sign so we get -1 or +1 when non-zero)
        attackDir = Mathf.Approximately(player.moveInput.x, 0f)
            ? player.facingDir
            : (int)Mathf.Sign(player.moveInput.x);

        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        // detect and damage enemies...
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time;
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0f)
            player.SetVelocity(0f, rb.linearVelocity.y); // use Rigidbody2D.velocity
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;

        Vector2 vel = Vector2.zero;

        // If we have per-combo velocities, pick the right one; clamp to be safe.
        if (player.attackVelocity != null && player.attackVelocity.Length > 0)
        {
            int i = Mathf.Clamp(comboIndex - FirstComboIndex, 0, player.attackVelocity.Length - 1);
            vel = player.attackVelocity[i];
        }

        player.SetVelocity(vel.x * attackDir, vel.y);
    }

    private void ResetComboIndexIfNeeded()
    {
        // Reset chain if too much time has passed
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;

        // Wrap around when exceeding the configured combo count
        if (comboIndex > MaxCombo)
            comboIndex = FirstComboIndex;
    }
}
