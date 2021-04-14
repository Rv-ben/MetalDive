using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class that represents a Character
/// </summary>
public class Character
{
    public GameObject prefab;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="prefab">GameObject to be used for the character</param>
    public Character(GameObject prefab)
    {
        this.prefab = prefab;
    }

    /// <summary>
    /// Sets the maximum possible value for the health slider
    /// </summary>
    /// <param name="health"> positive integer value</param>
    public void SetMaxHealth(int maxHealth)
    {
        this.prefab.GetComponent<HealthSlider>().maxHealth = maxHealth;
    }


}
