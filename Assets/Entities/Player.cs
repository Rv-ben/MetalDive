using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // The animator component.
    public Animator animator;
    public PlayerShooting shooter;
    public PlayerMovement mover;

    public LayerMask aimLayer;

    public string equippedArchetype = "Unarmed";
    public bool tabPressed = false;

    // Float that determines when the Player is able to move  again.
    public float delayTime;
    // Creates a field in which you can decide how long the delay is when interrupted.
    public float delay = 4;

    /// <summary>
    /// This Update Function will run all code within every frame of the game.
    /// It will constantly be checking for Movement and Attack Inputs and call the appropriate methods.
    /// </summary>
    public void Update() {
        // The Movement Function.  Checks every frame because it also handles NOT moving.
        if (freeze())
        {
            mover.Move(animator);
        }
        /**
        // Testing Purposes - Sets the Player's Weapon as a Pistol when TAB is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKey(KeyCode.Tab) && tabPressed == false)
        {
            // Tells the animator to play Pistol Anims.
            animator.SetBool("PistolEquipped", true);
            // Sets the Archetype of Weapon to pull ammo from.
            equippedArchetype = "Pistol";
            // Prevents this from being pressed again.  Needs changing when we get more weapons.
            tabPressed = true;
        }
        **/
        // Testing Purposes - Sets the Player's Weapon as a Pistol when TAB is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKey(KeyCode.Tab) && tabPressed == false)
        {
            // Tells the animator to play Pistol Anims.
            animator.SetBool("LongGunEquipped", true);
            // Sets the Archetype of Weapon to pull ammo from.
            equippedArchetype = "Shotgun";
            // Prevents this from being pressed again.  Needs changing when we get more weapons.
            tabPressed = true;
        }

        /**
        // Testing Purposes - Sets the Player's Weapon as a Pistol when TAB is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKey(KeyCode.Tab) && equippedArchetype != "Unarmed")
        {
            animator.SetBool("Unarmed", true);
            equippedArchetype = "Unarmed";
        }
        **/

        // Attacks are mapped to Left Click for now.
        if (shooter.shotReady() && Input.GetMouseButtonDown(0)) {
            if (equippedArchetype == "Pistol")
            {
                shooter.shootPistol(animator);
            }
            else if (equippedArchetype == "Shotgun") {
                // Handles attacks with firearms.
                shooter.shootShotgun(animator);
            }
        }
    }

    public bool freeze() => Time.time >= delayTime;
}
