using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R31Spawner : MonoBehaviour
{
    // The animator component.
    public Animator animator;

    [SerializeField] public RuntimeAnimatorController controller;

    // Ensures we only aim at the ground and not, like, walls.
    [SerializeField] public LayerMask aimLayer;
    
    // The R31 Player Model.
    [SerializeField] public GameObject playerObject;

    // Where the bullet comes out.
    public Transform ejector;
    // Creates a field in which you can decide how long the delay is between shots.
    [SerializeField] public float shotDelay;
    // What bullet gets fired.
    [SerializeField] public Bullet bulletPrefab;
    // How fast the bullet goes by default.
    [SerializeField] public float bulletSpeed;

    /// <summary>
    /// Starts up the entire animation process.  As this will be changed across the entire game, Awake is necessary instead of Start.
    /// This is because it will keep the generated Animator on permanently.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = controller;
    }

    public GameObject getPlayer()
    {
        Player player = playerObject.AddComponent<Player>();
        player = new Player(animator);

        // Add the Shooter component.
        PlayerShooting shooter = playerObject.AddComponent<PlayerShooting>();
        shooter = new PlayerShooting(ejector, shotDelay, bulletPrefab, bulletSpeed);

        // Add the Movement component.
        PlayerMovement mover = playerObject.AddComponent<PlayerMovement>();
        // Add the aim layer onto the mover.
        mover = new PlayerMovement(aimLayer);

        Rigidbody body = playerObject.AddComponent<Rigidbody>();

        BoxCollider collider = playerObject.AddComponent<BoxCollider>();

        return playerObject;
    }
}