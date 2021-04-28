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
    // The stored bullet object.  Needed to determine most of the logic of Shoot()
    public GameObject bullet;
    // The parent object's Animator. Needs to be set in-editor.
    public Animator animator;

    // Enum that holds the various animation triggers.
    public WeaponEnum animEnum;
    // Dictionary tying an Enum to a String, which is used to determine an animation to play.
    public Dictionary<WeaponEnum, string> animationDictionary;

    /// <summary>
    /// Checks if the Player is able to shoot their weapon yet.
    /// </summary>
    /// <returns>True or False, depending on if the Player is able to shoot yet.</returns>
    public bool shotReady() => Time.time >= timeToShoot;

    public void Start()
    {
        // Instantiate the Animation Dictionary, which will hold the state we want and the string name of the animation trigger we want.
        animationDictionary = new Dictionary<WeaponEnum, string>();
        // Ties the Dropkick Animation to the Unarmed state.
        animationDictionary.Add(WeaponEnum.Unarmed, "DropKick");
        // Ties the Pistol Firing Animation to the Pistol state.
        animationDictionary.Add(WeaponEnum.SciFiHandGun, "FiringPistol");
        // Ties the Guitar Playing Animation to the Guitar state.
        animationDictionary.Add(WeaponEnum.ElectricGuitar, "PlayingGuitar");

        // BELOW IS TEMPORARY.
        // Player is not armed.
        animator.SetBool("Unarmed", false);
        // Player's state is Pistol.
        animEnum = WeaponEnum.SciFiHandGun;
        // Player's Animation set should be pistol.
        animator.SetBool("PistolEquipped", true);
    }

    /// <summary>
    /// Plays every frame.  Checks if the Player's pressing left click, and does the bullet logic whenever it's clicked.
    /// </summary>
    public void Update()
    {
        // Checks if LClick is pushed.
        if (Input.GetMouseButtonDown(0))
        {
            // Shooting logic.  Passes in whatever state it's at, as well as the bullet to be fired.
            Shoot(animEnum, bullet);
        }
    }

    /// <summary>
    /// Shooting logic.  
    /// </summary>
    /// <param name="anim">The Enum that's currently equipped.  Should refer to the weapon that's equipped, used to play the appropriate firing animation.</param>
    /// <param name="bullet">The bullet to be fired.  Has a ton of inherent variables that must be read.</param>
    public void Shoot(WeaponEnum anim, GameObject bullet)
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
            bull.GetComponent<Rigidbody>().velocity = 
                // The forward transform - Makes sure it always faces the forward position of the ejector.s
                transform.forward + 
                // Create a new vector 3 Direction skewed randomly n the Z axis by the + and - spread of the bullet, but otherwise unchanging in it's X or Y planes.
                new Vector3(0, 0, Random.Range(-bullet.GetComponent<Bullet>().getSpread(), bullet.GetComponent<Bullet>().getSpread())) * 
                // Launch the bullet forward with it's inherent speed value.
                bullet.GetComponent<Bullet>().getBulletSpeed();
        }
        // Reset the time to shoot.
        timeToShoot = 0;
        
    }
}