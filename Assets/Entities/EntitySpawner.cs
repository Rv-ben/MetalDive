using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns Entity Prefabs.
/// </summary>
public class EntitySpawner : MonoBehaviour
{
    // Chosen Player Prefab.  All functions for gameplay.
    [SerializeField] public GameObject playerPrefab;

    // Prototype Hair.
    [SerializeField] public GameObject hair;

    // Generic Enemy Prefab.
    [SerializeField] public GameObject enemyPrefab;

    // Equipped Pistol
    [SerializeField] public GameObject pistol;

    // Equipped Shotgun
    [SerializeField] public GameObject shotgun;

    // Equipped AR
    [SerializeField] public GameObject AR;

    // Equipped Guitar
    [SerializeField] public GameObject guitar;

    // Minigun Prefab
    [SerializeField] public GameObject miniRifle;

    // Rocket Launcher Prefab
    [SerializeField] public GameObject rocketRifle;

    // An instantiated version of the equipped weapon.  Changed whenever we spawn a new one.
    public GameObject equipped;
    // Prevents Player from equipping multiple weapons at once.
    public bool armed;

    /// <summary>
    /// Handles preliminary bools.
    /// </summary>
    public void Start()
    {
        // DELETE THIS WHEN USING IN Dungeon Spawner - TESTING ONLY
        spawnPlayer(Vector3.zero, Quaternion.identity);
        // Confirms the Player is unarmed.
        armed = false;
    }

    /// <summary>
    /// Called every frame.  Handles Weapon Switching Logic until Weapon Switch is implemented.
    /// </summary>
    public void Update()
    {
        // Testing Purposes - Sets the Player's Weapon as the prefabbed Pistol when 1 is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha1) && armed == false)
        {
            // Spawn the serialized weapon
            spawnPistol(Instantiate(pistol));
        }
        // Testing Purposes - Sets the Player's Weapon as the prefabbed Shotgun when 2 is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha2) && armed == false)
        {
            // Spawn the serialized weapon
            spawnShotgun(Instantiate(shotgun));
        }
        // Testing Purposes - Sets the Player's Weapon as a the prefabbed AR when 3 is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha3) && armed == false)
        {
            // Spawn the serialized weapon
            spawnAR(Instantiate(AR));
        }
        // Testing Purposes - Sets the Player's Weapon as the serialized Guitar when 4 is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha4) && armed == false)
        {
            // Spawn the serialized weapon
            spawnGuitar(Instantiate(guitar));
        }
        // Testing Purposes - Sets the Player's Weapon as the serialized Minigun when 5 is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha5) && armed == false)
        {
            // Spawn the serialized weapon
            spawnMiniRifle(Instantiate(miniRifle));
        }
        // Testing Purposes - Sets the Player's Weapon as the serialized Rocket Rifle when 6 is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha6) && armed == false)
        {
            // Spawn the serialized weapon
            spawnRocketRifle(Instantiate(rocketRifle));
        }
        // Testing Purposes - Sets the Player's Weapon as Unamred when 0 is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha0) && armed == true)
        {
            // Despawns the weapon.
            despawnWeapon();
        }
    }

    /// <summary>
    /// Spawns a Player at the given coordinates.
    /// </summary>
    /// <param name="position">Vector3 position in the map where the Player is to spawn.</param>
    /// <param name="rot">Quaternion Rotation where the Player will be facing when they spawn.</param>
    public void spawnPlayer(Vector3 position, Quaternion rot, int healthMax)
    {
        // Instantiates a Player Prefab.  Stores as a GameObject.
        GameObject ob = Instantiate(playerPrefab, position, rot);
        // Grabs the Cinemachine Virtual Camera that should already be placed in the scene as a Child of the Main Camera.
        CinemachineVirtualCamera cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        // Sets the Cinemachine Camera to follow the Player Object.
        cinemachineVirtualCamera.m_Follow = ob.transform;
        // Sets the Cinemachine Camera to look at the Player Object.
        cinemachineVirtualCamera.m_LookAt = ob.transform;
    }

