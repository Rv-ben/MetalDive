using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// <c>Enemy</c> 
/// This class represents an Enemy and inherits the Character base class
/// </summary>
public class Enemy : Character
{
    /// <summary>
    /// Constructor that inherits the GameObject from the base class with base keyword
    /// </summary>
    /// <param name="prefab">GameObject used to create the Enemy</param>
    public Enemy (GameObject prefab) : base(prefab)
    {
        
    }

    /// <summary>
    /// This function sets the range of which an Enemy can move
    /// </summary>
    /// <param name="moveRange">float value</param>
    public void SetMovementRange(float moveRange)
    {
        this.prefab.GetComponent<EnemyMovement>().walkingRange = moveRange;
    }

    /// <summary>
    /// This function sets the walking speed for the enemy
    /// </summary>
    /// <param name="speed">integer value</param>
    public void SetWalkingSpeed(int speed)
    {
        this.prefab.GetComponent<EnemyMovement>().walkingSpeed = speed;
    }
    
}