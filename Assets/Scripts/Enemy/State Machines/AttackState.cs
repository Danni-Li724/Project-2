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
        // Implement attack logic (deal damage)
        // check if the attack animation finished to return to patrol
    }

    public override void Exit() { /* Handle exiting attack state */ }
}
