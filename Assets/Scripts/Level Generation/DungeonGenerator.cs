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

    public List<RoomNode> GetRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        BinarySpace bsp = new BinarySpace(this.dungeonWidth, this.dungeonLength, roomWidthMin, roomLengthMin);
        RoomNode root = bsp.rootNode;
        bsp.PartionSpace(root);

        RoomTree tree = new RoomTree(root);
        List<RoomNode> listOfRooms = tree.GetLeaves();
        shrinkRooms(listOfRooms);
        this.listOfRooms = listOfRooms;

        return listOfRooms;
    }

    public List<CorridorNode> GetCorridors(float minCorridorLength)
    {
        RoomGraph graph = new RoomGraph(this.listOfRooms, minCorridorLength);

        List<CorridorNode> c = graph.generateCorridors();

        return c;
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

    public List<Wall> GenerateWalls (List<RoomNode> listOfRoomNodes)
    {
        var walls = new List<Wall>();
        for (int i = 0; i < listOfRoomNodes.Count; i++) 
        {   
            var room = listOfRoomNodes[i];
            walls.AddRange(GetWallsInRoom(room));
        }

        

        return walls;
    }

    public List<Wall> GetWallsInRoom (RoomNode room)
    {
        List<List<CorridorNode>> sides = room.sides;
        var wallsInRoom = new List<Wall>();

       for (int i = 0; i < sides.Count; i++)
       {
           var side = sides[i];

           if (i < 2) 
           {    
               var top = true;
               if (i == 1) {top = false;}
               wallsInRoom.AddRange(GetHorizontalWalls(room, side, top));
           }
           else
           {    
               var left = true;
               if (i == 3) {left = false;}
               wallsInRoom.AddRange(GetVerticalWalls(room, side, left));
           }
       }

       return wallsInRoom;
    }

    public List<Wall> GetHorizontalWalls (RoomNode room, List<CorridorNode> corridorsOnSide, bool top)
    {
        float leftMostXValue = room.topLeft.x;
        float yValue = top ? room.topLeft.y : room.bottomRight.y;
        var wallsOnSide = new List<Wall>();

        Vector2 startPoint;
        Vector2 endPoint;
        float lengthOfWall;

        for (int i = 0; i < corridorsOnSide.Count; i++) 
        {
            CorridorNode currentCorridor = corridorsOnSide[i];
            startPoint = new Vector2(leftMostXValue, yValue);
            lengthOfWall = currentCorridor.topLeft.x - leftMostXValue;

            wallsOnSide.Add(new Wall(startPoint, lengthOfWall, Orientation.Horizontal));

            leftMostXValue = currentCorridor.bottomRight.x + currentCorridor.width;
        }

        startPoint = new Vector2(leftMostXValue, yValue);
        lengthOfWall = room.bottomRight.x - leftMostXValue;
        endPoint = new Vector2(leftMostXValue + lengthOfWall, yValue);

        wallsOnSide.Add(new Wall(startPoint, lengthOfWall, Orientation.Horizontal));

        return wallsOnSide;
    }

    public List<Wall> GetVerticalWalls (RoomNode room, List<CorridorNode> corridorsOnSide, bool left)
    {
        float topMostYValue = room.topLeft.y;
        float xValue = left ? room.topLeft.x : room.bottomRight.x;
        var wallsOnSide = new List<Wall>();

        Vector2 startPoint;
        Vector2 endPoint;
        float lengthOfWall;

        for (int i = 0; i < corridorsOnSide.Count; i++) 
        {
            CorridorNode currentCorridor = corridorsOnSide[i];
            startPoint = new Vector2(xValue, topMostYValue);
            lengthOfWall = currentCorridor.topLeft.y - topMostYValue;

            endPoint = new Vector2(xValue, topMostYValue + lengthOfWall);

            wallsOnSide.Add(new Wall(startPoint, lengthOfWall, Orientation.Vertical));

            topMostYValue = currentCorridor.bottomRight.y + currentCorridor.length;
        }

        startPoint = new Vector2(xValue, topMostYValue);
        lengthOfWall = room.bottomRight.y - topMostYValue;

        endPoint = new Vector2(xValue, topMostYValue + lengthOfWall);

        wallsOnSide.Add(new Wall(startPoint, lengthOfWall, Orientation.Vertical));

        return wallsOnSide;
    }

    public List<Wall> GetWallsFromCorridor(CorridorNode corridor)
    {
        var walls = new List<Wall>();


        return walls;
    }
}