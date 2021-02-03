using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeSpawner : MonoBehaviour
{
    [SerializeField] Player playerPrefab;
    Vector3 playerPosition;
    Quaternion playerRotation;

    private void Spawn(Vector3 playerPosition, Quaternion playerRotation)
    {
        Player player = Instantiate(playerPrefab, playerPosition, playerRotation);
    }
}
