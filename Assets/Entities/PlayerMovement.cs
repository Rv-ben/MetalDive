using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Speed variable.  Adjustable in Editor thanks to SerializeField.
    [SerializeField] float movementSpeed = 5f;
    // Ensures we only aim at the ground and not, like, walls.
    [SerializeField] LayerMask aimLayer;
    // The animator component.
    Animator animator;

    // Starts up the entire animation process.  As this will be changed across the entire game, Awake is necessary instead of Start.
    private void Awake() => animator = GetComponent<Animator>();

    void Update() {
        // Create a Ray from the Main Character to the Mouse's position.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Use Physics to create a Raycast out of the ground, the max distance to shoot, infinity, and the ground layer.
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayer)) {
            // Calculates direction.
            var direction = hitInfo.point - transform.position;
            // Consider the forward to be the direction of the mouse.
            transform.forward = direction;
        }

        // Reads input.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Makes sure that A and D go on the Horizontal and W and S go on the Vertical
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // Moving
        if (movement.magnitude > 0) {
            // Adjust the speed according to framerate.
            movement *= movementSpeed * Time.deltaTime;
            // Moves object according to worldspace.
            transform.Translate(movement, Space.World);
        }

        // Animating using Dot Product of normalized movement and the forward transform.
        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        // Animating using Dot Product of normalized movement and the right transform.
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        // Changes the position of the Player Model and smoothly transitions between animations.
        animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }
}
