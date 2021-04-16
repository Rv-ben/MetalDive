using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class WeaponSwitchMenu : MonoBehaviour
{
    [NonSerialized]
    private readonly List<GameObject> weapons;

    [NonSerialized]
    public int weaponIndex = 0;

    public GameObject weapon;

    public GameObject WeaponView;

    public GameObject MenuCanvas;

    private void Start()
    {
        MenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            PauseGame();
            DisplayMenu();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        MenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// 
    /// </summary>
    private void DisplayMenu()
    {
        MenuCanvas.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public void NextItem(GameObject button)
    {
        string buttonName = button.name;
        switch(buttonName)
        {
            case "LeftButton":
                Debug.Log("left item");
                if (weaponIndex == 0)
                {
                    weaponIndex = weapons.Count - 1;
                }
                else
                {
                    weaponIndex--;
                }
                SwitchWeapon(weaponIndex);
                break;


            case "RightButton":
                Debug.Log("right item");
                if (weaponIndex == weapons.Count - 1)
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

       // return currentWeaponIndex;
    } 

    public void SwitchWeapon(int index)
    {
        foreach (Transform weapon in transform)
        {
            int i = 0;
            if (i == index)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void LoadWeapon()
    {
        WeaponView.SendMessage("SwitchWeapon", weapons[weaponIndex]);
    }


}
