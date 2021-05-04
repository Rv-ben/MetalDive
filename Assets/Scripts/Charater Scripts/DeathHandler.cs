using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public HealthSlider healthSlider;
    public Animator animator;
    public PlayerMovement player;
    public EnemyMovement enemy;
    public bool dead;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        gameOver = false;
        // Gets all the components we need.
        healthSlider = GetComponent<HealthSlider>();
        animator = GetComponent<Animator>();
        if (gameObject.tag == "Player")
        {
            player = GetComponent<PlayerMovement>();
            enemy = null;
        }
        else if (gameObject.tag == "Enemy")
        {
            enemy = GetComponent<EnemyMovement>();
            player = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int timeToDie = Time.frameCount + 1;
        // Whenever the Player loses all their health the first time.
        if (healthSlider.healthValue == 0 && !dead)
        {
            dead = true;
            animator.StopPlayback();
            animator.Play("Flying Back Death");
            // Triggers the death animation.
            animator.SetTrigger("Dying");
            if (gameObject.tag == "Player")
            {
                // This should stop them in their tracks, like a good corpse.
                player.movementSpeed = 0;
                // This prevents them from looking around, like a good corpse.
                player.turnSpeed = 0;
                // GetComponent<PlayerMovement>().enabled = !GetComponent<PlayerMovement>().enabled;
                // GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = !GetComponent<UnityEngine.AI.NavMeshAgent>().enabled;
                GetComponent<Shooting>().enabled = !GetComponent<Shooting>().enabled;
                GetComponent<CapsuleCollider>().enabled = !GetComponent<CapsuleCollider>().enabled;
                // GetComponent<HealthSlider>().enabled = !GetComponent<HealthSlider>().enabled;
                // GetComponent<DeathHandler>().enabled = !GetComponent<DeathHandler>().enabled;
                player.isDead = true;
            }
            else if (gameObject.tag == "Enemy")
            {
                enemy.walkingRange = 0;
                enemy.walkingSpeed = 0;
                enemy.dead = true;
                // Disconnect enemy's classes.
                // GetComponent<EnemyMovement>().enabled = !GetComponent<EnemyMovement>().enabled;
                // GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = !GetComponent<UnityEngine.AI.NavMeshAgent>().enabled;
                GetComponent<Shooting>().enabled = !GetComponent<Shooting>().enabled;
                GetComponent<CapsuleCollider>().enabled = !GetComponent<CapsuleCollider>().enabled;
                // GetComponent<HealthSlider>().enabled = !GetComponent<HealthSlider>().enabled;
                // GetComponent<DeathHandler>().enabled = !GetComponent<DeathHandler>().enabled;
            }
            // Prevents them from just getting back up.
            animator.SetBool("TotallyDead", true);
        }
        else if (dead && Time.frameCount >= timeToDie)
        {
            animator.Play("Dead");
            animator.speed = 0;
            gameOver = true;
        }
    }
}
