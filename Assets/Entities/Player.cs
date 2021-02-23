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

    /// <summary>
    /// Set the player's health max value when the game starts. 
    /// This function is called from EntitySpawner script when its spawned to dungeon.
    /// </summary>
    /// <param name="healthMax">Integer number that sets a limit of health value.</param>
    public void setPlayerHealthMax(int healthMax)
    {
        Debug.Log("Player health max is now set");
        health.SetMax(healthMax);
        this.maxHealth = healthMax;
        this.healthValue = maxHealth;
    }

    /// <summary>
    /// Updates player's health value based on recieving value.
    /// The value can be positive(pickup health) or negative(attack).
    /// </summary>
    /// <param name="changingValue">Integer number to update player's current health value.</param>
    public void setPlayerCurrentHealth(int changingValue)
    {
        // If it's attack
        if (changingValue < 0) { 
            if (this.healthValue + changingValue < 0)
            {
                this.healthValue = 0;
                // TODO: Calling end-game screen???
            }
            else
                this.healthValue += changingValue;
        }
        // If it's a pickup health
        else
        {
            
            if (this.healthValue + changingValue > 100)
            {
                this.healthValue = 100;
            }
            else
                this.healthValue += changingValue;

        }

        // Updating the health value on slider to display on the screen.
        health.SetHealth(this.healthValue);
        
        //Debug.Log("Player Health value has been updated to " + this.healthValue);
    }

    /// <summary>
    /// Player's trigger function. 
    /// When player is collide with other object, this function calls appropriate functions.
    /// 1. Update current health value.
    /// </summary>
    /// <param name="other">Collide object to distinguish what function needed to be called.</param>
    private void OnTriggerEnter(Collider other)
    {
        // update current health value based on collider object.
        if (other.CompareTag("Attack"))
        {
            setPlayerCurrentHealth(-10);
        }
        else if (other.CompareTag("Health"))
        {
            setPlayerCurrentHealth(10);
        }
    }
}
