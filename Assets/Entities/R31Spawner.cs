using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R31Spawner : MonoBehaviour
{
    // The animator component.
    public Animator animator;
    // Ensures we only aim at the ground and not, like, walls.
    [SerializeField] public LayerMask aimLayer;
    
    // The R31 Player Model.
    [SerializeField] public GameObject playerModel;
    // Player Movement Scripts.
    public PlayerMovement mover;
    // Player Shooting Scripts.
    public PlayerShooting shooter;

    // Where the bullet comes out.
    [SerializeField] public Transform ejector;
    // Creates a field in which you can decide how long the delay is between shots.
    [SerializeField] public float shotDelay;
    // Float that determines when the Player is able to shoot again.
    public float timeToShoot;
    // What bullet gets fired.
    [SerializeField] public Bullet bulletPrefab;
    // How fast the bullet goes by default.
    [SerializeField] public float bulletSpeed;

    Vector3 position;
    Quaternion rotation;

    public R31Spawner()
    {
        mover = new PlayerMovement(aimLayer);
    }

    /// <summary>
    /// Starts up the entire animation process.  As this will be changed across the entire game, Awake is necessary instead of Start.
    /// This is because it will keep the generated Animator on permanently.
    /// </summary>
    private void Awake() => animator = GetComponent<Animator>();

    public GameObject getPlayer()
    {
        //return new Player(aimLayer);
        return new GameObject();
    }
}