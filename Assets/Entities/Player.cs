using UnityEngine;

public class Player : MonoBehaviour
{

    // The animator component.
    public Animator animator;
    public PlayerShooting shooter;
    public PlayerMovement mover;

    public LayerMask aimLayer;

    // Float that determines when the Player is able to move  again.
    public float delayTime;
    // Creates a field in which you can decide how long the delay is when interrupted.
    public float delay = 4;

    public float spread;
    public float shotDelay;
    public int spreadNumber;

    [SerializeField] public GameObject pellet;
    [SerializeField] public GameObject bullet;

    [SerializeField] public GameObject waves;

    public GameObject ammo;
    public bool armed;
    private bool enableMovement;
    
    public void Start()
    {
        setUnarmed();
        armed = false;
    }

    /// <summary>
    /// This Update Function will run all code within every frame of the game.
    /// It will constantly be checking for Movement and Attack Inputs and call the appropriate methods.
    /// </summary>
    public void Update()
    {
        if (enableMovement)
        {
            mover.Move(animator);
        }
        // Testing Purposes - Sets the Player's Weapon as an AR when TAB is pressed, and prevents them from continually updating it every frame.
        if (Input.GetKeyDown(KeyCode.Alpha1) && armed == false)
        {
            setPistol();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && armed == false)
        {
            setShotgun();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && armed == false)
        {
            setAR();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && armed == false)
        {
            setGuitar();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && armed == true)
        {
            setUnarmed();
        }
        if (shooter.shotReady() && Input.GetMouseButton(0))
        {
            shooter.Shoot(animator, spread, shotDelay, spreadNumber, ammo);
            if (shooter.getGuitar())
            {
                enableMovement = false;
            }
        }
        else
        {
            animator.SetBool("PlayingGuitar", false);
            enableMovement = true;
        }

    }

    public void setPistol()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("PistolEquipped", true);
        animator.SetBool("Unarmed", false);
        ammo = bullet;
        spread = 1;
        shotDelay = 1;
        spreadNumber = 1;
        armed = true;
    }

    public void setShotgun()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("LongGunEquipped", true);
        animator.SetBool("Unarmed", false);
        ammo = pellet;
        spread = 30;
        shotDelay = 1.5f;
        spreadNumber = 9;
        armed = true;
    }

    public void setAR()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("LongGunEquipped", true);
        animator.SetBool("Unarmed", false);
        ammo = bullet;
        spread = 1;
        shotDelay = 0.2f;
        spreadNumber = 1;
        armed = true;
    }

    public void setGuitar()
    {
        shooter.setGuitar(true);
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("Unarmed", true);
        animator.SetBool("LongGunEquipped", false);
        animator.SetBool("PistolEquipped", false);
        ammo = waves;
        spread = 1;
        shotDelay = 0;
        spreadNumber = 1;
        armed = true;
    }

    public void setUnarmed()
    {
        shooter.setGuitar(false);
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("Unarmed", true);
        animator.SetBool("LongGunEquipped", false);
        animator.SetBool("PistolEquipped", false);
        ammo = null;
        spread = 0;
        shotDelay = 0;
        spreadNumber = 0;
        armed = false;
    }
}
