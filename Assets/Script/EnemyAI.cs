// using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyAI : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack }
    public State currentState = State.Patrol;

    [Header("R�f�rences")]
    public Transform patrolPointA;
    public Transform patrolPointB;
    public Transform player;
    public Animator animator;

    [Header("Mouvement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.2f;
    public float stopDistance = 1.2f;

    [Header("D�tection")]
    public float detectionRadius = 6f;
    public LayerMask playerMask;

    [Header("Attaque")]
    public float attackCooldown = 1.2f;
    public float attackRange = 1.1f;
    public string attackTrigger = "Attack";

    private Rigidbody2D rb;
    private Vector2 patrolTarget;
    private float lastAttackTime = -999f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        patrolTarget = patrolPointA ? (Vector2)patrolPointA.position : (Vector2)transform.position;
    }

    private void Update()
    {
        // Mise � jour des flags Animator
        float speed = rb.linearVelocity.magnitude;
        if (animator)
        {
            animator.SetFloat("Speed", speed);
            animator.SetBool("IsChasing", currentState == State.Chase);
        }

        // Choix d'�tat
        bool playerInSight = PlayerInDetection();
        float distToPlayer = player ? Vector2.Distance(transform.position, player.position) : Mathf.Infinity;

        switch (currentState)
        {
            case State.Patrol:
                if (playerInSight) currentState = State.Patrol; 
                Patrol();
                break;

            case State.Chase:
                if (!playerInSight) currentState = State.Patrol;
                else if (distToPlayer <= attackRange) currentState = State.Attack;
                else Chase();
                break;

            case State.Attack:
                rb.linearVelocity = Vector2.zero;
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    if (animator && !string.IsNullOrEmpty(attackTrigger))
                    {
                        animator.SetTrigger(attackTrigger);
                        lastAttackTime = Time.time;
                    }

                    if (distToPlayer > attackRange * 1.2f)
                        currentState = State.Chase;
                }
                break;
        }
    }

    private void Patrol()
    {
        if (!patrolPointA || !patrolPointB) return;

        Vector2 target = patrolTarget;
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * patrolSpeed;

        if (dir.x != 0)
            transform.localScale = new Vector3(-Mathf.Sign(dir.x), 1, 1);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            // Changer de point
            patrolTarget = (target == (Vector2)patrolPointA.position) ?
                           (Vector2)patrolPointB.position : (Vector2)patrolPointA.position;
        }
    }

    private void Chase()
    {
        if (!player) return;

        Vector2 dir = (Vector2)player.position - (Vector2)transform.position;
        float distance = dir.magnitude;

        if (distance > stopDistance)
        {
            dir.Normalize();
            rb.linearVelocity = dir * chaseSpeed;

            if (dir.x != 0)
                transform.localScale = new Vector3(Mathf.Sign(dir.x), 1, 1);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private bool PlayerInDetection()
    {
        if (!player) return false;
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > detectionRadius) return false;

        // Optionnel : raycast simple pour la ligne de vue
        Physics2D.Raycast(
            transform.position,
            (player.position - transform.position).normalized,
            detectionRadius,
            ~0
        );

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
