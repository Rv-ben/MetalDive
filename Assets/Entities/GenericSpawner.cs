using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawner : MonoBehaviour
{
    [SerializeField] public GameObject prefab;

    public void Spawn(Vector3 position, Quaternion rot)
    {
        GameObject ob = Instantiate(prefab, position, rot);        
    }
}