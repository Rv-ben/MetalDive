using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PrefabSpawner : MonoBehaviour
{
    [SerializeField] Spawner spawner;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start invoked");


        spawner.SpawnWeapon(new Vector2(0, 1), WeaponEnum.Launcher);
        spawner.SpawnWeapon(new Vector2(0, 2), WeaponEnum.MiniRifle);
        spawner.SpawnWeapon(new Vector2(0, 3), WeaponEnum.LightAR);
        spawner.SpawnWeapon(new Vector2(1, 0), WeaponEnum.ElectricGuitar);
        spawner.SpawnWeapon(new Vector2(1, 1), WeaponEnum.SciFiHandGun);
        spawner.SpawnWeapon(new Vector2(1, 2), WeaponEnum.ShotGun);


        spawner.SpawnCharacter(new Vector2(0, 1), CharacterEnum.Enemy);
        spawner.SpawnCharacter(new Vector2(1, 1), CharacterEnum.Player);


        spawner.SpawnMapAsset(new Vector2(2, 0), MapAssetEnum.Bitcoin);
        spawner.SpawnMapAsset(new Vector2(2, 1), MapAssetEnum.Barrier);
        spawner.SpawnMapAsset(new Vector2(3, 0), MapAssetEnum.Elevator);
        spawner.SpawnMapAsset(new Vector2(3, 1), MapAssetEnum.Healthkit);

        spawner.SpawnBullet(new Vector2(4, 0), BulletEnum.Field);
        spawner.SpawnBullet(new Vector2(4, 1), BulletEnum.MiniGun);
        spawner.SpawnBullet(new Vector2(4, 3), BulletEnum.AR);
        spawner.SpawnBullet(new Vector2(5, 1), BulletEnum.Pistol);
        spawner.SpawnBullet(new Vector2(5, 2), BulletEnum.Rocket);
        spawner.SpawnBullet(new Vector2(5, 3), BulletEnum.ShotPellet);


    }
}
