using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class WeaponSwitchMenu : MonoBehaviour
{
    [NonSerialized]

    private int weaponIndex = 0;

    [SerializeField] public GameObject menuCanvas;

    private Texture2D currentImage;

    private int weaponLength = Enum.GetNames(typeof(WeaponEnum)).Length;

    private Texture2D[] weaponImages;

    private void Start()
    {
        weaponImages = Resources.LoadAll<Texture2D>("Weapons/Icons");
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public void NextItem(GameObject button)
    {
        switch (button.name)
        {
            case "LeftButton":
                Debug.Log("left item");
                if (weaponIndex == 0)
                {
                    weaponIndex = weaponLength - 1;
                }
                else
                {
                    weaponIndex--;
                }
                SwitchWeapon(weaponIndex);
                break;


            case "RightButton":
                Debug.Log("right item");
                if (weaponIndex == weaponLength - 1)
                {
                    weaponIndex = 0;
                }
                else
                {
                    weaponIndex++;
                }
                SwitchWeapon(weaponIndex);
                break;
        }

    } 

    public void SwitchWeapon(int index)
    {
        
        var enum_ = ((WeaponEnum)index);
        Debug.Log(enum_);
        Debug.Log(weaponImages.Length);
        foreach (var image in weaponImages)
        {
            Debug.Log("BEFORE IF " + image.ToString());
            if (image.ToString().Equals(enum_.ToString()))
            {
                
                Instantiate(image);
                currentImage = image;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void LoadWeapon()
    {
        Instantiate(this.currentImage);
        //weaponView.SendMessage("SwitchWeapon", weapons[weaponIndex]);
    }



}
