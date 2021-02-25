using UnityEngine;

public class Player : MonoBehaviour
{

    // The animator component.
    public Animator animator;
    public PlayerShooting shooter;
    public PlayerMovement mover;

    public LayerMask aimLayer;

    [SerializeField] public GameObject pellet;
    [SerializeField] public GameObject pistolBullet;
    [SerializeField] public GameObject ARBullet;
    [SerializeField] public GameObject miniBullet;
    [SerializeField] public GameObject rocket;
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
        if (Input.GetKeyDown(KeyCode.Alpha5) && armed == false)
        {
            setMiniRifle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && armed == false)
        {
            setRocketRifle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && armed == true)
        {
            setUnarmed();
        }
        if (shooter.shotReady() && Input.GetMouseButton(0))
        {
            shooter.Shoot(animator, ammo);
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
        ammo = pistolBullet;
        armed = true;
    }

    public void setShotgun()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("LongGunEquipped", true);
        animator.SetBool("Unarmed", false);
        ammo = pellet;
        armed = true;
    }

    public void setAR()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("LongGunEquipped", true);
        animator.SetBool("Unarmed", false);
        ammo = ARBullet;
        armed = true;
    }

    public void setMiniRifle()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("LongGunEquipped", true);
        animator.SetBool("Unarmed", false);
        ammo = miniBullet;
        armed = true;
    }

    public void setRocketRifle()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("LongGunEquipped", true);
        animator.SetBool("Unarmed", false);
        ammo = rocket;
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
        armed = false;
    }
}
