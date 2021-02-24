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

    public GameObject equipped;
    public bool armed;

    // DELETE THIS WHEN USING IN MAJOR CONTEXTS - TESTING ONLY
    public void Start()
    {
        spawnPlayer(Vector3.zero, Quaternion.identity);
        armed = false;
    }

    public void Update()
    {
        // Testing Purposes - Sets the Player's Weapon as a Pistol when TAB is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha1) && armed == false)
        {
            // Spawn the serialized weapon
            spawnPistol(Instantiate(pistol));
        }
        // Testing Purposes - Sets the Player's Weapon as a Pistol when TAB is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha2) && armed == false)
        {
            spawnShotgun(Instantiate(shotgun));
        }
        // Testing Purposes - Sets the Player's Weapon as a Pistol when TAB is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha3) && armed == false)
        {
            spawnAR(Instantiate(AR));
        }
        // Testing Purposes - Sets the Player's Weapon as a Pistol when TAB is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha4) && armed == false)
        {
            spawnGuitar(Instantiate(guitar));
        }
        // Testing Purposes - Sets the Player's Weapon as a Pistol when TAB is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha0) && armed == true)
        {
            despawnWeapon();
        }
    }

    /// <summary>
    /// Spawns a Player at the given coordinates.
    /// </summary>
    /// <param name="position">Vector3 position in the map where the Player is to spawn.</param>
    /// <param name="rot">Quaternion Rotation where the Player will be facing when they spawn.</param>
    public void spawnPlayer(Vector3 position, Quaternion rot)
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

    public void spawnPistol(GameObject pistol)
    {
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
        armed = true;
    }

    public void spawnShotgun(GameObject shotgun)
    {
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
        armed = true;
    }

    public void spawnAR(GameObject AR)
    {
        this.equipped = AR;
        // Stores a reference to the Player Model's hand.
        GameObject hand = GameObject.Find("mixamorig:RightHand");
        // Parent the Gun to the hand.
        equipped.transform.parent = hand.transform;
        // Set the gun to the hand's position.
        equipped.transform.position = hand.transform.position;
        // Fine tune the position of the gun.
        equipped.transform.localPosition= new Vector3(0.0135f, 0.0157f, 0.0076f);
        // Set the gun's rotation equivalent to the hand's rotation.
        equipped.transform.rotation = hand.transform.rotation;
        // Fine tune the rotation of the gun.
        equipped.transform.Rotate(-52.146f, 13.04f, 79.097f);
        // Fine tune the scale of the gun.
        equipped.transform.localScale = new Vector3(.009f, .009f, .009f);
        armed = true;
    }

    public void spawnGuitar(GameObject guitar)
    {
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
        armed = true;
    }

    public void despawnWeapon()
    {
        Destroy(equipped);
        armed = false;
    }

    /// <summary>
    /// Spawns an Enemy at the given coordinates.
    /// </summary>
    /// <param name="position">Vector3 position in the map where the Enemy is to spawn.</param>
    /// <param name="rot">Quaternion Rotation where the Enemy will be facing when they spawn.</param>
    public void spawnEnemy(Vector3 position, Quaternion rot, float walkingRange)
    {
        // Instantiates an Enemy at the position.
        GameObject ob = Instantiate(enemyPrefab, position, rot);
        // Setting enemy's range of random walking.
        enemyPrefab.GetComponent<EnemyMovement>().SetMoveRange(walkingRange);
    }

    /**
    private void Start()
    {
        // Instantiates a Player Prefab.  Stores as a GameObject.
        GameObject ob = Instantiate(playerPrefab, gameObject.transform);
    }
    **/
}