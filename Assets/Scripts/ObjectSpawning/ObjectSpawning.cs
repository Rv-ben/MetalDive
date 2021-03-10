using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawning
{
    Vector2 center;
    Vector2 size;
    public abstract void OnTriggerEnter(Collider other);

}

public class HealthBox : ObjectSpawning
{

    HealthBox(Vector2 center, Vector2 size) 
    {
    }
    public override void OnTriggerEnter(Collider other)
    {
        // If other is player, then player can pickup health.
    }

}