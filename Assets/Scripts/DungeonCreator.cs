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

        var surfaces = (NavMeshSurface[])FindObjectsOfType(typeof(NavMeshSurface));

        surfaces[0].BuildNavMesh();


        RoomNode firstRoom = list[0];
        int playerHealthMax = 100;
        Vector3 playerPos = new Vector3(firstRoom.topLeft.x + firstRoom.width / 2, 0, firstRoom.topLeft.y + firstRoom.length / 2);
        Vector3 enemyPos = new Vector3(firstRoom.topLeft.x + firstRoom.width / 2, 0, firstRoom.topLeft.y + firstRoom.length / 2);
        Quaternion quaternion = new Quaternion();
        // Spawns a Player at the given coordinates (position, rotation).
        spawner.spawnPlayer(playerPos, quaternion, playerHealthMax);
        // Spawns an Enemy at the given coordinates (position, rotation).
        float walkingRange = 10f;
        int enemyHealthMax = 100;
        spawner.spawnEnemy(enemyPos, quaternion, walkingRange, enemyHealthMax);
        // Spawn an empty object that enemy follows when randomly walking.
        spawner.spawnEnemyTarget(playerPos);
    }

    private void CreateFloor(Node node)
    {

        Vector2 topLeft = node.topLeft;
        Vector2 bottomRight = node.bottomRight;

        Vector3 topLeftV = new Vector3(topLeft.x, 0, topLeft.y);
        Vector3 topRightV = new Vector3(bottomRight.x, 0, topLeft.y);
        Vector3 bottomRightV = new Vector3(bottomRight.x, 0, bottomRight.y);
        Vector3 bottomLeftV = new Vector3(topLeft.x, 0, bottomRight.y);

        Vector3[] vertices = new Vector3[]
        {
            topLeftV,
            topRightV,
            bottomLeftV,
            bottomRightV
        };

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        int[] triangles = new int[]
        {
            0,1,2,2,1,3
        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        GameObject dungeonFloor = new GameObject("RoomFloor"+topLeft, typeof(MeshFilter), typeof(MeshRenderer));

        dungeonFloor.transform.position = Vector3.zero;
        dungeonFloor.transform.localScale = Vector3.one;
        dungeonFloor.GetComponent<MeshFilter>().mesh = mesh;
        dungeonFloor.GetComponent<MeshRenderer>().material = material;

        dungeonFloor.AddComponent<NavMeshSurface>();


        // dungeonFloor.layer = 8;
        // dungeonFloor.AddComponent<MeshCollider>().sharedMesh = aimLayer;
    }
}
