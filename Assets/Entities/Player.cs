using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // The animator component.
    public Animator animator;
    public GenericShooting shooter;
    public PlayerMovement mover;
    public Health health;
    public LayerMask aimLayer;
    private int maxHealth = 100;
    private int healthValue;

    /// <summary>
    /// Initialize all the variables with objects upon game starts.
    /// </summary>
    void Start()
    {
        setPlayerHealthMax(maxHealth);   // ------------------------------ delete after testing
    }

    /// <summary>
    /// This Update Function will run all code within every frame of the game.
    /// It will constantly be checking for Movement and Attack Inputs and call the appropriate methods.
    /// </summary>
    public void Update() {
        // The Movement Function.  Checks every frame because it also handles NOT moving.
        mover.Move(animator);
        // Attacks are mapped to Left Click for now.
        if (shooter.shotReady() & Input.GetMouseButtonDown(0)) {
            // Handles attacks with firearms.
            shooter.Shoot(animator);
        }
    }

    public void setPlayerHealthMax(int healthMax)
    {
        Debug.Log("Player health max is now set!");
        health.SetMax(healthMax);
        this.maxHealth = healthMax;
        this.healthValue = maxHealth;
    }

    public void setPlayerCurrentHealth(int healthValue)
    {
        Debug.Log("Player Health value has been updated to " + healthValue);
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
        
        if (other.CompareTag("Obstacles"))
        {
            Debug.Log("Player hitting Obstacles");
            setPlayerCurrentHealth(-10);
        }
    }
}
