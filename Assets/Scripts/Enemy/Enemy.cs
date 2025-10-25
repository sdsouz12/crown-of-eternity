using System.IO;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;

    [Header("Battler Details")]
    public float battleMoveSpeed = 3;
    public float attackDistance = 2;
    public float battleTimeDuration = 5;
    public float minRetreatDistance = 1;
    public Vector2 retreatVelocity;

    //public float lastTimeWasInBattle;
    //public float inGameTime;

    [Header("Movement Details")]
    public float idleTime = 2;
    public float moveSpeed = 1.4f;
    [Range(0,2)]
    public float moveAnimSpeedMultiplier = 1;

    [Header("Player detection")]
    [SerializeField] private LayerMask whatIsPalyer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10;
    public Transform player{get; private set; }


     public override void EntityDeath()
    {
        base.EntityDeath();

        stateMachine.ChangeState(deadState);
    }

    private void HandlePlayerDeath()
    {
        stateMachine.ChangeState(idleState);
    }

     public void TryEnterBattleState(Transform player)
    {
        if (stateMachine.currentState == battleState)
            return;

        if (stateMachine.currentState == attackState)
            return;

        this.player = player;
        stateMachine.ChangeState(battleState);
    }

      public Transform GetPlayerReference()
    {
        if (player == null)
            player = PlayerDetected().transform;

        return player;
    }

    //protected override void Update()
    //{
    //    base.Update();
    //    inGameTime = Time.time;
    //}

    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPalyer | whatIsGround);

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;
        
        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y));
        Gizmos.color = Color.blue ;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * attackDistance), playerCheck.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * minRetreatDistance), playerCheck.position.y));


    }

     private void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
       Player.OnPlayerDeath -= HandlePlayerDeath;  
    }
}