    /// <summary>
    /// Handles spawning of the Pistol - Location, Rotation, and Scale
    /// </summary>
    /// <param name="pistol"></param>
    public void spawnPistol(GameObject pistol)
    {
        // Sets the inherent "equipped" Game Object to the Pistol
        this.equipped = pistol;
        // Stores a reference to the Player Model's hand.
        GameObject hand = GameObject.Find("mixamorig:RightHand");
        // Parent the Gun to the hand.
        equipped.transform.parent = hand.transform;
        // Set the gun to the hand's position.
        equipped.transform.position = hand.transform.position;
        // Fine tune the position of the gun.
        equipped.transform.localPosition = new Vector3(-0.0021f, 0.0096f, 0.0005f);
        // Set the gun's rotation equivalent to the hand's rotation.
        equipped.transform.rotation = hand.transform.rotation;
        // Fine tune the rotation of the gun.
        equipped.transform.Rotate(270, 0, 90);
        // Fine tune the scale of the gun.
        equipped.transform.localScale = new Vector3(.9f, .9f, .9f);
        // Confirm that the Player is armed.
        armed = true;
    }

    /// <summary>
    /// Spawns the shotgun with specialized Location and Rotation.
    /// </summary>
    /// <param name="shotgun"></param>
    public void spawnShotgun(GameObject shotgun)
    {
        // Sets the equipped weapon as the shotgun prefab.
        this.equipped = shotgun;
        // Stores a reference to the Player Model's hand.
        GameObject hand = GameObject.Find("mixamorig:RightHand");
        // Parent the Gun to the hand.
        equipped.transform.parent = hand.transform;
        // Set the gun to the hand's position.
        equipped.transform.position = hand.transform.position;
        // Fine tune the position of the gun.
        equipped.transform.localPosition = new Vector3(-0.002f, 0.0221f, 0.0106f);
        // Set the gun's rotation equivalent to the hand's rotation.
        equipped.transform.rotation = hand.transform.rotation;
        // Fine tune the rotation of the gun.
        equipped.transform.Rotate(30, 0, 80);
        // Confirm that the Player is armed.
        armed = true;
    }

    /// <summary>
    /// Spawns in the Assault Rifle Prefab - Custom Location and Rotation
    /// </summary>
    /// <param name="AR"></param>
    public void spawnAR(GameObject AR)
    {
        // Sets the inherent equipped object to the AR Prefab.
        this.equipped = AR;
        // Stores a reference to the Player Model's hand.
        GameObject hand = GameObject.Find("mixamorig:RightHand");
        // Parent the Gun to the hand.
        equipped.transform.parent = hand.transform;
        // Set the gun to the hand's position.
        equipped.transform.position = hand.transform.position;
        // Fine tune the position of the gun.
        equipped.transform.localPosition= new Vector3(0.0135016f, 0.01583202f, 0.007588122f);
        // Set the gun's rotation equivalent to the hand's rotation.
        equipped.transform.rotation = hand.transform.rotation;
        // Fine tune the rotation of the gun.
        equipped.transform.Rotate(-55.913f, 13.854f, 78.192f);
        // Fine tune the scale of the gun.
        equipped.transform.localScale = new Vector3(.009f, .009f, .009f);
        // Confirms that the Player is armed
        armed = true;
    }

    /// <summary>
    /// Spawns a guitar in the Player's hands - Custom Location and Rotation.
    /// </summary>
    /// <param name="guitar"></param>
    public void spawnGuitar(GameObject guitar)
    {
        // Set the currently equipped weapon as the Guitar.
        this.equipped = guitar;
        // Stores a reference to the Player Model's hand.
        GameObject hand = GameObject.Find("mixamorig:LeftHand");
        // Parent the Gun to the hand.
        equipped.transform.parent = hand.transform;
        // Set the gun to the hand's position.
        equipped.transform.position = hand.transform.position;
        // Fine tune the position of the gun.
        equipped.transform.localPosition = new Vector3(-0.02750365f, 0.006047898f, 0.02101708f);
        // Set the gun's rotation equivalent to the hand's rotation.
        equipped.transform.rotation = hand.transform.rotation;
        // Fine tune the rotation of the gun.
        equipped.transform.Rotate(-23.13f, 37.067f, 184.243f);
        // Fine tune the scale of the gun.
        equipped.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        // Sets the Player to Armed
        armed = true;
    }

