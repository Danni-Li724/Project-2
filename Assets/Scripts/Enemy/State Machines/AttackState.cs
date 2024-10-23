using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    private int attackCount = 0;
    private float attackCooldown = 1f;
    private float attackDuration = 2f;
    private float attackTimer = 0f;
    public AttackState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        enemyController.SetAnimation(enemyController.Attack, false, 1f);
    }

    public override void Update()
    {
        enemyController.TurnToPlayer();
    }

    public override void Exit()
    {
        Debug.Log("attack admistered");
    }
}