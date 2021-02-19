using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{

    // The animator component.
    public Animator animator;
    public EnemyMovement mover;
    private NavMeshAgent agent;
    public static Transform targetPoint;

    /// <summary>
    /// Initialize all the variables with objects upon game starts.
    /// </summary>
    void Start()
    {
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
}
