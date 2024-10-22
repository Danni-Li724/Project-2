using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class EnemyController : MonoBehaviour
{
    //public float speed = 2f;
    public float attackDistance = 3f;
    public bool inAgroRange;
    public float animationDuration;
    public float health;
    public float stunDuration;
    public float patrolSpeed = 8f;
    public Transform pointA;
    public Transform pointB;
    public Transform Player;
    public GameObject player;

    public Rigidbody2D rb;
    public EnemyState currentState;
    public float currentHealth;
    public Spine.Unity.SkeletonAnimation skeletonAnimation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skeletonAnimation = GetComponent<Spine.Unity.SkeletonAnimation>();
        currentHealth = health;
        SwitchState(new PatrolState(this));
        patrolSpeed = 6f;
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }

    private void Update()
    {
        currentState.Update();

        // Change to collision detection?
        if (Vector2.Distance(transform.position, Player.transform.position) < attackDistance)
        {
            SwitchState(new AttackState(this));
            inAgroRange = true;
        }
        else
        {
            inAgroRange = false;
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
        rb.MovePosition(rb.position + direction * patrolSpeed * Time.deltaTime);
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
    public void PlayAttackAnimation()
    {
        // Play your attack animation using Spine
        skeletonAnimation.AnimationState.SetAnimation(0, "Attack", false);
    }

    public IEnumerator PauseBeforeNextAttack()
    {
        yield return new WaitForSeconds(1f);
        SwitchState(new AttackState(this));
    }
    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            PlayDeathAnimation();
        }
    }
    public void PlayAnimation(string animationName)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, animationName, true);
    }

    private void PlayDeathAnimation()
    {
        // Play death animation
        skeletonAnimation.AnimationState.SetAnimation(0, "Death", false);
        Destroy(gameObject, 2f); // Wait for the animation to finish
    }
}
