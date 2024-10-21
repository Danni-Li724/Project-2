using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    public PatrolState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        enemyController.PlayAnimation("Patrol"); 
    }

    public override void Update()
    {
        Vector2 direction = enemyController.GetNextPatrolDirection();
        enemyController.Move(direction);
    }

    public override void Exit() { /* Handle exiting patrol state */ }
}
