using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{

    // The animator component.
    public Animator animator;
    public EnemyMovement mover;
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
        capsuleCollider.radius = 0.1f;
    }

    /// <summary>
    /// This Update Function will run all code within every frame of the game.
    /// This function will keep updating the orientation of Enemy health bar to face the camera.
    /// </summary>
    void Update()
    {
        GameObject.Find("EnemyCanvas").transform.LookAt(Camera.main.transform.position);
    }

    public void setEnemyHealthMax(int healthMax) {
        Debug.Log("Enemy Health max is now set");
        health.SetMax(healthMax);
        this.maxHealth = healthMax;
        this.healthValue = maxHealth;
    }

    public void setEnemyCurrentHealth(int changingValue) {
        if (this.healthValue + changingValue < 0)
        {
            this.healthValue = 0;
        }
        else
            this.healthValue += changingValue;

        health.SetHealth(this.healthValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerShoot"))
        {
            setEnemyCurrentHealth(-10);
        }
    }
}
