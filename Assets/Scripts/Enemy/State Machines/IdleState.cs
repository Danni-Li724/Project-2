using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        enemyController.PlayAnimation("Idle"); // Replace with your idle animation name
    }

    public override void Update()
    {
        // Transition to patrol state after a delay or condition
        enemyController.SwitchState(new PatrolState(enemyController));
    }

    public override void Exit() { /* Handle exiting idle state */ }
}
