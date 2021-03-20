using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Class <c>MapObjectSpawner</c>
/// Handles the spawing of MapAssets
/// </summary>
public class Spawner : MonoBehaviour
{

    GameObject[] mapAssetsPrefabs;
    readonly Dictionary<MapAssetEnum, GameObject> mapAssetsPrefabDict;
    GameObject[] characterPrefabs;
    readonly Dictionary<CharacterEnum, GameObject> characterPrefabDict;

    /// <summary>
    /// Method <c>MapObjectSpawner</c>
    /// Populates prefab dictionary
    /// </summary>
    public Spawner()
    {   
        // SetMapAssetsDict()

        // Get all the prefabs from Resources/Map Assets/Prefabs
        mapAssetsPrefabs = Resources.LoadAll<GameObject>("Map Assets/Prefabs");
        // Get all character prefabs
        characterPrefabs = Resources.LoadAll<GameObject>("Characters/Prefabs");

        // Make empty dictionaries
        mapAssetsPrefabDict = new Dictionary<MapAssetEnum, GameObject>();
        
        characterPrefabDict = new Dictionary<CharacterEnum, GameObject>();


        foreach (GameObject spawnableObject in mapAssetsPrefabs)
        {   
            // Parse the prefab name to a enum
            var enumKey = (MapAssetEnum)Enum.Parse(typeof(MapAssetEnum), spawnableObject.name);
            // Add the enum as a key and the prefab to a dictionary
            mapAssetsPrefabDict.Add(enumKey, spawnableObject);
        }

        foreach (GameObject spawnableCharacter in characterPrefabs)
        {
            var enumKey = (CharacterEnum)Enum.Parse(typeof(CharacterEnum), spawnableCharacter.name);
            characterPrefabDict.Add(enumKey, spawnableCharacter);
        }
    }

    /// <summary>
    /// Method <c>Spawn</c>
    /// Spawns a map asset object
    /// </summary>
    /// <param name="center">Vector2 position</param>
    /// <param name="type">MapAssetEnum</param>
    /// <returns></returns>
    public GameObject SpawnMapAsset(Vector2 center, MapAssetEnum type)
    {
        // Find in prefab from the dictionary
        var prefab = this.mapAssetsPrefabDict[type];
        
        // Return a the correct map asset object
        return Instantiate(prefab, new Vector3(center.x, 0, center.y), new Quaternion());
    }

    /// <summary>
    /// Method <c>Spawn</c>
    /// Spawns a character object
    /// </summary>
    /// <param name="center">Vector2 position</param>
    /// <param name="type">CharacterEnum</param>
    /// <returns></returns>
    public GameObject SpawnCharacter(Vector2 center, CharacterEnum type)
    {
        // Store prefab from Dictionary given key
        var prefab = this.characterPrefabDict[type];

        // Return the character prefab onto the game at the given position
        return Instantiate(prefab, new Vector3(center.x, 0, center.y), new Quaternion());
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

/// <summary>
/// Enum <c>CharacterEnum</c>
/// Represents all the spawnable character objects
/// </summary>
public enum CharacterEnum
{
    Enemy,
    Player
}
