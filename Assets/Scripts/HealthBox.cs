using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : ObjectSpawning
{

    public override void OnTriggerEnter(Collider other)
    {
        // If other is player, then player can pickup health.
    }

}
