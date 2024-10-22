using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemyState
{
    public DeathState(EnemyController controller) : base(controller) { }

    public override void Enter()
    {
        enemyController.PlayAnimation("Death");
        var currentTrackEntry = enemyController.skeletonAnimation.AnimationState.GetCurrent(0);
        if (currentTrackEntry == null)
        {
            Object.Destroy(enemyController.gameObject, 2f);
        }
        // Optionally disable collider and other components
        // Schedule destruction after animation finishes?
    }

    public override void Update() { /* Handle death state logic if necessary */ }

    public override void Exit() { /* Handle exiting death state */ }
}
