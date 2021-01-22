using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

///<summary>
///Enemy behavior
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float randArea = 10f;

    [SerializeField] float waitingTime = 2f;

    [SerializeField] float countWaitingTime = 0f;

    [SerializeField] float idlingDistanceFollowing = 2f;

    // Display remain distance in Inspector
    [SerializeField] float dispRemainDistance = 0f;

    public static Transform targetPoint;

    private NavMeshAgent agent;

    float minimumWalk;

    private Animator anim;

    private Vector3 positionVector;

    /// <summary>
    /// Initialize all the variables with objects upon game starts.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();

        GameObject targetObject = GameObject.Find("Target");

        targetPoint = targetObject.transform;

        agent.radius = 0.1f;

        // No brake when near obstacle
        agent.autoBraking = false;

        // No rotation while walking
        agent.updateRotation = false;

        MoveToNextTarget();
    }

    /// <summary>
    /// Enemy starts following player once player touches trigger collider
    /// </summary>
    /// <param name="collider">object that touched this collider</param>
    public void FollowPlayer(Collider collider)
    {
        Debug.Log("Start following");
        agent.isStopped = false;

        if (collider.name.Equals("Player")) {

            // Rotates enemy before changing direction
            Quaternion rotation = GetQuaternion(collider.transform.position - transform.position, Vector3.zero);

            transform.rotation = rotation;

            // Set player's position as a next target
            agent.destination = collider.transform.position;

            // Displaying remain distance in Inspector
            dispRemainDistance = agent.remainingDistance;

            if (agent.remainingDistance < idlingDistanceFollowing)
            {
                IdlePoint();
            }

            // Blend Idle and Walk animation 
            anim.SetFloat("Blend", agent.velocity.sqrMagnitude);
        }
    }

    /// <summary>
    /// Generate the next position and move
    /// </summary>
    void MoveToNextTarget()
    {
        float position_X;
        float position_Z;
        int randSign_X;
        int randSign_Z;

        agent.isStopped = false;

        minimumWalk = randArea / 2;

        positionVector = targetPoint.position;

        // Generate the next target point
        position_X = Random.Range(minimumWalk, randArea);
        position_Z = Random.Range(minimumWalk, randArea);
        randSign_X = Random.Range(0, 5) % 2;
        randSign_Z = Random.Range(0, 5) % 2;

        if (randSign_X == 0)
        {
            position_X = position_X * -1;
        }

        if (randSign_Z == 0)
        {
            position_Z = position_Z * -1;
        }

        positionVector.x += position_X;
        positionVector.z += position_Z;

        // Rotates enemy before start walking to the next point
        Quaternion rotation = GetQuaternion(positionVector - transform.position, Vector3.zero);

        transform.rotation = rotation;

        agent.destination = positionVector;
    }

    /// <summary>
    /// Get quaternion.
    /// </summary>
    /// <param name="distanceVector3"></param>
    /// <param name="vector3">vector3 for center</param>
    /// <returns>Quaternion</returns>
    Quaternion GetQuaternion(Vector3 distanceVector3, Vector3 vector3) {
        return Quaternion.LookRotation(distanceVector3, vector3);
    }

    /// <summary>
    /// Enemy idle behavior.
    /// </summary>
    void Update()
    {
        // Enemy idle while waiting for the next point and the remaining distance is less than 0.5
        if(!agent.pathPending && agent.remainingDistance < minimumWalk) { 
        //if (agent.remainingDistance == 0) {
            IdlePoint();
        }

        // Blend Idle and Walk animation 
        anim.SetFloat("Blend", agent.velocity.sqrMagnitude);
    }

    /// <summary>
    /// Idle behavior.
    /// </summary>
    void IdlePoint()
    {
        agent.isStopped = true;
        countWaitingTime += Time.deltaTime;

        if (countWaitingTime > waitingTime)
        {
            MoveToNextTarget();
            countWaitingTime = 0;
        }
    }
    
}