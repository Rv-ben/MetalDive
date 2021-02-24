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

    public float shotDelay;
    public int spreadNumber;

    [SerializeField] public GameObject pellet;
    [SerializeField] public GameObject bullet;

    public GameObject ammo;
    public bool armed;

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
        // The Movement Function.  Checks every frame because it also handles NOT moving.
        if (freeze())
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
        if (Input.GetKeyDown(KeyCode.Alpha0) && armed == true)
        {
            setUnarmed();
        }
        // For EXTREMELY Full-Auto Fire
        if (shooter.shotReady() && Input.GetMouseButton(0))
        {
            shooter.Shoot(animator, shotDelay, spreadNumber, ammo);
        }
    }

    public bool freeze() => Time.time >= delayTime;

    public void setPistol()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("PistolEquipped", true);
        animator.SetBool("Unarmed", false);
        ammo = bullet;
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
        shotDelay = 0.2f;
        spreadNumber = 1;
        armed = true;
    }

    public void setUnarmed()
    {
        // Tells the animator to play Long Gun Anims.
        animator.SetBool("Unarmed", true);
        animator.SetBool("LongGunEquipped", false);
        animator.SetBool("PistolEquipped", false);
        ammo = null;
        shotDelay = 0;
        spreadNumber = 0;
        armed = false;
    }
}
