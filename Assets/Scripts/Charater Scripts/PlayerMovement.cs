using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Speed variable.  Adjustable in Editor thanks to SerializeField.
    public float movementSpeed = .5f;

    public float turnSpeed = 200f;
    // Ensures we only aim at the ground and not, like, walls.
    [SerializeField] Animator animator;

    public CharacterController characterController;

    public bool isDead;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;

        isDead = false;
    }

    /// <summary>
    /// Handles the 3D movement of the Player Character.
    /// </summary>
    public void Update()
    {
        // Reads input on the Horizontal Axis, which is assigned to A and D.
        float horizontal = Input.GetAxis("Mouse X");
        // Reads input on the Vertical Axis, which is assigned to W and S.
        float vertical = Input.GetAxis("Vertical");

        // Creates a Movement Vector that places Horizontal inputs on the X axis and Vertical inputs on the Z Axis, while leaving the Y Axis empty.
        // This prevents displacement from the "Top Down" perspective.
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        animator.SetFloat("VelocityZ", movement.magnitude);

        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);

        if (vertical != 0)
        {
            characterController.Move(transform.forward * movementSpeed * vertical);
        }
    }
}
