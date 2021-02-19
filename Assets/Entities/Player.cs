using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // The animator component.
    public Animator animator;
    public GenericShooting shooter;
    public PlayerMovement mover;

    public LayerMask aimLayer;

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
}