    /// <summary>
    /// Spawns the minigun - Custom Location and Rotation
    /// </summary>
    /// <param name="miniRifle"></param>
    public void spawnMiniRifle(GameObject miniRifle)
    {
        // Equips the Minigun.
        this.equipped = miniRifle;
        // Stores a reference to the Player Model's hand.
        GameObject hand = GameObject.Find("mixamorig:RightHand");
        // Parent the Gun to the hand.
        equipped.transform.parent = hand.transform;
        // Set the gun to the hand's position.
        equipped.transform.position = hand.transform.position;
        // Fine tune the position of the gun.
        equipped.transform.localPosition = new Vector3(-0.0053f, 0.0193f, 0.009f);
        // Set the gun's rotation equivalent to the hand's rotation.
        equipped.transform.rotation = hand.transform.rotation;
        // Fine tune the rotation of the gun.
        equipped.transform.Rotate(-56.573f, 13.36f, 75.833f);
        // Fine tune the scale of the gun.
        equipped.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        // Confirms Player is armed.
        armed = true;
    }

    /// <summary>
    /// Spawns the Rocket Launcher.
    /// </summary>
    /// <param name="rocketRifle"></param>
    public void spawnRocketRifle(GameObject rocketRifle)
    {
        // Equips the Rocket Launcher.
        this.equipped = rocketRifle;
        // Stores a reference to the Player Model's hand.
        GameObject hand = GameObject.Find("mixamorig:RightHand");
        // Parent the Gun to the hand.
        equipped.transform.parent = hand.transform;
        // Set the gun to the hand's position.
        equipped.transform.position = hand.transform.position;
        // Fine tune the position of the gun.
        equipped.transform.localPosition = new Vector3(-0.00361f, 0.01585f, 0.00479f);
        // Set the gun's rotation equivalent to the hand's rotation.
        equipped.transform.rotation = hand.transform.rotation;
        // Fine tune the rotation of the gun.
        equipped.transform.Rotate(-56.409f, 15.781f, 74.286f);
        // Fine tune the scale of the gun.
        equipped.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        // Confirms the Player is armed.
        armed = true;
    }

    /// <summary>
    /// Despawns the current weapon and sets Player back to Unarmed.
    /// </summary>
    public void despawnWeapon()
    {
        // Destroys the equipped weapon instance.
        Destroy(equipped);
        // Disarms the Player.
        armed = false;
    }

    /// <summary>
    /// Spawns an Enemy at the given coordinates.
    /// </summary>
    /// <param name="position">Vector3 position in the map where the Enemy is to spawn.</param>
    /// <param name="rot">Quaternion Rotation where the Enemy will be facing when they spawn.</param>
    /// <param name="walkingRange">Enemy can move randomly within this range.</param>
    /// <param name="healthMax">Enemy's initial health value.</param>
    public void spawnEnemy(Vector3 position, Quaternion rot, float walkingRange, int healthMax)
    {
        // Instantiates an Enemy at the position.
        GameObject ob = Instantiate(enemyPrefab, position, rot);
        // Setting enemy's range of random walking.
        enemyPrefab.GetComponent<EnemyMovement>().SetMoveRange(walkingRange);
        // Setting enemy's health max value.
        enemyPrefab.GetComponent<Enemy>().setEnemyHealthMax(healthMax);
    }

    /// <summary>
    /// Spawns an empty GameObject. Enemy follows this object when its randomly walking.
    /// </summary>
    /// <param name="position">Vector3 position in the map where the GameObject is to spawn.</param>
    public void spawnEnemyTarget(Vector3 position)
    {
        GameObject gameObject = new GameObject("Target");
        gameObject.transform.position = position;
    }
}