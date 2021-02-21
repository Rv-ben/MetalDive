using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{

    // The animator component.
    public Animator animator;
    public EnemyMovement mover;
    private NavMeshAgent agent;
    public Health health;
    public static Transform targetPoint;
    private CapsuleCollider capsuleCollider;
    private int maxHealth = 100;
    private int healthValue;


    /// <summary>
    /// Initialize all the variables with objects upon game starts.
    /// </summary>
    void Start()
    {
        
        setEnemyHealthMax(maxHealth);   // ------------------------------ delete after testing

        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.radius = 0.5f;

        agent = GetComponent<NavMeshAgent>();

        agent.radius = 0.1f;

        // No brake when near obstacle
        agent.autoBraking = false;

        // No rotation while walking
        agent.updateRotation = false;

        GameObject targetObject = GameObject.Find("Target");

        targetPoint = targetObject.transform;

        mover.MoveToNextTarget();
    }

    /// <summary>
    /// Enemy idle behavior.
    /// </summary>
    void Update()
    {
        // Checks every frame whether start moving to the next point or idle.
        mover.Idle(animator);
    }

    public void setEnemyHealthMax(int healthMax) {
        Debug.Log("Enemy Health max is now set!");
        health.SetMax(healthMax);
        this.maxHealth = healthMax;
        this.healthValue = maxHealth;
    }

    public void setEnemyCurrentHealth(int healthValue) {
        Debug.Log("Enemy Health value has been updated to " + healthValue);
        health.SetHealth(healthValue);
        if (this.healthValue - healthValue < 0)
        {
            this.healthValue = 0;
        }
        else
            this.healthValue -= healthValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            setEnemyCurrentHealth(-10);
        }
    }
}
