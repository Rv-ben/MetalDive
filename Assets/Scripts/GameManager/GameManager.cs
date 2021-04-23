using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public DungeonCreator dungeonCreator;

    [SerializeField] public Spawner spawner;
    
    public ObstacleGeneration obstacleGenerator;

    // Start is called before the first frame update
    void Awake()
    {
        spawner.LoadPrefabs();
        var rooms = dungeonCreator.CreateDungeon();
        var obstacleGenerator = new ObstacleGeneration(rooms, spawner);
        var player = new Player(rooms[0], spawner);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
