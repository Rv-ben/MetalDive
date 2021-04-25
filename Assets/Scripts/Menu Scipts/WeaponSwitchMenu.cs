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

    //[SerializeField] public GameObject menuCanvas;

    private Sprite currentImage;

    private GameObject weaponView;

    private int weaponLength = Enum.GetNames(typeof(WeaponEnum)).Length - 1;

    private Sprite[] weaponImages;

    private void Start()
    {
        Debug.Log("hello? ");
        weaponImages = Resources.LoadAll<Sprite>("Weapons/Icons");
        currentImage = weaponImages[0];
        // weaponView = GameObject.Find("WeaponView");
        this.GetComponent<Image>().sprite = currentImage;
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
        Debug.Log("index is now " + index);
        Debug.Log(enum_);
        Debug.Log("weaponImages size " + weaponImages.Length);
        Debug.Log("enum size is " + weaponLength);

        currentImage = weaponImages[index];
        Debug.Log("currentImage name " + currentImage.ToString());

        this.GetComponent<Image>().sprite = currentImage;

        

        //foreach (var image in weaponImages)
        //{
        //    if (image.ToString().Equals(enum_.ToString()))
        //    {
        //        this.GetComponent<Image>().sprite = image;
        //        currentImage = image;
        //    }
        //}
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
