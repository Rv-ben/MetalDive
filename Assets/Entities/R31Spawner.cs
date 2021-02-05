using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R31Spawner : MonoBehaviour
{
    // Ensures we only aim at the ground and not, like, walls.
    [SerializeField] public LayerMask aimLayer;
    [SerializeField] public GameObject playerModel;

    Vector3 position;
    Quaternion rotation;

    public R31Spawner()
    {

    }

    public Player getPlayer()
    {
        return new Player(aimLayer);
    }
}