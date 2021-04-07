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

    GameObject[] mapAssetsPrefabs, characterPrefabs, weaponPrefabs, bulletPrefabs;
    Dictionary<MapAssetEnum, GameObject> mapAssetsPrefabDict;
    Dictionary<CharacterEnum, GameObject> characterPrefabDict;
    Dictionary<WeaponEnum, GameObject> weaponPrefabDict;
    Dictionary<BulletEnum, GameObject> bulletPrefabDict;

    /// <summary>
    /// Method <c>MapObjectSpawner</c>
    /// Populates prefab dictionary
    /// </summary>
    public void Start()
    {   
        // SetMapAssetsDict()

        // Get all the prefabs from Resources: Map Assets, Characters, Weapons, and Bullets
        mapAssetsPrefabs = Resources.LoadAll<GameObject>("Map Assets/Prefabs");
        characterPrefabs = Resources.LoadAll<GameObject>("Characters/Prefabs");
        weaponPrefabs = Resources.LoadAll<GameObject>("Weapons/Prefabs/WeaponPrefabs");
        bulletPrefabs = Resources.LoadAll<GameObject>("Weapons/Prefabs/BulletPrefabs");

        // Make empty dictionaries
        mapAssetsPrefabDict = new Dictionary<MapAssetEnum, GameObject>();
        characterPrefabDict = new Dictionary<CharacterEnum, GameObject>();
        weaponPrefabDict = new Dictionary<WeaponEnum, GameObject>();
        bulletPrefabDict = new Dictionary<BulletEnum, GameObject>();

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

        foreach (GameObject spawnableWeapon in weaponPrefabs)
        {
            var enumKey = (WeaponEnum)Enum.Parse(typeof(WeaponEnum), spawnableWeapon.name);
            weaponPrefabDict.Add(enumKey, spawnableWeapon);
        }

        foreach (GameObject spawnableBullet in bulletPrefabs)
        {
            var enumKey = (BulletEnum)Enum.Parse(typeof(BulletEnum), spawnableBullet.name);
            bulletPrefabDict.Add(enumKey, spawnableBullet);
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

        // Return the correct character prefab
        return Instantiate(prefab, new Vector3(center.x, 0, center.y), new Quaternion());
    }

    /// <summary>
    /// Method <c>Spawn</c>
    /// Spawns a weapon object
    /// </summary>
    /// <param name="center">Vector2 position</param>
    /// <param name="type">WeaponEnum</param>
    /// <returns></returns>
    public GameObject SpawnWeapon(Vector2 center, WeaponEnum type)
    {
        // Find in prefab from the dictionary
        var prefab = this.weaponPrefabDict[type];

        // Return the correct weapon object
        return Instantiate(prefab, new Vector3(center.x, 0, center.y), new Quaternion());
    }

    /// <summary>
    /// Method <c>Spawn</c>
    /// Spawns a bullet object
    /// </summary>
    /// <param name="center">Vector2 position</param>
    /// <param name="type">BulletEnum</param>
    /// <returns></returns>
    public GameObject SpawnBullet(Vector2 center, BulletEnum type)
    {
        // Store prefab from Dictionary given key
        var prefab = this.bulletPrefabDict[type];

        // Return the correct bullet prefab
        return Instantiate(prefab, new Vector3(center.x, 0, center.y), new Quaternion());
    }
}

