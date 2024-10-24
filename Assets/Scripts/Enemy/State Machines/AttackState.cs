using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    private int attackCount = 0;
    private float attackCooldown = 1f;
    private float attackDuration = 2f;
    private float attackTimer = 0f;
    public AttackState(SpiderController controller) : base(controller) { }

    public override void Enter()
    {
        spider.SetAnimation(spider.Attack, false, 1f);
    }

    public override void Update()
    {
        spider.TurnToPlayer();
    }

    public override void Exit()
    {
        Debug.Log("attack admistered");
    }
}