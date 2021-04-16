using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Spawner spawner;
    public List<RoomNode> rooms;
    public int difficulty;
    public List<int> indexes;
    static int enemyCount;

    /// <summary>
    /// EnemySpawner constructor
    /// </summary>
    /// <param name="spawner">spawner</param>
    /// <param name="rooms">list of RoomNode objects</param>
    /// <param name="difficulty">difficulty of level</param>
    public EnemySpawner(Spawner spawner, List<RoomNode> rooms, int difficulty)
    {
        this.spawner = spawner;
        this.rooms = rooms;
        this.difficulty = difficulty;
        this.indexes = GetRoomIndexes();
        enemyCount = 0;
    }

    /// <summary>
    /// This function spawns enemies at random positions in specific RoomNodes
    /// </summary>
    /// <returns>total enemies spawned</returns>
    public int SpawnEnemies()
    {
        for (int i = 0; i < this.rooms.Count; i++)
        {
            if (this.indexes.Contains(i))
            {
                spawner.SpawnCharacter(rooms[i].GetRandomPosition(rooms[i]), CharacterEnum.Enemy);
                ++enemyCount;
            }
        }

        return enemyCount;
    }

    /// <summary>
    /// This function calculates how many enemies will be spawned on the level given a difficulty
    /// </summary>
    /// <param name="difficulty">integer value</param>
    /// <returns>integer value of total enemies on a level</returns>
    public int GetEnemyCount(int difficulty)
    {
        return difficulty * 5;
    }

    /// <summary>
    /// This function selects random values that represent room indexes in the range 1-total number of rooms
    /// </summary>
    /// <returns>indexes - list of integers</returns>
    public List<int> GetRoomIndexes()
    {
        int randRoom;

        // get the total number of enemies to place on the level
        int enemyCount = GetEnemyCount(this.difficulty);

        List<int> indexes = new List<int>();

        // generate the room indexes for the exact amount of enemies
        for (int i = 0; i < enemyCount; i++)
        {
            randRoom = UnityEngine.Random.Range(1, rooms.Count + 1); // generate random room number

            indexes.Add(randRoom);  // add room index to list
        }

        return indexes;
    }

}