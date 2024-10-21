using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyController enemyController;

    protected EnemyState(EnemyController controller)
    {
        enemyController = controller;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
