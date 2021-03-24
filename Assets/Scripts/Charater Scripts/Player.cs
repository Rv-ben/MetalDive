using UnityEngine;

public class Player : MonoBehaviour
{

    // The animator component.
    public Animator animator;
    public GenericShooting shooter;
    public PlayerMovement mover;
    public HealthSlider health;
    public LayerMask aimLayer;
    private int maxHealth = 100;
    private int healthValue;

    // The shotgun pellet prefab.
    [SerializeField] public GameObject pellet;
    // The pistol bullet prefab.
    [SerializeField] public GameObject pistolBullet;
    // The AR Bullet prefab.
    [SerializeField] public GameObject ARBullet;
    // The minigun bullet prefab.
    [SerializeField] public GameObject miniBullet;
    // The rocket  prefab.
    [SerializeField] public GameObject rocket;
    // The soundwave prefab.
    [SerializeField] public GameObject waves;
    // The currently-equipped ammo type.
    public GameObject ammo;
    // Checks if the Player is armed - prevents equipping multiple weapons.
    public bool armed;
    // checks if the player is able to move.
    private bool enableMovement;

    /// <summary>
    /// At the start of it's instantiation:
    /// </summary>
    public void Start()
    {
        setPlayerHealthMax(maxHealth);
        // Set the Player to Unarmed.
        setUnarmed();
        // Set the bool to unarmed.
        armed = false;
    }

    /// <summary>
    /// Set the player's health max value when the game starts. 
    /// This function is called from EntitySpawner script when its spawned to dungeon.
    /// </summary>
    /// <param name="healthMax">Integer number that sets a limit of health value.</param>
    public void setPlayerHealthMax(int healthMax)
    {
        Debug.Log("Player health max is now set");
        health.SetMax(healthMax);
        this.maxHealth = healthMax;
        this.healthValue = maxHealth;
    }

    /// <summary>
    /// Updates player's health value based on recieving value.
    /// The value can be positive(pickup health) or negative(attack).
    /// </summary>
    /// <param name="changingValue">Integer number to update player's current health value.</param>
    public void setPlayerCurrentHealth(int changingValue)
    {
        // If it's attack
        if (changingValue < 0) { 
            if (this.healthValue + changingValue < 0)
            {
                this.healthValue = 0;
                // TODO: Calling end-game screen???
            }
            else
                this.healthValue += changingValue;
        }
        // If it's a pickup health
        else
        {
            
            if (this.healthValue + changingValue > 100)
            {
                this.healthValue = 100;
            }
            else
                this.healthValue += changingValue;

        }

        // Updating the health value on slider to display on the screen.
        health.SetHealth(this.healthValue);
        
        //Debug.Log("Player Health value has been updated to " + this.healthValue);
    }

