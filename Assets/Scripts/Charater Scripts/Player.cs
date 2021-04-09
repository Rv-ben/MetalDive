using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>Player</c>
/// This class represents a Player and inherits the Character base class
/// </summary>
public class Player : Character
{
    public float movementSpeed;
    public GameObject[] weapons;

    /// <summary>
    /// Constructor that inherits the GameObject from the base class with base keyword
    /// </summary>
    /// <param name="prefab">GameObject used to create the Player</param>
    public Player (GameObject prefab) : base(prefab)
    {

    }

    /// <summary>
    /// This function sets the movement speed of a player
    /// </summary>
    /// <param name="movementSpeed">float value</param>
    public void SetMovementSpeed(float movementSpeed)
    {
        this.prefab.GetComponent<PlayerMovement>().movementSpeed = movementSpeed;
    }


}
