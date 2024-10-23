using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public EnemyState currentState;
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset Patrol, Alert, Attack, Death;
    private string currentAnimationPlaying;

    //public float speed = 2f;
    public float attackDistance = 3f;
    public float detectionRange = 6;
    public bool inAgroRange;
    public float animationDuration;
    public float health;
    public float stunDuration;
    public float patrolSpeed;
    public float currentHealth;
    public Transform pointA;
    public Transform pointB;
    public Transform Player;
    public GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
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
            SwitchState(new PatrolState(this));
            inAgroRange = false;
        }
    }

    public void TurnToPlayer()
    {
        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            if (Mathf.Abs(direction.x) <= detectionRange)
            {
                if (direction.x > 0)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                else if (direction.x < 0)
                {
                    // Player is to the left
                    transform.localScale = new Vector2(1, 1);
                }
            }
            else
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
        }
    }

    // Spine stuff
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        if (animation.name.Equals(currentAnimationPlaying))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timescale;
        currentAnimationPlaying = animation.name;
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
    /*public void PlayAttackAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Attack", false);
    }

    public IEnumerator PauseBeforeNextAttack()
    {
        yield return new WaitForSeconds(1f);
        SwitchState(new AttackState(this));
    }*/
    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            PlayDeathAnimation();
        }
    }
    /*public void PlayAnimation(string animationName)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, animationName, true);
        if (currentAnimationPlaying == animationName) return;
        currentAnimationPlaying = animationName;
        skeletonAnimation.AnimationName = currentAnimationPlaying;
    }
    public void SetLoopMode(bool willLoop)
    {
        skeletonAnimation.loop = willLoop;
    }
    */

    private void PlayDeathAnimation()
    {
        // Play death animation
        skeletonAnimation.AnimationState.SetAnimation(0, "Death", false);
        Destroy(gameObject, 2f); // Wait for the animation to finish
    }
}
