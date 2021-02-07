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
    // Generic Enemy Prefab.
    [SerializeField] public GameObject enemyPrefab;

    /// <summary>
    /// Spawns a Player at the given coordinates.
    /// </summary>
    /// <param name="position">Vector3 position in the map where the Player is to spawn.</param>
    /// <param name="rot">Quaternion Rotation where the Player will be facing when they spawn.</param>
    public void spawnPlayer(Vector3 position, Quaternion rot)
    {
        // Instantiates a Player Prefab.  Stores as a GameObject.
        GameObject ob = Instantiate(playerPrefab, position, rot);     
    }

    /// <summary>
    /// Spawns an Enemy at the given coordinates.
    /// </summary>
    /// <param name="position">Vector3 position in the map where the Enemy is to spawn.</param>
    /// <param name="rot">Quaternion Rotation where the Enemy will be facing when they spawn.</param>
    public void spawnEnemy(Vector3 position, Quaternion rot)
    {
        // Instantiates an Enemy at the position.
        GameObject ob = Instantiate(enemyPrefab, position, rot);
    }
}