using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        enemyController.PlayAnimation("Idle"); 
    }

    public override void Update()
    {
       
    }

    public override void Exit() { /* Handle exiting idle state */ }
}
