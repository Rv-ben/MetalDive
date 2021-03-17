﻿using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]

/// <summary>
/// Handle trigger. Enemy follows player while player is in a certain distance 
/// </summary>

public class EnemyTrigger : MonoBehaviour
{

    [SerializeField] private TriggerEvent _stay = new TriggerEvent();

    private SphereCollider sphereCollider;
    private Enemy enemy;

    /// <summary>
    /// Initialize radius centered enemy.
    /// Enemy will follow once player touches collider.
    /// </summary>
    void Start() {
        enemy = GetComponent<Enemy>();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = 1.0f;
    }

    /// <summary>
    /// Event function that triggers by colliding objects.
    /// If passed collider is tagged as "Player", then enemy will start follow.
    /// </summary>
    /// <param name="other">object that touched this collider</param>
    void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player")) {
            _stay.Invoke(other);
        }
    }

    /// <summary>
    /// Collision event will be called once a specified object touches the collider.
    /// </summary>
    /// <remarks>
    /// To set up this collision event behavior on Inspectot:
    ///     Add a relation in 'Stay(Collider)'
    ///     Drag and add Enemy object
    ///     Once set an object, Function should be able to select on the right.
    ///     Scroll to 'EnemyMovement.cs' and choose 'FollowPlayer' function.
    /// </remarks>
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }
}