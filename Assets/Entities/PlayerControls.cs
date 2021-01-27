using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    // Speed variable.  Adjustable in Editor thanks to SerializeField.
    [SerializeField] float movementSpeed;
    // Ensures we only aim at the ground and not, like, walls.
    [SerializeField] LayerMask aimLayer;
    // The animator component.
    Animator animator;
    // Where the bullet comes out.
    public Transform ejector;
    // Creates a field in which you can decide how long the delay is between shots.
    [SerializeField] float shotDelay;
    // Float that determines when the Player is able to shoot again.
    float timeToShoot;
    // What bullet gets fired.
    [SerializeField] Bullet bulletPrefab;
    // How fast the bullet goes by default.
    [SerializeField] float bulletSpeed = 15f;

    // Starts up the entire animation process.  As this will be changed across the entire game, Awake is necessary instead of Start.
    private void Awake() => animator = GetComponent<Animator>();

    void Update() {
        Move();
        // Should be mapped to Left Click for now.
        if (shotReady() & Input.GetMouseButtonDown(0))
        {
            // Shoot function.
            Shoot();
        }
    }

    private void Move()
    {
        // Tells the Animator you're not firing, you're moving.  Cancels out the Firing animation.  Can be improved later.
        animator.SetBool("Firing", false);
        // Create a Ray from the Main Character to the Mouse's position.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Use Physics to create a Raycast out of the ground, the max distance to shoot, infinity, and the ground layer.
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayer))
        {
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
        if (movement.magnitude > 0)
        {
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

    // Determines if the gun is able to shoot yet.
    bool shotReady() => Time.time >= timeToShoot;

    void Shoot()
    {
        // Sets the next time you're able to shoot.
        timeToShoot = Time.time + shotDelay;

        Bullet bullet = Instantiate(bulletPrefab, ejector.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
        
        // Tell the animator to play the "Firing" animation.
        animator.SetBool("Firing", true);
    }

    void AimTowardMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayer))
        {
            var destination = hitInfo.point;
            destination.y = transform.position.y;

            Vector3 direction = destination - transform.position;
            direction.Normalize();
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    /*
    void AimTowardMouse()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotation_on_set);
    }
    */
}
