using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class SpiderController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public EnemyState currentState;
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset Patrol, Alert, Attack, Death;
    private string currentAnimationPlaying;

    [Header("Vision")]
    public Transform sensor;
    public float depth;
    public float chaseRange;
    public float attackRange;

    //public float speed = 2f;
    public float attackDistance = 5f;
    public float detectionRange = 6;
    public bool inAgroRange;
    public float animationDuration;
    public float health;
    public float stunDuration;
    public float patrolSpeed;
    public Vector2 target;
    public bool isReached;
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
        patrolSpeed = 15f;
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }

    public void Update()
    {
        currentState.Update();
        target = pointA.position;
  
            if (Vector2.Distance(sensor.transform.position, target) <= 2f)
        {
            isReached = true;
            if (target == (Vector2)pointA.position)
            {
                target = pointB.position;
            }
            else
            {
                target = pointA.position;
            }
        }

            // check attack
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
                    transform.localScale = new Vector2(1, 1);
                }
            }
            else
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
        }
    }

    public void IsPlayerDetected()
    {
        Vector3 dirToTarget = (Player.position - transform.position).normalized;
        float disToTarget = Vector3.Distance(transform.position, Player.position);
    }

    public void Flip()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    // Spine-Unity runtime code, sets animation and defines animation completion
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        if (animation.name.Equals(currentAnimationPlaying))
        {
            return;
        }
        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timescale;
        animationEntry.Complete += AnimationEntry_Complete;
        currentAnimationPlaying = animation.name;
    }

    private void AnimationEntry_Complete(TrackEntry trackEntry)
    {
        // this method returns a state to its previous state when animation finishes. can just use state machine transition instead.
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

    public void Move()
    {
        Vector2 direction = (target - rb.position).normalized;
        rb.velocity = new Vector2(direction.x * patrolSpeed, rb.velocity.y);
        if (Vector2.Distance(rb.position, target) < 0.1f)
        {
            if (target == (Vector2)pointA.position)
            {
                target = pointB.position;
            }
            else
            {
                target = pointA.position;
            }
            FlipTarget();
        }
    }

    public void FlipTarget()
    {
        if ((Vector2)sensor.position == (Vector2)pointB.position)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
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
    public void CheckHealth()
    {
        if (Input.GetKeyDown(KeyCode.E)/*currentHealth <= 0*/)
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
       skeletonAnimation.AnimationState.SetAnimation(0, "Death", false);
        Destroy(gameObject, 2f);
        // Wait for the animation to finish
    }
}
