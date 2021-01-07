using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Initialize the Input Handler.
    private InputHandler _input;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool rotateTowardsMouse;
    [SerializeField] Camera camera;

    /**
     * Handles Input upon awakening.
     */
    private void Awake() {
        _input = GetComponent<InputHandler>();
    }

    /**
     * "Listens" for inputs for character movement.
     */
    void Update()
    {
        // TargetVector variable that constantly updates to determine where the Player wants the Actor to move.
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        // 
        var movementVector = MoveTowardTarget(targetVector);

        if (!rotateTowardsMouse) {
            // Rotates the Character to the Target Vector.
            RotateTowardVector(movementVector);
        }
        else
        {
            RotateTowardsMouse();
        }
    }

    /**
     * Moves the Player Character towards a designated Vector.
     */
    private Vector3 MoveTowardTarget(Vector3 targetVector) {
       // Calculates Player Character movement speed by taking into account frame time.
        var speed = movementSpeed * Time.deltaTime;
        // Makes sure that movement is dependent on the Y-Axis of the In-Game Camera.
        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;

        var targetPosition = transform.position + targetVector * speed;
        // Translate the transform to have speed with direction.
        transform.position = targetPosition;

        return targetVector;
    }

    private void RotateTowardVector(Vector3 movementVector) {
        if (movementVector.magnitude == 0) {
            return;
        }
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private void RotateTowardsMouse()
    {
        Ray ray = camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f)) {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }
}
