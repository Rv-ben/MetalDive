using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            CreateFloor(roomNode);
        }

        foreach(CorridorNode corridoNode in listOfCooridors)
        {
            CreateFloor(corridoNode);
        }



        RoomNode firstRoom = list[0];
        Vector3 playerPos = new Vector3(firstRoom.topLeft.x + firstRoom.width / 2, 5, firstRoom.topLeft.y + firstRoom.length / 2);
        Vector3 enemyPos = new Vector3(1, 5, firstRoom.topLeft.y + 0);
        int playerHealthMax = 100;
        Quaternion quaternion = new Quaternion();
        // Spawns a Player at the given coordinates (position, rotation).
        spawner.spawnPlayer(playerPos, quaternion, playerHealthMax);      // <---------------------------------- added healthMax(int)
        // Spawns an Enemy at the given coordinates (position, rotation).
        float walkingRange = 10f;
        int enemyHealthMax = 100;
        spawner.spawnEnemy(enemyPos, quaternion, walkingRange, enemyHealthMax);  // <---------------------------------- added healthMax(int)

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
        // dungeonFloor.layer = 8;
        // dungeonFloor.AddComponent<MeshCollider>().sharedMesh = aimLayer;
    }
}
