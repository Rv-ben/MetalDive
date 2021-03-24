using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System;

/// <summary>
/// Enemy behavior
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    private float walkingRange;
    private float waitingTime = 2f;
    private float countWaitingTime = 0f;
    private float idlingDistanceFollowing = 0.5f;
    private static Transform targetPoint;
    private NavMeshAgent agent;
    private float minimumWalk;
    private Animator anim;
    private Vector3 positionVector;
    private GameObject targetObject;
    [Serializable] public class TriggerEvent : UnityEvent<Collider>{}
    [SerializeField] private TriggerEvent _stay = new TriggerEvent();

    /// <summary>
    /// Initialize all the variables with objects upon game starts.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        targetObject = GameObject.Find("Target");
        targetPoint = targetObject.transform;
        this.walkingRange = GetComponent<Enemy>().walkingRange;
        agent.autoBraking = false;  // No brake when near obstacle
        agent.updateRotation = false;   // No rotation while walking
        
        MoveToNextTarget();
    }

    /// <summary>
    /// This Update Function will run all code within every frame of the game.
    /// Determines enemy behavior whether to stay idle or walk.
    /// </summary>
    void Update()
    {
        if (agent.remainingDistance < idlingDistanceFollowing)
        {
            Debug.Log("under Update()");
            IdlePoint();
        }
        // Blend Idle and Walk animation 
        anim.SetFloat("Blend", agent.velocity.sqrMagnitude);
    }

    /// <summary>
    /// Enemy's behaivior when it gets to the target point. 
    /// Enemy idles and generates the next position by calling MoveToNextTarget().
    /// </summary>
    void IdlePoint()
    {
        agent.isStopped = true;
        countWaitingTime += Time.deltaTime;
        Debug.Log("under IdlePoint() time: " + countWaitingTime);
        if (countWaitingTime > waitingTime)
        {
            MoveToNextTarget();
            countWaitingTime = 0;
        }
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
        minimumWalk = this.walkingRange / 2;
        positionVector = targetPoint.position;

        // Generate the next target point
        position_X = UnityEngine.Random.Range(minimumWalk, this.walkingRange);
        position_Z = UnityEngine.Random.Range(minimumWalk, this.walkingRange);
        randSign_X = UnityEngine.Random.Range(0, 5) % 2;
        randSign_Z = UnityEngine.Random.Range(0, 5) % 2;

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
        Quaternion rotation = Quaternion.LookRotation(positionVector - transform.position, Vector3.zero);

        transform.rotation = rotation;
        agent.destination = positionVector;
        anim.Play("Blend Tree");

    }

    /// <summary>
    /// Event function that triggers by colliding objects.
    /// If passed collider is tagged as "Player", then enemy will start follow.
    /// </summary>
    /// <param name="other">object that touched this collider</param>
    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            _stay.Invoke(other);
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
            Quaternion rotation = Quaternion.LookRotation(collider.transform.position - transform.position, Vector3.zero);

            transform.rotation = rotation;

            // Set player's position as a next target
            agent.destination = GetComponent<Collider>().transform.position;

        }

    }

}
