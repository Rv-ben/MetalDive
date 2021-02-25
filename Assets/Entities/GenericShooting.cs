using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericShooting : MonoBehaviour
{
    // Where the bullet comes out.
    public Transform ejector;
    // Creates a field in which you can decide how long the delay is between shots.
    public float shotDelay;
    // Float that determines when the Player is able to shoot again.
    public float timeToShoot;
    // What bullet gets fired.
    public Bullet bulletPrefab;
    // How fast the bullet goes by default.
    public float bulletSpeed;

    public GenericShooting(Transform ejector, float shotDelay, Bullet bulletPrefab, float bulletSpeed)
    {
        this.ejector = ejector;
        this.shotDelay = shotDelay;
        this.bulletPrefab = bulletPrefab;
        this.bulletSpeed = bulletSpeed;
    }

    /// <summary>
    /// Checks if the Player is able to shoot their weapon yet.
    /// </summary>
    /// <returns>True or False, depending on if the Player is able to shoot yet.</returns>
    public bool shotReady() => Time.time >= timeToShoot;

    /// <summary>
    /// Handles the action of "Shooting" for the Player.  Ejects a bullet from a specified position down a specified line.
    /// </summary>
    public void Shoot(Animator animator, string tagName)
    {
        // Determines the next time you're able to shoot.
        timeToShoot = Time.time + shotDelay;
        // Instantiates a pre-chosen bullet at specified ejector facing the same way as the ejector.
        Bullet bullet = Instantiate(bulletPrefab, ejector.position, Quaternion.identity);

        bullet.tag = tagName;
        // Generates a Rigidbody for the generated bullet.
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        // Tells the bullet where to go and how fast it needs to go.
        rb.velocity = transform.forward * bulletSpeed;
    }
}
