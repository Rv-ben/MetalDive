using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Where the bullet comes out.
    public Transform ejector;
    // Float that determines when the Player is able to shoot again.
    public float timeToShoot;

    // How fast the bullet goes by default.
    public float bulletSpeed;

    // The stored bullet
    public GameObject bullet;

    /// <summary>
    /// Checks if the Player is able to shoot their weapon yet.
    /// </summary>
    /// <returns>True or False, depending on if the Player is able to shoot yet.</returns>
    public bool shotReady() => Time.time >= timeToShoot;

    public void setBullet(GameObject bullet)
    {
        this.bullet = bullet;
    }

    public void Shoot(Animator animator, float shotDelay, int spreadNumber, GameObject bullet)
    {
        // Determines the next time you're able to shoot.
        timeToShoot = Time.time + shotDelay;
        for (int i = 0; i < spreadNumber; i++)
        {
            // Instantiates a pre-chosen bullet at specified ejector facing the same way as the ejector.
            bullet = Instantiate(bullet, ejector.position, Quaternion.Euler(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)));
            // Tells the bullet where to go and how fast it needs to go.
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }
        // Will replace with Enums later.
        if (animator.GetBool("Unarmed"))
        {
            // Tell the animator to play the "DropKick" animation.
            animator.SetTrigger("DropKick");
        }
        else if (animator.GetBool("PistolEquipped"))
        {
            // Tell the animator to play the "Firing" animation.
            animator.SetTrigger("FiringPistol");
        }
        else if (animator.GetBool("LongGunEquipped"))
        {
            // Tell the animator to play the "Firing" animation.
            animator.SetTrigger("FiringLongarm");
        }
    }
}
