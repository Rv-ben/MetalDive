using System;
using System.Collections.Generic;
using UnityEngine;


public class DungeonGenerator
{
    List<RoomNode> listOfRooms = new List<RoomNode>();
    private int dungeonWidth, dungeonLength;

    public DungeonGenerator(int dungeonWidth, int dungeonLength)
    {
        this.dungeonLength = dungeonLength;
        this.dungeonWidth = dungeonWidth;
    }

    public RoomNode CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        BinarySpace bsp = new BinarySpace(this.dungeonWidth, this.dungeonLength, roomWidthMin, roomLengthMin);
        RoomNode root = bsp.rootNode;
        bsp.PartionSpace(root);

        RoomTree tree = new RoomTree(root);
        List<RoomNode> listOfRooms = tree.GetLeaves();
        shrinkRooms(listOfRooms);
        this.listOfRooms = listOfRooms;

        RoomGraph graph = new RoomGraph(listOfRooms);
        Dictionary<int, List<System.Tuple<int, int, RoomGraph.neighborType>>>  g = graph.generateGraph();

        return root;
    }

    public void shrinkRooms(List<RoomNode> listOfRooms)
    {
        foreach (RoomNode roomNode in listOfRooms){
            float widthPercentage = UnityEngine.Random.Range(0.07f, 0.15f);
            float lengthPercentage = UnityEngine.Random.Range(0.07f, 0.15f);
            roomNode.Shrink(widthPercentage, lengthPercentage);
        }
    }

    public List<RoomNode> GetListOfRooms()
    {
        return this.listOfRooms;
    }
}