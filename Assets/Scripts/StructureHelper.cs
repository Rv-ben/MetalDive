using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StructureHelper
{
    public static List<Node> TraverseGraphToExtractLowestLeafes(RoomNode parentNode)
    {
        Queue<Node> nodesToCheck = new Queue<Node>();
        List<Node> listToReturn = new List<Node>();

        if(parentNode.ChildrenNodeList.Count == 0)
        {
            return new List<Node>() { parentNode };
        }

        foreach( var child in parentNode.ChildrenNodeList)
        {
            nodesToCheck.Enqueue(child);
        }
        while(nodesToCheck.Count > 0)
        {
            var currentNode = nodesToCheck.Dequeue();
            if (currentNode.ChildrenNodeList.Count == 0)
            {
                listToReturn.Add(currentNode);
            }
            else
            {
                foreach (var child in currentNode.ChildrenNodeList)
                {
                    nodesToCheck.Enqueue(child);
                }
            }
        }

        return listToReturn;
    }

    public static Vector2Int GenerateBottomLeftCornerBetween(
        Vector2Int boundryLeftPoint, Vector2Int boundryRightPoint, float pointModifier, int offset)
    {
        int minX = boundryLeftPoint.x + offset;
        int maxX = boundryRightPoint.x - offset;
        int minY = boundryLeftPoint.y + offset;
        int maxY = boundryRightPoint.y - offset;

        return new Vector2Int(
            Random.Range(minX, (int)(minX + (maxX - minX) * pointModifier)),
            Random.Range(minY, (int)(minY + (maxY - minY) * pointModifier))
            );
    }

    public static Vector2Int GenerateTopRightCornerBetween(
        Vector2Int boundryLeftPoint, Vector2Int boundryRightPoint, float pointModifier, int offset)
    {
        int minX = boundryLeftPoint.x + offset;
        int maxX = boundryRightPoint.x - offset;
        int minY = boundryLeftPoint.y + offset;
        int maxY = boundryRightPoint.y - offset;

        return new Vector2Int(
            Random.Range((int)(minX+(maxX-minX)*pointModifier),maxX) ,
            Random.Range((int)(minY+(maxY-minY)*pointModifier),maxY)
            );
    }
}