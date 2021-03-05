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
    private GameObject enemyCanvas;
    private Vector3 rotation;

    /// <summary>
    /// Initialize all the variables with objects upon game starts.
    /// </summary>
    void Start()
    {
        setEnemyHealthMax(maxHealth);   // ------------------------------ delete after testing

        capsuleCollider = GetComponent<CapsuleCollider>();

        enemyCanvas = GameObject.Find("EnemyCanvas");

        // Getting enemyCanvas's rotation to use in Update().
        rotation = enemyCanvas.transform.localRotation.eulerAngles;
    }

    /// <summary>
    /// This Update Function will run all code within every frame of the game.
    /// This function will keep updating the orientation of Enemy health bar to stay horizontal on the screen.
    /// </summary>
    void Update()
    {
        // Get enemy's rotation.
        Vector3 enemyQuaternion = transform.localRotation.eulerAngles;

        // Use localRotation to rotate only the enemyCanvas, not the Enemy(parent). 
        // Original enemyCanvas's rotation - enemy's rotation will keep the health bar horizontal on the screen.
        enemyCanvas.transform.localRotation = Quaternion.Euler(rotation - enemyQuaternion);
    }

    /// <summary>
    /// This function sets enemy's maximum health value and should be called when enemy's spawned.
    /// </summary>
    /// <param name="healthMax">Integer value that is maximum limit of enemy health.</param>
    public void setEnemyHealthMax(int healthMax) 
    {
        health.SetMax(healthMax);
        this.maxHealth = healthMax;
        this.healthValue = maxHealth;
    }

    /// <summary>
    /// This function is to update enemy's health value every time its changed.
    /// </summary>
    /// <param name="changingValue">Integer value that represents pickup-health or damage.</param>
    public void setEnemyCurrentHealth(int changingValue) 
    {
        if (this.healthValue + changingValue < 0)
        {
            this.healthValue = 0;
        }
        else
            this.healthValue += changingValue;

        // Update the health value on slider.
        health.SetHealth(this.healthValue);
    }

    /// <summary>
    /// This function will be called when a GameObject collides with enemy.
    /// </summary>
    /// <param name="other">Object that touched enemy collider.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pistol"))
        {
            setEnemyCurrentHealth(-10);
        }
        else if (other.CompareTag("AR"))
        {
            setEnemyCurrentHealth(-5);
        }
        else if (other.CompareTag("Mini"))
        {
            setEnemyCurrentHealth(-2);
        }
        else if (other.CompareTag("Pellet"))
        {
            setEnemyCurrentHealth(-5);
        }
        else if (other.CompareTag("Rocket"))
        {
            setEnemyCurrentHealth(-30);
        }
        else if (other.CompareTag("Sound"))
        {
            setEnemyCurrentHealth(-10);
        }
    }
}
