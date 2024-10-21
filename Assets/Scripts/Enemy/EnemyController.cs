using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float attackDistance = 1f;
    public float health = 100f;
    public Transform pointA;
    public Transform pointB;

    private Rigidbody2D rb;
    private EnemyState currentState;
    private float currentHealth;
    private Spine.Unity.SkeletonAnimation skeletonAnimation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skeletonAnimation = GetComponent<Spine.Unity.SkeletonAnimation>();
        currentHealth = health;
        SwitchState(new IdleState(this));
    }

    private void Update()
    {
        currentState.Update();

        // Change to collision detection.
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) < attackDistance)
        {
            SwitchState(new AttackState(this));
        }
    }

    public void SwitchState(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            SwitchState(new DeathState(this));
        }
    }

    public void Move(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    public Vector2 GetNextPatrolDirection()
    {
        if (Vector2.Distance(transform.position, pointA.position) < 0.1f)
        {
            return (pointB.position - transform.position).normalized;
        }
        else if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
        {
            return (pointA.position - transform.position).normalized;
        }

        return Vector2.zero;
    }

    public void PlayAnimation(string animationName)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, animationName, true);
    }
}
