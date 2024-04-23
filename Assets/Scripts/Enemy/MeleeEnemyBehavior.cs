using UnityEngine;

[RequireComponent(typeof(Health), typeof(Collider))]
public class MeleeEnemyBehavior : BaseEnemy
{
    [Header("Melee Enemy basic properties")]
    [SerializeField] private float timeToWait;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float chaseSpeed = 5;

    [Header("Melee Attack properties")]
    [SerializeField] private Vector3 attackPositionOffset;
    [SerializeField] private Vector3 attackRange;

    private float walkCooldownTimer;
    private float attackCooldownTimer;
    private bool arrived;
    private bool canAttack;
    private bool canChase;

    protected override void Awake()
    {
        base.Awake();
        navAgent.speed = base.speed;
        GetComponent<Health>().OnHurt += HandleHurt;
        UpdateAgentDestination(SortMovePoint());
    }

    protected override void Update()
    {
        UpdateAnimParams();
        if (!canAttack)
        {
            CountAttackTimer();
        }
        else
        {
            canChase = detector.isInDetectArea();
        }


        if (canChase)
        {
            ChasePlayer();
        }
        else
        {
            HandlePatrol();
        }
    }

    private void UpdateAnimParams()
    {
        animator.SetFloat("speed", base.navAgent.velocity.magnitude);
    }

    private void CountAttackTimer()
    {
        attackCooldownTimer += Time.deltaTime;
        if (attackCooldownTimer >= attackCooldown)
        {
            attackCooldownTimer = 0;
            canAttack = true;
        }
    }

    private void ChasePlayer()
    {
        navAgent.speed = chaseSpeed;

        Vector3 playerPosition = detector.GetCollidersInDetectAreaSphere()[0].GetComponent<Transform>().position;

        Collider[] detectedPlayerColliders = detector.GetCollidersInDetectAreaBox(transform.position + attackPositionOffset, attackRange);

        bool isPlayerDetected = detectedPlayerColliders.Length > 0;
        if (isPlayerDetected)
        {
            HandleAttackPlayer(detectedPlayerColliders[0]);
        }
        UpdateAgentDestination(playerPosition);
    }

    private void HandleAttackPlayer(Collider playerCollider)
    {
        if (!canAttack) return;

        if (playerCollider.TryGetComponent(out Health playerHealth))
        {
            playerHealth.TakeDamage(base.damagePower);
        }

        canChase = false;
        canAttack = false;
        navAgent.speed = 0;
        animator.SetTrigger("attack");
    }

    private void HandlePatrol()
    {
        navAgent.speed = base.speed;
        arrived = navAgent.remainingDistance <= 0.5f;

        if (arrived)
        {
            walkCooldownTimer += Time.deltaTime;
        }

        if (walkCooldownTimer >= timeToWait)
        {
            walkCooldownTimer = 0;
            UpdateAgentDestination(SortMovePoint());
        }
    }

    private Vector3 SortMovePoint()
    {
        int sortedIndex = Random.Range(0, movePoints.Length);
        return movePoints[sortedIndex].position;
    }

    private void HandleHurt()
    {
        print("Got damaged");
        canAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position + attackPositionOffset, attackRange);
    }
}
