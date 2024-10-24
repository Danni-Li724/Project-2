using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected SpiderController spider;

    protected EnemyState(SpiderController controller)
    {
        spider = controller;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
