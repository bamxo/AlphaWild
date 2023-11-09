using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CactusAnimationController : MonoBehaviour
{
    Animator animator;

    public float lookRadius = 10f; // detection range for player

    Transform target; // reference to player
    NavMeshAgent agent; // reference to the navmesh agent
    CharacterCombat combat;

    int isWalkingHash;
    int isHitHash;
    int isDeadHash;
    int isAttackingHash;

    bool isAttacking = false;
    float attackCooldown = 1.5f; // Adjust this value as needed
    float currentCooldown = 0f;

    // initialization
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();

        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isHitHash = Animator.StringToHash("isHit");
        isDeadHash = Animator.StringToHash("isDead");
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isHit = animator.GetBool(isHitHash);
        bool isDead = animator.GetBool(isDeadHash);
        bool isAttacking = animator.GetBool(isAttackingHash);

        
        // walking
        // distance to target
        float distance = Vector3.Distance(target.position, transform.position);

        // if inside the look radius
        if (distance <= lookRadius)
        {
            // move towards target
            agent.SetDestination(target.position);
            animator.SetBool(isWalkingHash, true);

            // if within attacking distance
            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool(isWalkingHash, false);
                
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    currentCooldown -= Time.deltaTime;
                    // attacking animations
                    if (!isAttacking && currentCooldown <= 0)
                    {
                        animator.SetBool(isAttackingHash, true);
                        currentCooldown = attackCooldown;
                    }
                    
                    // attacks player
                    combat.Attack(targetStats);
                    
                    if (isAttacking)
                    {
                        animator.SetBool(isAttackingHash, false);
                    }

                }

                FaceTarget(); // make sure to face towards the target
            }
        }
        // if outside the look radius
        else if (distance > lookRadius)
        {
            animator.SetBool(isWalkingHash, false);
        }
    }

    // rotate to face the target
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
