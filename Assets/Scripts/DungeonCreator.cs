using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using ProceduralPrimitives;

/// <summary>
/// Class <c>DungeonCreator</c>
/// </summary>
public class DungeonCreator : MonoBehaviour
{
    public static Transform targetPoint;


    public int dunWidth, dunLength;
    public int roomWidthMin, roomLengthMin;
    public int maxIterations;
    public int corridorWidth;
    public Material material;

    public Mesh aimLayer;

    public NavMeshSurface surface;

    // Spawns Prefab Entities.
    [SerializeField] public EntitySpawner spawner;

    /// <summary>
    /// method <c>Start</c>
    /// Starts the creation of a dungeon
    /// </summary>
    void Start()
    {
        CreateDungeon();
    }

    /// <summary>
    /// method <c>CreateDungeon</c>
    /// Creates mesh for calculated rooms
    /// </summary>
    private void CreateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator(dunWidth, dunLength);

        List<RoomNode> list = generator.GetRooms(maxIterations, roomWidthMin, roomLengthMin);
        List<CorridorNode> listOfCooridors = generator.GetCorridors(corridorWidth);

        foreach(RoomNode roomNode in list)
        {
            var room = ProceduralPrimitives.Primitive.CreatePlaneGameObject(roomNode.width, roomNode.length, 2, 2);
            room.transform.position = new Vector3(roomNode.topLeft.x + roomNode.width/2, 0, roomNode.topLeft.y + roomNode.length/2);
            room.AddComponent<NavMeshSurface>();
        }

        foreach(CorridorNode corridorNode in listOfCooridors)
        {
            //CreateFloor(corridorNode);
            corridorNode.CalculateLength();
            corridorNode.CalulateWidth();
            var corridor = ProceduralPrimitives.Primitive.CreatePlaneGameObject(corridorNode.width, corridorNode.length, 1, 1);
            corridor.transform.position = new Vector3(corridorNode.topLeft.x + corridorNode.width / 2, 0, corridorNode.topLeft.y + corridorNode.length / 2);
            corridor.AddComponent<NavMeshSurface>();
        }

        RoomNode firstRoom = list[0];
        int playerHealthMax = 100;
        Vector3 playerPos = new Vector3(firstRoom.topLeft.x + firstRoom.width / 2, 0, firstRoom.topLeft.y + firstRoom.length / 2);
        // Spawns a Player at the given coordinates (position, rotation).
        Quaternion quaternion = new Quaternion();
        spawner.spawnPlayer(playerPos, quaternion, playerHealthMax);

        SpawnEnemies(list);

        // Spawn an empty object that enemy follows when randomly walking.

        var surfaces = (NavMeshSurface[])FindObjectsOfType(typeof(NavMeshSurface));

        surfaces[0].BuildNavMesh();
    }

    public void SpawnEnemies(List<RoomNode> rooms)
    {
        Quaternion quaternion = new Quaternion();
        for (var i = 1; i < rooms.Count; i++)
        {
            Vector3 enemyPos = new Vector3(rooms[i].topLeft.x + rooms[i].width / 2, 0, rooms[i].topLeft.y + rooms[i].length / 2);
            float walkingRange = 10f;
            int enemyHealthMax = 100;
            // Spawns an Enemy at the given coordinates (position, rotation).
            spawner.spawnEnemy(enemyPos, quaternion, walkingRange, enemyHealthMax);
            spawner.spawnEnemyTarget(enemyPos);
        }
    }
}
