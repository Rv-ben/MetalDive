using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    // Weapon
    public WeaponEnum enums;

    // Passed-in.
    public GameObject model;

    // These'll be passed in.
    public WeaponSwitching(WeaponEnum enums, GameObject model) {
        this.enums = enums;
        this.model = model;
    }

    public Dictionary<WeaponEnum, (Vector3, Quaternion)> dictionary;
    public (Vector3, Quaternion) positionData;

    public void Start()
    {
        // Instantiate the Animation Dictionary, which will hold the state we want and the string name of the animation trigger we want.
        dictionary = new Dictionary<WeaponEnum, (Vector3, Quaternion)>();
        // Ties the Dropkick Animation to the Unarmed state.
        dictionary.Add(WeaponEnum.Unarmed, (new Vector3(0, 0, 0), new Quaternion()));
        // Ties the Pistol Firing Animation to the Pistol state.
        Vector3 pistolRotation = new Vector3(-180, 180, 180);
        dictionary.Add(WeaponEnum.SciFiHandGun, (new Vector3(-0.00201f, 0.01224f, 0.00013f), Quaternion.Euler(pistolRotation)));
        Vector3 shotgunRotation = new Vector3(-180, 180, 180);
        dictionary.Add(WeaponEnum.ShotGun, (new Vector3(-0.0019f, 0.0219f, -0.0001f), Quaternion.Euler(shotgunRotation)));
        Vector3 arRotation = new Vector3(-180, 180, 180);
        dictionary.Add(WeaponEnum.LightAR, (new Vector3(0.0095f, 0.0097f, 0.0031f), Quaternion.Euler(arRotation)));
        // Ties the Guitar Playing Animation to the Guitar state.
        dictionary.Add(WeaponEnum.ElectricGuitar, (new Vector3(0.0311f, 0.0047f, -0.0014f), new Quaternion()));
        Vector3 miniRotation = new Vector3(-180, 180, 180);
        dictionary.Add(WeaponEnum.MiniRifle, (new Vector3(-0.0053f, 0.0193f, 0.009f), Quaternion.Euler(miniRotation)));
        Vector3 launcherRotation = new Vector3(-180, 180, 180);
        dictionary.Add(WeaponEnum.Launcher, (new Vector3(-0.006F, 0.014f, 0f), Quaternion.Euler(launcherRotation)));

        // weapons = ShootingEnums.Pistol;
    }

    public void Update()
    {
        // Testing Purposes - Sets the Player's Weapon as the prefabbed Pistol when 1 is pressed.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Spawn the serialized weapon.  Shouldn't use the Instantiation.
            // WeaponSwitch(weapons, Instantiate(weapon));
            Debug.Log("SWITCHEROO");
        }
    }

    public void WeaponSwitch(WeaponEnum position, GameObject model)
    {
        // Deactivates the original model
        this.model.SetActive(false);
        // Overwrites the model.
        this.model = model;
        // Overwrites the model with the new model.
        this.model.SetActive(true);
        // Stores a reference to the Player Model's hand.
        GameObject hand = GameObject.Find("mixamorig:RightHand");
       // Location Data for hand.  Delete later.
        Debug.Log(hand.transform.rotation);
        // Parent the Gun to the hand.
        this.model.transform.parent = hand.transform;
        // Set the gun to the hand's position.
        this.model.transform.position = hand.transform.position;
        // Fine tune the position of the gun.
        this.model.transform.localPosition = dictionary[position].Item1;
        // Set the gun's rotation equivalent to the hand's rotation.
        this.model.transform.rotation = hand.transform.rotation;
        // Fine tune the model's rotation.
        this.model.transform.rotation = dictionary[position].Item2;
    }
}
