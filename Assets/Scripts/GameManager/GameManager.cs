using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public DungeonCreator dungeonCreator;

    [SerializeField] public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        var rooms = dungeonCreator.CreateDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
