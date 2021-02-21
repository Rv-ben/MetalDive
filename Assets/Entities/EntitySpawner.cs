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

    public int switchCount = 0;

    // DELETE THIS WHEN USING IN MAJOR CONTEXTS - TESTING ONLY
    public void Start()
    {
        spawnPlayer(Vector3.zero, Quaternion.identity);
    }

    // Testing Only - Will Figure Out Later
    public void Update()
    {
        // Testing Purposes - Sets the Player's Weapon as a Pistol when TAB is pressed.
        if (Input.GetKey(KeyCode.Tab) && switchCount == 0)
        {
            // Spawn the serialized pistol
            spawnShotgun(Instantiate(shotgun));
            switchCount++;
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
        // Stores a reference to the Player Model's hand.
        GameObject hand = GameObject.Find("mixamorig:RightHand");
        // Parent the Gun to the hand.
        pistol.transform.parent = hand.transform;
        // Set the gun to the hand's position.
        pistol.transform.position = hand.transform.position;
        // Fine tune the position of the gun.
        pistol.transform.Translate(new Vector3(-0.01f, -0.029f, 0.015f));
        // Set the gun's rotation equivalent to the hand's rotation.
        pistol.transform.rotation = hand.transform.rotation;
        // Fine tune the rotation of the gun.
        pistol.transform.Rotate(270, 0, 90);
        // Fine tune the scale of the gun.
        pistol.transform.localScale = new Vector3(.9f, .9f, .9f);
    }

    public void spawnShotgun(GameObject shotgun)
    {
        // Stores a reference to the Player Model's hand.
        GameObject hand = GameObject.Find("mixamorig:RightHand");
        // Parent the Gun to the hand.
        shotgun.transform.parent = hand.transform;
        // Set the gun to the hand's position.
        shotgun.transform.position = hand.transform.position;
        // Fine tune the position of the gun.
        shotgun.transform.Translate(new Vector3(-0.02f, -0.06f, 0.005f));
        // Set the gun's rotation equivalent to the hand's rotation.
        shotgun.transform.rotation = hand.transform.rotation;
        // Fine tune the rotation of the gun.
        shotgun.transform.Rotate(30, 0, 80);
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