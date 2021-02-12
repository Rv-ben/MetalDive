using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// </summary>
public class CustomizationMenu : MonoBehaviour
{

    private Player player;
    
    // available hairstyles
    private List<GameObject> hairstyles = new List<GameObject>();

    // 
    private int currentOption = 0;

    public void UpdateCustomization(GameObject options)         // eventually have 2 params (player's body part, option)
    {   
        // update prefab here
    }


    /// <summary>
    /// 
    /// </summary>
    public void NextItem(GameObject button)
    {
        if (button.name == "LeftButton")
        {
            if (currentOption == 0) // in 1st item in list/index 0, can't go left, start from end index/size-1
            {
                currentOption = hairstyles.Count - 1;
            }
            // update current index to previous
            currentOption--;
            Debug.Log("left button pressed");
            /// 
        }
        else if (button.name == "RightButton")
        {
            // update current index to next
            currentOption++;
            Debug.Log("right button pressed");
        }

        UpdateCustomization(hairstyles[currentOption]);
    }


    /// <summary>
    /// 
    /// </summary>
    public void Save()
    {
        Debug.Log("Saving...");

    }

}
