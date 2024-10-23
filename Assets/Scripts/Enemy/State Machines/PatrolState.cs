using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    public PatrolState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        enemyController.SetAnimation(enemyController.Patrol, true, 1f);
    }

    public override void Update()
    {
       
    }

    public override void Exit() 
    {
        Debug.Log("patroled");
    }
}
