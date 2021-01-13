using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    List<RoomNode> allSpaceNodes = new List<RoomNode>();
    private int dungeonWidth, dungeonLength;

    public DungeonGenerator(int dungeonWidth, int dungeonLength)
    {
        this.dungeonLength = dungeonLength;
        this.dungeonWidth = dungeonWidth;
    }

    public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(dungeonWidth, dungeonLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
    }
}
