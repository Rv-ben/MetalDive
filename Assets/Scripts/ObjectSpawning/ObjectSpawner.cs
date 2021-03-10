using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Class <c>MapObjectSpawner</c>
/// Handles the spawing of MapAssets
/// </summary>
public class MapObjectSpawner 
{

    GameObject[] mapAssets;
    readonly Dictionary<MapAssetEnum, GameObject> prefabDict;

    /// <summary>
    /// Method <c>MapObjectSpawner</c>
    /// Populates prefab dictionary
    /// </summary>
    public MapObjectSpawner()
    {
        // Get all the prefabs from Resources/Map Assets/Prefabs
        mapAssets = Resources.LoadAll<GameObject>("Map Assets/Prefabs");

        // Make an empty dictionary
        prefabDict = new Dictionary<MapAssetEnum, GameObject>();

        foreach (GameObject spawnableObject in mapAssets)
        {   
            // Parse the prefab name to a enum
            var enumKey = (MapAssetEnum)Enum.Parse(typeof(MapAssetEnum), spawnableObject.name);
            // Add the enum as a key and the prefab to a dictionary
            prefabDict.Add(enumKey, spawnableObject);
        }
    }

    /// <summary>
    /// Method <c>Spawn</c>
    /// Spawns a map asset object
    /// </summary>
    /// <param name="center">Vector2 position</param>
    /// <param name="type">MapAssetEnum</param>
    /// <returns></returns>
    public SpawnableMapObject CreateSpawnableMapObject(Vector2 center, MapAssetEnum type)
    {
        // Find in prefab from the dictionary
        var prefab = prefabDict[type];
        
        // Return a the correct map asset object
        return new SpawnableMapObject(center, prefab);
    }
}

/// <summary>
/// Class <c>SpawnableMapObject</c>
/// Represents a spawnable map asset
/// </summary>
public class SpawnableMapObject : MonoBehaviour
{
    GameObject prefab;
    Vector2 center;
    Vector2 size;

    /// <summary>
    /// Method <c>SpawnableMapObject</c>
    /// Constructor
    /// </summary>
    /// <param name="center"></param>
    /// <param name="prefab"></param>
    public SpawnableMapObject(Vector2 center, GameObject prefab)
    {
        this.center = center;
        this.prefab = prefab;
    }

    /// <summary>
    /// Method <c>IsOverLap</c>
    /// Checks if object is overlaping with another
    /// </summary>
    /// <param name="other"></param>
    /// <returns>Bool</returns>
    public bool IsOverlap(SpawnableMapObject other)
    {
        return true;
    }

    /// <summary>
    /// Method <c>Spawn</c>
    /// Instantiates a spawnable map asset
    /// </summary>
    public void Spawn()
    {
        Instantiate(prefab, new Vector3(this.center.x, 0, this.center.y), new Quaternion());
    }

}

/// <summary>
/// Enum <c>MapAssetEnum</c>
/// Reprents all the spawnable map asset objects
/// </summary>
public enum MapAssetEnum
{
    Healthkit,
    Barrier,
    Elevator,
    Bitcoin
}
