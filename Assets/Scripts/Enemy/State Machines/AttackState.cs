using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    private int attackCount = 0;
    private float attackCooldown = 1f;
    private float attackDuration = 1f;
    private float attackTimer = 0f;
    public AttackState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        attackCount = 0;
        enemyController.PlayAnimation("Attack");
    }


    public override void Update()
    {
        enemyController.rb.velocity = Vector2.zero;

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDuration)
        {
            attackCount++;
            attackTimer = 0f;

            if (attackCount < 2)
            {
                enemyController.PlayAttackAnimation();
            }
            else
            {
                attackCount = 0;
                enemyController.StartCoroutine(enemyController.PauseBeforeNextAttack());
            }
        }

        if (!enemyController.inAgroRange)
        {
            enemyController.SwitchState(new PatrolState(enemyController));
        }
    }

    // attack logic (deal damage)
    // check if the attack animation finished to return to patrol
    //var currentTrackEntry = enemyController.skeletonAnimation.AnimationState.GetCurrent(0);
    //if (currentTrackEntry == null)
    // {
    //    enemyController.SwitchState(new PatrolState(enemyController));
    //}
    //enemyController.SwitchState(new PatrolState(enemyController));

    public override void Exit()
    {
        Debug.Log("attack admistered");
    }
}