using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    private float patrolSpeed;
    private Vector2 pointA;
    private Vector2 pointB;
    private bool movingToB;
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

    public override void Exit() 
    { 
    }
}
