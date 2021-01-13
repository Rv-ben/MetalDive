using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    RoomNode rootNode;
    List<RoomNode> allSpaceNodes = new List<RoomNode>();
    private int dungeonWidth, dungeonLength;

    public DungeonGenerator(int dungeonWidth, int dungeonLength)
    {
        this.dungeonLength = dungeonLength;
        this.dungeonWidth = dungeonWidth;
    }

    internal object CalculateRooms(int maxIterations, int roomWidth, int roomLengthMin)
    {

    }
}
