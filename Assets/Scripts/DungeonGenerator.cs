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

    public RoomNode CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        BinarySpace bsp = new BinarySpace(this.dungeonWidth, this.dungeonLength, roomWidthMin, roomLengthMin);
        RoomNode root = bsp.rootNode;
        bsp.PartionSpace(root);
        return root;
    }
}