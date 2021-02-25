using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

/// <summary>
/// Enemy behavior
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    private float randArea = 5f;

    private float waitingTime = 2f;

    private float countWaitingTime = 0f;

    private float idlingDistanceFollowing = 0.5f;

    public static Transform targetPoint;

    private NavMeshAgent agent;

    float minimumWalk;

    private Animator anim;

    private Vector3 positionVector;

    // Attach the Shooting mechanism.
    [SerializeField] public GenericShooting shooter;

    /// <summary>
    /// Initialize all the variables with objects upon game starts.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();

        shooter = GetComponent<GenericShooting>();

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
    /// Set the enemy's range of random walking distance.
    /// </summary>
    /// <param name="walkingRange">Range of distance </param>
    public void SetMoveRange(float walkingRange)
    {
        this.randArea = walkingRange;
    }

    /// <summary>
    /// Generate the next position and move
    /// </summary>
    public void MoveToNextTarget()
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

        anim.Play("Blend Tree");

    }

    /// <summary>
    /// Get quaternion.
    /// </summary>
    /// <param name="distanceVector3"></param>
    /// <param name="vector3">vector3 for center</param>
    /// <returns>Rotating operation to have the charactor face the direction it is moving to.</returns>
    Quaternion GetQuaternion(Vector3 distanceVector3, Vector3 vector3) {
        return Quaternion.LookRotation(distanceVector3, vector3);
    }


    /// <summary>
    /// Idle behavior.
    /// </summary>
    public void Idle(Animator anim)
    {
        this.anim = anim;

        // Enemy idle while waiting for the next point and the remaining distance is less than 0.5
        if (!agent.pathPending && agent.remainingDistance < minimumWalk)
        {
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

    /// <summary>
    /// Enemy starts following player once player touches trigger collider
    /// </summary>
    /// <param name="collider">object that touched this collider</param>
    public void FollowPlayer(Collider collider)
    {
        Debug.Log("Start following");
        agent.isStopped = false;

        if (collider.tag.Equals("Player"))
        {

            anim.Play("Pistol Walk");

            // Rotates enemy before changing direction
            Quaternion rotation = GetQuaternion(collider.transform.position - transform.position, Vector3.zero);

            transform.rotation = rotation;

            // Set player's position as a next target
            agent.destination = GetComponent<Collider>().transform.position;

            // If the Enemy has a shot ready.
            if (shooter.shotReady())
            {
                // Shoot!  Pass in the animator.
                shooter.Shoot(anim, "EnemyShoot");
            }
        }

    }

    void Update() {

        if (agent.remainingDistance < idlingDistanceFollowing)
        {
            IdlePoint();
        }

        // Blend Idle and Walk animation 
        anim.SetFloat("Blend", agent.velocity.sqrMagnitude);

    }

}