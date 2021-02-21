using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Where the bullet comes out.
    public Transform ejector;
    // Creates a field in which you can decide how long the delay is between shots.
    public float shotDelay;
    // Float that determines when the Player is able to shoot again.
    public float timeToShoot;

    // A List of Bullets to pull from.
    public GameObject pistolBullet;
    public GameObject shotgunPellet;

    // How fast the bullet goes by default.
    public float bulletSpeed;

    /**
    public PlayerShooting(Transform ejector, float shotDelay, GameObject bulletPrefab, float bulletSpeed)
    {
        this.ejector = ejector;
        this.shotDelay = shotDelay;
        this.bulletPrefab = bulletPrefab;
        this.bulletSpeed = bulletSpeed;
    }
    **/

    /// <summary>
    /// Checks if the Player is able to shoot their weapon yet.
    /// </summary>
    /// <returns>True or False, depending on if the Player is able to shoot yet.</returns>
    public bool shotReady() => Time.time >= timeToShoot;

    /// <summary>
    /// Handles the action of "Shooting" for the Player.  Ejects a bullet from a specified position down a specified line.
    /// </summary>
    public void shootPistol(Animator animator)
    {
        // Determines the next time you're able to shoot.
        timeToShoot = Time.time + shotDelay;
        // Instantiates a pre-chosen bullet at specified ejector facing the same way as the ejector.
        GameObject bullet = Instantiate(pistolBullet, ejector.position, Quaternion.identity);
        // Tells the bullet where to go and how fast it needs to go.
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        // Tell the animator to play the "Firing" animation.
        animator.SetTrigger("FiringPistol");
    }

    public void shootShotgun(Animator animator)
    {
        // Determines the next time you're able to shoot.
        timeToShoot = Time.time + shotDelay;
        for (int i = 0; i < 9; i++)
        {
            // Instantiates a pre-chosen bullet at specified ejector facing the same way as the ejector.
            GameObject bullet = Instantiate(shotgunPellet, ejector.position, Quaternion.Euler(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)));
            // Tells the bullet where to go and how fast it needs to go.
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }
        // Tell the animator to play the "Firing" animation.
        animator.SetTrigger("FiringShotgun");
    }
}
