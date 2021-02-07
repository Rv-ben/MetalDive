﻿using System.Collections;
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

    [SerializeField] public GenericSpawner spawner;

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
        var rootNode = generator.CalculateRooms(maxIterations, roomWidthMin, roomLengthMin);

        List<RoomNode> list = generator.GetListOfRooms();

        foreach(RoomNode roomNode in list)
        {
            CreateFloor(roomNode);
        }

        Vector3 playerPos = new Vector3(list[0].topLeft.x, 0, list[0].topLeft.y);
        Quaternion quaternion = new Quaternion();
        spawner.Spawn(playerPos, quaternion);
    }

    private void CreateFloor(RoomNode node)
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
    }
}
