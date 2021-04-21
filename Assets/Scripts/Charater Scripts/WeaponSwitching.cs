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

    public Dictionary<WeaponEnum, Vector3> positionData;

    public void Start()
    {
        // Instantiate the Animation Dictionary, which will hold the state we want and the string name of the animation trigger we want.
        positionData = new Dictionary<WeaponEnum, Vector3>();
        // Ties the Dropkick Animation to the Unarmed state.
        positionData.Add(WeaponEnum.Unarmed, new Vector3(0, 0, 0));
        // Ties the Pistol Firing Animation to the Pistol state.
        positionData.Add(WeaponEnum.Pistol, new Vector3(-0.0021f, 0.0096f, 0.0005f));
        positionData.Add(WeaponEnum.Shotgun, new Vector3(-0.002f, 0.0221f, 0.0106f));
        positionData.Add(WeaponEnum.AssaultRifle, new Vector3(0.0135016f, 0.01583202f, 0.007588122f));
        // Ties the Guitar Playing Animation to the Guitar state.
        positionData.Add(WeaponEnum.Guitar, new Vector3(-0.02750365f, 0.006047898f, 0.02101708f));
        positionData.Add(WeaponEnum.Minigun, new Vector3(-0.0053f, 0.0193f, 0.009f));
        positionData.Add(WeaponEnum.RocketRifle, new Vector3(-0.00361f, 0.01585f, 0.00479f));

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
        this.model.transform.localPosition = positionData[position];
        // Set the gun's rotation equivalent to the hand's rotation.
        this.model.transform.rotation = hand.transform.rotation;
    }
}
