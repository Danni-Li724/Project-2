using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemyState
{
    public DeathState(SpiderController controller) : base(controller) { }

    public override void Enter()
    {
    }

    public override void Update() {  }

    public override void Exit() { Debug.Log("died"); }
}