    /// <summary>
    /// Player's trigger function. 
    /// When player is collide with other object, this function calls appropriate functions.
    /// 1. Update current health value.
    /// </summary>
    /// <param name="other">Collide object to distinguish what function needed to be called.</param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " is coming!");
        // update current health value based on collider object.
        if (other.CompareTag("Pistol"))
        {
            setPlayerCurrentHealth(-10);
        }
        else if(other.CompareTag("AR"))
        {
            setPlayerCurrentHealth(-5);
        }
        else if (other.CompareTag("Mini"))
        {
            setPlayerCurrentHealth(-2);
        }
        else if (other.CompareTag("Pellet"))
        {
            setPlayerCurrentHealth(-5);
        }
        else if (other.CompareTag("Rocket"))
        {
            setPlayerCurrentHealth(-30);
        }
        else if (other.CompareTag("Sound"))
        {
            setPlayerCurrentHealth(-10);
        }
        else if (other.CompareTag("Health"))
        {
            setPlayerCurrentHealth(10);
        }
    }
    
    public void Update()
    {
        // Checks if the Player is allowed to move at this time.  If it's true, they're free to - if not, they're stuck in place.
        if (enableMovement)
        {
            // Moves the Player around.  Handles look rotation, too.
            mover.Move(animator);
        }
        // Testing Purposes - Handles the logic to set the Player's Weapon to the pistol when 1 is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKeyDown(KeyCode.Alpha1) && armed == false)
        {
            setPistol();
        }
        // Testing Purposes - Handles the logic to set the Player's Weapon to the shotgun when 2 is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKeyDown(KeyCode.Alpha2) && armed == false)
        {
            setShotgun();
        }
        // Testing Purposes - Handles the logic to set the Player's Weapon to the AR when 3 is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKeyDown(KeyCode.Alpha3) && armed == false)
        {
            setAR();
        }
        // Testing Purposes - Handles the logic to set the Player's Weapon to the guitar when 4 is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKeyDown(KeyCode.Alpha4) && armed == false)
        {
            setGuitar();
        }
        // Testing Purposes - Handles the logic to set the Player's Weapon to the Minigun when 5 is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKeyDown(KeyCode.Alpha5) && armed == false)
        {
            setMiniRifle();
        }
        // Testing Purposes - Handles the logic to set the Player's Weapon to the rocket launcher when 6 is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKeyDown(KeyCode.Alpha6) && armed == false)
        {
            setRocketRifle();
        }
        // Testing Purposes - Handles the logic to set the Player's Weapon to Unarmed when 0 is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKeyDown(KeyCode.Alpha0) && armed == true)
        {
            setUnarmed();
        }
        // If the Player is allowed to shoot and pressed the Left Mouse Button:
        if (shooter.shotReady() && Input.GetMouseButton(0))
        {
            // Shoot the weapon (with the ammo's characteristics)
            shooter.Shoot(animator, ammo);
            // If the weapon's ammo grows around the user:
            if (shooter.getGrows())
            {
                // Prevent movement so long as LMouse is held.
                enableMovement = false;
            }
        }
        // NEEDS REVAMP
        // Otherwise:
        else
        {
            // Probably needs to go away, but it resets the Guitar animation for now.
            animator.SetBool("PlayingGuitar", false);
            // Let the Player move around.
            enableMovement = true;
        }

    }

    /// <summary>
    /// Handles the logic of setting the Player's shooting behavior to the pistol archetype.
    /// </summary>
    public void setPistol()
    {
        // Tells the animator to play Pistol Anims.
        animator.SetBool("PistolEquipped", true);
        // Cancels Unarmed animations.
        animator.SetBool("Unarmed", false);
        // Sets the pistol bullet as currently-equipped ammo.
        ammo = pistolBullet;
        // Confirms the Player is now armed.
        armed = true;
    }

    public void setShotgun()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("LongarmEquipped", true);
        // Cancels Unarmed animations.
        animator.SetBool("Unarmed", false);
        // Sets the shotgun pellet as currently-equipped ammo.
        ammo = pellet;
        // Confirms the Player is now armed.
        armed = true;
    }

    public void setAR()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("LongarmEquipped", true);
        // Cancels Unarmed animations.
        animator.SetBool("Unarmed", false);
        // Sets the AR bullet as currently-equipped ammo.
        ammo = ARBullet;
        // Confirms the Player is now armed.
        armed = true;
    }

    public void setMiniRifle()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("LongarmEquipped", true);
        // Cancels Unarmed animations.
        animator.SetBool("Unarmed", false);
        // Sets the minigun bullet as currently-equipped ammo.
        ammo = miniBullet;
        // Confirms the Player is now armed.
        armed = true;
    }

    public void setRocketRifle()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("LongarmEquipped", true);
        // Cancels Unarmed animations.
        animator.SetBool("Unarmed", false);
        // Sets the rocket bullet as currently-equipped ammo.
        ammo = rocket;
        // Confirms the Player is now armed.
        armed = true;
    }

    public void setGuitar()
    {
        shooter.setGrows(true);
        // Tells the animator to play Unarmed Anims.
        animator.SetBool("Unarmed", true);
        // Tells the animator to cancel Long Gun Anims.
        animator.SetBool("LongarmEquipped", false);
        // Tells the animator to cancel Pistol Anims.
        animator.SetBool("PistolEquipped", false);
        // Sets the guitar soundwave as currently-equipped ammo.
        ammo = waves;
        // Confirms the Player is now armed.
        armed = true;
    }

    public void setUnarmed()
    {
        shooter.setGrows(false);
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("Unarmed", true);
        // Tells the animator to cancel Long Gun Anims.
        animator.SetBool("LongarmEquipped", false);
        // Tells the animator to cancel Pistol Anims.
        animator.SetBool("PistolEquipped", false);
        // Removes equipped ammo reference.
        ammo = null;
        // Confirms the Player is now unarmed.
        armed = false;
    }
}
