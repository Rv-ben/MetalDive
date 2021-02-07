using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// </summary>
public class CustomizationMenu : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public void NextItem(GameObject button)
    {
        Debug.Log("button pressed: " + button.name);
        if (button.name == "LeftButton")
        {
            Debug.Log("left button pressed");
            /// 
        }
        else if (button.name == "RightButton")
        {
            Debug.Log("right button pressed");
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void Save()
    {
        Debug.Log("Saving...");

    }

}
