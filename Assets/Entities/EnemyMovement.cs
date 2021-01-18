using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{

    // Target next position
    public Transform targetPoint;

    private NavMeshAgent agent;

    // Limit of random distance
    [SerializeField] float randArea = 5f;

    // Limit of waiting time
    [SerializeField] float waitingTime = 2f;

    // Counting time
    [SerializeField] float countWaitingTime = 0f;

    Animator anim;

    // Position
    Vector3 pos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();

        // No brake when near obstacle
        agent.autoBraking = false;

        // No rotation while walking
        agent.updateRotation = false;

        NextTarget();
    }

    // Enemy follows player called by TriggerEvent 
    public void FollowPlayer(Collider collider)
    {
        Debug.Log("Start following");
        agent.isStopped = false;

        if (collider.CompareTag("Player")) {
            agent.destination = collider.transform.position;
        }
    }

    // Generate the next position and move
    void NextTarget()
    {
        agent.isStopped = false;

        // Generate the next target point
        float position_X = Random.Range(-1 * randArea, randArea);
        float position_Z = Random.Range(-1 * randArea, randArea);

        pos = targetPoint.position;
        pos.x += position_X;
        pos.z += position_Z;

        // Set the direction based on the next point
        Vector3 direction = new Vector3(pos.x, transform.position.y, pos.z);

        // Rotates enemy before start walking to the next point
        Quaternion rotation = Quaternion.LookRotation(direction - transform.position, Vector3.up);
        transform.rotation = rotation;

        agent.destination = pos;
    }

    void Update()
    {
        // Enemy idle while waiting for the next point and the remaining distance is less than 0.5
        if (!agent.pathPending && agent.remainingDistance < 0.5f) {
            IdlePoint();
        }

        // Blend Idle and Walk animation 
        anim.SetFloat("Blend", agent.velocity.sqrMagnitude);
    }

    // Behavior at the destination. Count time then generate next point.
    void IdlePoint()
    {
        agent.isStopped = true;
        countWaitingTime += Time.deltaTime;

        if (countWaitingTime > waitingTime)
        {
            NextTarget();
            countWaitingTime = 0;
        }
    }
    
}