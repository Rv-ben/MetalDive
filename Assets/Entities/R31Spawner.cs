using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R31Spawner : MonoBehaviour
{
    [SerializeField] public Player playerPrefab;

    public void Spawn(Vector3 position, Quaternion rot)
    {
        Player player = Instantiate(playerPrefab, position, rot);
        // FIgure out how to unpack it.
    }
}