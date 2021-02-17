using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGraphHelper 
{
    private readonly Dictionary<int, List<System.Tuple<int, int, neighborType>>> roomGraph;

    public RoomGraphHelper (Dictionary<int, List<System.Tuple<int, int, neighborType>>> graph)
    {
        this.roomGraph = graph;
    }

    public float GetLowerBound(float position1, float position2)
    {
        if (position1 >= position2)
            return position2;
        else
            return position1;

    }

    public float GetUpperBound(float position1, float position2, float distance1, float distance2)
    {
        if (position1 + distance1 >= position2 + distance2)
            return position2 + distance2;
        else
            return position1 + distance1;
    }

    public System.Tuple<RoomNode, RoomNode> GetXOrientation(RoomNode node1, RoomNode node2)
    {
        if (node1.topLeft.x < node2.topLeft.x)
            return System.Tuple.Create(node1, node2);
        else
            return System.Tuple.Create(node2, node1);
    }

    public System.Tuple<RoomNode, RoomNode> GetYOrientation(RoomNode node1, RoomNode node2)
    {
        if (node1.topLeft.y < node2.topLeft.y)
            return System.Tuple.Create(node1, node2);
        else
            return System.Tuple.Create(node2, node1);
    }

    public neighborType GetNeighborType(int node1Key, int node2Key)
    {
        var listOfEdges = roomGraph[node1Key];
        neighborType neighborTypeN1N2 = neighborType._null;

        foreach (System.Tuple<int, int, neighborType> edge in listOfEdges)
        {
            if (edge.Item1 == node2Key)
            {
                neighborTypeN1N2 = edge.Item3;
                break;
            }
        }

        return neighborTypeN1N2;
    }

    public neighborType IsNeighborNode(RoomNode node1, RoomNode node2)
    {
        Vector2 topLeftNode1 = node1.topLeft;

        bool xNeighbor = IsInArea(new Vector2(topLeftNode1.x + node1.width + 2f, topLeftNode1.y + node1.length / 2), node2);
        bool yNeighbor = IsInArea(new Vector2(topLeftNode1.x + node1.width / 2, topLeftNode1.y + node1.length + 2f), node2);

        if (xNeighbor)
        {
            return neighborType.xNeighbor;
        }
        else if (yNeighbor)
        {
            return neighborType.yNeighbor;
        }

        return neighborType._null;
    }

    public bool IsInArea(Vector2 point, RoomNode area)
    {
        bool xStatus = point.x >= area.topLeft.x && point.x <= area.topLeft.x + area.width;
        bool yStatus = point.y >= area.topLeft.y && point.y <= area.topLeft.y + area.length;
        return xStatus && yStatus;
    }
}

public enum neighborType
{
    xNeighbor = 0,
    yNeighbor = 1,
    _null = -1
}