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

    /// <summary>
    /// method <c>CreateDungeon</c>
    /// Creates mesh for calculated rooms
    /// </summary>
    public List<RoomNode> CreateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator(dunWidth, dunLength);

        List<RoomNode> list = generator.GetRooms(maxIterations, roomWidthMin, roomLengthMin);
        List<CorridorNode> listOfCooridors = generator.GetCorridors(corridorWidth);

        var rooms = new List<GameObject>();

        foreach(RoomNode roomNode in list)
        {
            var room = ProceduralPrimitives.Primitive.CreatePlaneGameObject(roomNode.width, roomNode.length, 2, 2);
            room.transform.position = new Vector3(roomNode.topLeft.x + roomNode.width/2, 0, roomNode.topLeft.y + roomNode.length/2);
            room.AddComponent<NavMeshSurface>();
            rooms.Add(room);
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

        return list;

    }
}
