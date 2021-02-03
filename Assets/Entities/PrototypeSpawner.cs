using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeSpawner
{
    Vector3 playerPosition;
    Quaternion playerRotation;

    public GameObject Spawn(Vector3 playerPosition)
    {
        GameObject player = (GameObject)Resources.Load("Characters/Base/PREFABS/R31");
        player.transform.position = playerPosition;
        player.name = "Test";
        return player;
    }
}
