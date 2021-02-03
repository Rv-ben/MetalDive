using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
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

    /// <summary>
    /// Starts up the entire animation process.  As this will be changed across the entire game, Awake is necessary instead of Start.
    /// This is because it will keep the generated Animator on permanently.
    /// </summary>
    private void Awake() => animator = GetComponent<Animator>();

    /// <summary>
    /// This Update Function will run all code within every frame of the game.
    /// It will constantly be checking for Movement and Attack Inputs and call the appropriate methods.
    /// </summary>
    void Update() {
        // The Movement Function.  Checks every frame because it also handles NOT moving.
        Move();
        // Attacks are mapped to Left Click for now.
        if (shotReady() & Input.GetMouseButtonDown(0)) {
            // Handles attacks with firearms.
            Shoot();
        }
    }

    /// <summary>
    /// Handles the 3D movement of the Player Character.
    /// </summary>
    private void Move() {
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

    /// <summary>
    /// Checks if the Player is able to shoot their weapon yet.
    /// </summary>
    /// <returns>True or False, depending on if the Player is able to shoot yet.</returns>
    bool shotReady() => Time.time >= timeToShoot;

    /// <summary>
    /// Handles the action of "Shooting" for the Player.  Ejects a bullet from a specified position down a specified line.
    /// </summary>
    void Shoot()
    {
        // Determines the next time you're able to shoot.
        timeToShoot = Time.time + shotDelay;
        // Instantiates a pre-chosen bullet at specified ejector facing the same way as the ejector.
        Bullet bullet = Instantiate(bulletPrefab, ejector.position, Quaternion.identity);
        // Generates a Rigidbody for the generated bullet.
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        // Tells the bullet where to go and how fast it needs to go.
        rb.velocity = transform.forward * bulletSpeed;
        // Tell the animator to play the "Firing" animation.
        animator.SetBool("Firing", true);
    }
}
