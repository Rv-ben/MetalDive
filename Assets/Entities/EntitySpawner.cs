﻿using Cinemachine;
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

    // Prototype Hair.
    [SerializeField] public GameObject gun;

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
        // Begins to spawn non-Player things like Hair and weapons.
        // GameObject hairChild = Instantiate(hair);
        // hairChild.transform.parent = GameObject.Find("mixamorig:Head").transform;
        GameObject gunChild = Instantiate(gun);
        gunChild.tag = "PlayerShoot";
        GameObject hand = GameObject.Find("mixamorig:RightHand");
        gunChild.transform.parent = hand.transform;
        gunChild.transform.position = hand.transform.position;
        gunChild.transform.Translate(new Vector3(0.03f, -0.005f, 0.003f));
        gunChild.transform.rotation = hand.transform.rotation;
        gunChild.transform.Rotate(270, 0, 90);
        gunChild.transform.localScale = new Vector3(.9f, .9f, .9f);
        // Setting enemy's health max value.
        playerPrefab.GetComponent<Player>().setPlayerHealthMax(healthMax);
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

    /**
    private void Start()
    {
        // Instantiates a Player Prefab.  Stores as a GameObject.
        GameObject ob = Instantiate(playerPrefab, gameObject.transform);
    }
    **/
}