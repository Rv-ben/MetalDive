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

    //[SerializeField] public GameObject menuCanvas;

    //private Sprite currentImage;

    private SpriteRenderer spriteRenderer;

    [SerializeField] GameObject weaponView;

    private GameManager gameManager;

    private int weaponLength = Enum.GetNames(typeof(WeaponEnum)).Length;

    public int currentWeaponIndex;

    private Sprite[] weaponImages;

    

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // weaponView = GameObject.Find("WeaponView");
        weaponImages = Resources.LoadAll<Sprite>("Weapons/Icons");
        // spriteRenderer = this.GetComponent<SpriteRenderer>();
        //weaponView = this.transform.GetChild(0).GetChild(1).gameObject;
        spriteRenderer = weaponView.GetComponent<SpriteRenderer>();
        // weaponView.GetComponent<SpriteRenderer>();
        // start with unarmed.
        currentWeaponIndex = weaponLength - 1;
        spriteRenderer.sprite = weaponImages[currentWeaponIndex];
        
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
                if (currentWeaponIndex == 0)
                {
                    currentWeaponIndex = weaponLength - 1;
                }
                else
                {
                    currentWeaponIndex--;
                }
                SwitchWeapon(currentWeaponIndex);
                break;


            case "RightButton":
                Debug.Log("right item");
                if (currentWeaponIndex == weaponLength - 1)
                {
                    currentWeaponIndex = 0;
                }
                else
                {
                    currentWeaponIndex++;
                }
                SwitchWeapon(currentWeaponIndex);
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

        spriteRenderer.sprite = weaponImages[currentWeaponIndex];

    }

    public WeaponEnum currentWeapon()
    {
        Debug.Log("here");
        var enum_ = ((WeaponEnum)currentWeaponIndex);
        Debug.Log(enum_.ToString());
        return enum_;
    }

}
