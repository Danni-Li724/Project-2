using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    public PatrolState(SpiderController controller) : base(controller) { }

    public override void Enter()
    {
        spider.SetAnimation(spider.Patrol, true, 1f);
    }

    public override void Update()
    {
       
    }

    public override void Exit() 
    {
        Debug.Log("patroled");
    }
}
