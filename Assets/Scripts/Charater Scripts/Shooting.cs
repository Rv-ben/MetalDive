using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
// Gets the enums from the ShootingEnums file.
using static ShootingEnums;

public class Shooting : MonoBehaviour
{
    // Where the bullet comes out.
    public Transform ejector;
    // Float that determines when the Player is able to shoot again.  Set during run-time in the Shoot() function.
    public float timeToShoot;
    // Checks if the weapon projectiles should grow around the Player like a bubble.
    // public bool grows;
    // The stored bullet object.  Needed to determine most of the logic of Shoot()
    public GameObject bullet;
    public Animator animator;

    // Enum
    public Animations animEnum;
    // Dictionary tying an Enum to a String, which is used to determine an animation to play.
    public Dictionary<Animations, string> animationDictionary;

    /// <summary>
    /// Checks if the Player is able to shoot their weapon yet.
    /// </summary>
    /// <returns>True or False, depending on if the Player is able to shoot yet.</returns>
    public bool shotReady() => Time.time >= timeToShoot;

    public void Start()
    {
        animationDictionary = new Dictionary<Animations, string>();
        animationDictionary.Add(Animations.Unarmed, "DropKick");
        animationDictionary.Add(Animations.Pistol, "FiringPistol");
        animationDictionary.Add(Animations.Guitar, "PlayingGuitar");

        // Temporary stuff here
        animator.SetBool("Unarmed", false);

        animEnum = Animations.Pistol;

        animator.SetBool("PistolEquipped", true);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(Animations.Pistol, bullet);
        }
    }

    /// <summary>
    /// Generic Shoot Function that handles bullet spawning and behavior.
    /// </summary>
    /// <param name="animator"> The passed-in Animator that determines what animations play. </param>
    /// <param name="bullet"> The passed-in Bullet object. </param>
    public void Shoot(Animations anim, GameObject bullet)
    {
        // Plays a specific animation based on the weapon equipped.
        animator.SetTrigger(animationDictionary[animEnum]);
        // Determines the next time you're able to shoot off the equipped bullet's shot delay.
        timeToShoot = Time.time + bullet.GetComponent<Bullet>().getShotDelay();
        // Basically spawns multiple bullets, up to the bullet's inherent spread number.
        for (int i = 0; i < bullet.GetComponent<Bullet>().getSpreadNumber(); i++)
        {
            // Instantiates a pre-chosen bullet at specified ejector facing the same way as the ejector.
            GameObject bull = Instantiate(bullet, ejector.position, ejector.rotation);
            bull.GetComponent<Rigidbody>().velocity = transform.forward + new Vector3(0, 0, Random.Range(-bullet.GetComponent<Bullet>().getSpread(), bullet.GetComponent<Bullet>().getSpread())) * bullet.GetComponent<Bullet>().getBulletSpeed();
        }
        // Reset the time to shoot.
        timeToShoot = 0;
        
    }
}