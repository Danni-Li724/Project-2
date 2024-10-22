using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        enemyController.PlayAnimation("Attack");
    }


    public override void Update()
    { 
        enemyController.rb.velocity = Vector2.zero;
        enemyController.attacking = true;

        // attack logic (deal damage)
        // check if the attack animation finished to return to patrol
        //var currentTrackEntry = enemyController.skeletonAnimation.AnimationState.GetCurrent(0);
        //if (currentTrackEntry == null)
       // {
        //    enemyController.SwitchState(new PatrolState(enemyController));
        //}
        //enemyController.SwitchState(new PatrolState(enemyController));

    }

    public override void Exit() 
    {
        Debug.Log("One attack administered");
    }
}
