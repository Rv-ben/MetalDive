using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceedNextLevel : MonoBehaviour
{
    private List<string> FloorObjects;
    /// <summary>
    /// Initialize all the variables with objects upon game starts.
    /// </summary>
    void Start()
    {
        FloorObjects = new List<string>(){"Barrier(Clone)", "Bitcoin(Clone)", "Healthkit(Clone)", "Elevator(Clone)", "Wall", "Floor", "Player", "Enemy", "Enemy(Clone)"};
    }

    /// <summary>
    /// Destroys all the object that have the same name as saved in FloorObjects.
    /// </summary>
    public void DestroyEnvironment()
    {
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (FloorObjects.Contains(obj.name))
            {
                Destroy(obj);
            }
        }
    }
}
