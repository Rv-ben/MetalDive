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
    public GameObject weapon;

    public GameObject weaponView;

    private static bool isPaused = false;

    private int weaponIndex = 0;

    private GameObject menuCanvas;

    private Image currentImage;

    private int weaponLength = Enum.GetNames(typeof(WeaponEnum)).Length;

    private Image[] weaponImages;

    private void Start()
    {
        this.menuCanvas = GameObject.Find("Panel");
        menuCanvas.SetActive(isPaused);
        weaponImages = Resources.LoadAll<Image>("Weapons/Icons");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Screen.lockCursor = false;
            if (isPaused) 
            {
                Debug.Log("Tab key entered should resume");
                ResumeGame();
            }
            else
            {
                Debug.Log("Tab key entered should be paused");
                PauseGame();
            }
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    private void PauseGame()
    {
        menuCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        LoadWeapon();
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public void NextItem(GameObject button)
    {
        string buttonName = button.name;
        switch (buttonName)
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

    public bool accessIsPaused() 
    {
        return isPaused;
     }


}
