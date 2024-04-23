using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent), typeof(Detector))]
public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator animator;
    protected NavMeshAgent navAgent;
    protected Collider collider;
    protected Detector detector;
    private Health health;

    [Header("Base Enemy properties")]
    [SerializeField] protected float speed;
    [SerializeField] protected int damagePower;

    protected virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        detector = GetComponent<Detector>();
        collider = GetComponent<Collider>();
        health = GetComponent<Health>();
        //Método de uma linha só (Lambda)
        health.OnHurt += () => animator.SetTrigger("hurt");
        health.OnDie += HandleDeath;
    }

    protected abstract void Update();

    protected void UpdateAgentDestination(Vector3 destination)
    {
        navAgent.SetDestination(destination);
    }

    protected virtual void HandleDeath()
    {
        animator.SetTrigger("die");
        collider.enabled = false;
        StartCoroutine(DestroyEnemyInSeconds(2));
    }

    private IEnumerator DestroyEnemyInSeconds(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
