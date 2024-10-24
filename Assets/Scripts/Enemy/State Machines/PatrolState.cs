using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    public PatrolState(SpiderController controller) : base(controller) { }

    private Vector2 direction;
    private List<Transform> points;
    private int index = 0;
    private Transform sensor;

    public override void Enter()
    {
        spider.SetAnimation(spider.Patrol, true, 1f);
        points = new List<Transform>();
        points.Add(spider.pointA);
        points.Add(spider.pointB);
        SwitchDirection();
    }

    public override void Update()
    {
        direction = spider.sensor.transform.position.x < points[index].position.x ? Vector2.right : Vector2.left;
        spider.rb.velocity = new Vector2(direction.x * spider.patrolSpeed, spider.rb.velocity.y);
        spider.FlipTarget();
            Debug.Log("<color=red>turned around</color>");
           
    }

    private void SwitchDirection()
    {
        index++;
        index = index % points.Count;

        Vector3 scale = spider.transform.localScale;
        direction = spider.sensor.transform.position.x < points[index].position.x ? Vector2.left : Vector2.right;
        scale.x = spider.sensor.transform.position.x < points[index].position.x ? -1 : 1;
        spider.transform.localScale = scale;
    }

    public override void Exit() 
    {
        Debug.Log("patroled");
    }
}
