using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Speed variable.  Adjustable in Editor thanks to SerializeField.
    public float movementSpeed = 0.5f;
    // Ensures we only aim at the ground and not, like, walls.
    // [SerializeField] public LayerMask aimLayer;
    public LayerMask aimLayer;

    public PlayerMovement(LayerMask aimLayer) 
    {
        this.aimLayer = aimLayer;
    }

    /// <summary>
    /// Handles the 3D movement of the Player Character.
    /// </summary>
    public void Move(Animator animator)
    {

        // Tells the Animator you're not firing, you're moving, which cancels out the Firing animation.  Can be improved later.
        animator.SetBool("Firing", false);
        // Create an infinte-distance Ray from the Main Character to the Mouse's position and beyond.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Check if an infinitely-long Ray generated from the Mouse to the character intersects with a collider on the aimLayer mask.
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayer))
        {
            // Calculates direction from the hitInfo variable generated in the if statement and the position of the Player.
            var direction = hitInfo.point - transform.position;
            // Says that "Forward" of the Player is the generated direction, presumably the mouse.
            transform.forward = direction;
        }

        // Reads input on the Horizontal Axis, which is assigned to A and D.
        float horizontal = Input.GetAxis("Horizontal");
        // Reads input on the Vertical Axis, which is assigned to W and S.
        float vertical = Input.GetAxis("Vertical");

        // Creates a Movement Vector that places Horizontal inputs on the X axis and Vertical inputs on the Z Axis, while leaving the Y Axis empty.
        // This prevents displacement from the "Top Down" perspective.
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // If the Player is inputting anything:
        if (movement.magnitude > 0)
        {
            // Adjust the Player Character's movement Vector according to framerate and the Editor-specified value.
            movement *= movementSpeed * Time.deltaTime;
            // Translates the Player's generated movement Vector into movement of the Player Character object in the World Space.
            transform.Translate(movement, Space.World);
        }

        // Animations for Up and Down use the Dot Product of the normalized movement Vector and the "forward" transform assigned earlier.
        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        // Animations for Left and Right use the Dot Product of the normalized movement Vector and the "forward" transform assigned earlier.
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        // Changes the position of the Player Model and smoothly transitions between animations.
        animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }
}
