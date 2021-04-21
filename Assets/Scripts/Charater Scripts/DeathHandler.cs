using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public HealthSlider healthSlider;
    public Animator animator;
    public PlayerMovement player;
    public EnemyMovement enemy;

    // Start is called before the first frame update
    void Start()
    {
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
            enemy.GetComponent<EnemyMovement>();
            player = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Whenever the Player loses all their health the first time.
        if (healthSlider.healthValue == 0 && !animator.GetBool("TotallyDead"))
        {
            animator.StopPlayback();
            Debug.Log("FUCKING DIE DAMN YOU");
            // Triggers the death animation.
            animator.SetTrigger("Dying");
            if (gameObject.tag == "Player")
            {
                // This should stop them in their tracks, like a good corpse.
                player.movementSpeed = 0;
                // With luck this'll prevent the Player from looking around.
                player.aimLayer = default;
            }
            else if (gameObject.tag == "Enemy")
            {
                enemy.walkingSpeed = 0;
            }
            // Prevents them from just getting back up.
            animator.SetBool("TotallyDead", true);
        }
    }
}
