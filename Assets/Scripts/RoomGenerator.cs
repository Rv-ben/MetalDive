using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator
{
    private int maxIterations;
    private int roomLengthMin;
    private int roomWidthMin;

    public RoomGenerator(int maxIterations, int roomLengthMin, int roomWidthMin)
    {
        this.maxIterations = maxIterations;
        this.roomLengthMin = roomLengthMin;
        this.roomWidthMin = roomWidthMin;
    }

    public List<RoomNode> generateRoomsInGivenSpaces(List<Node> roomSpaces)
    {
        List<RoomNode> listToReturn = new List<RoomNode>();

        foreach(var space in roomSpaces)
        {
            Vector2Int newBottomLeftPoint = StructureHelper.GenerateBottomLeftCornerBetween(
                space.bottomLeftAreaCorner, space.topRightAreaCorner, 0.1f, 1);

            Vector2Int newTopRightPoint = StructureHelper.GenerateTopRightCornerBetween(
                space.bottomLeftAreaCorner, space.topLeftAreaCorner, 0.9f, 1);

            space.bottomLeftAreaCorner = newBottomLeftPoint;
            space.topLeftAreaCorner = newTopRightPoint;
            space.bottomRightAreaCorner = new Vector2Int(newTopRightPoint.x,newBottomLeftPoint.y);
            space.topLeftAreaCorner = new Vector2Int(newBottomLeftPoint.x, newTopRightPoint.y);
            listToReturn.Add((RoomNode)space);
        }
        return listToReturn;
    }
}