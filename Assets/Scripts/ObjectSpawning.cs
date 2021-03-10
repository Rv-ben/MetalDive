using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawning
{
    Vector2 center;
    Vector2 size;
    public abstract void OnTriggerEnter(Collider other);

}
