using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGraph 
{
    public Dictionary<int, List<System.Tuple<int, int, neighborType>>> roomGraph;
    public Queue<RoomNode> roomNodesQueue;
    public List<RoomNode> roomNodes;

    public RoomGraph (List<RoomNode> roomNodes)
    {
        this.roomGraph = new Dictionary<int, List<System.Tuple<int, int, neighborType>>>();
        this.roomNodesQueue = new Queue<RoomNode>(roomNodes);
        this.roomNodes = roomNodes;


        // Make a key for each room node

        for(var i = 0; i < roomNodes.Count; i++ )
        {
            roomGraph.Add(i, new List<System.Tuple<int, int, neighborType>>() );
        }
    }

    public neighborType isNeighborNode (RoomNode node1, RoomNode node2)
    {
        Vector2 topLeftNode1 = node1.topLeft;

        bool xNeighbor = isInArea(new Vector2(topLeftNode1.x + 5, topLeftNode1.y), node2);
        bool yNeighbor = isInArea(new Vector2(topLeftNode1.x , topLeftNode1.y + 5), node2);

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

    public bool isInArea (Vector2 point, RoomNode area)
    {
        bool xStatus = point.x >= area.topLeft.x && point.x <= area.topLeft.x + area.width;
        bool yStatus = point.y >= area.topLeft.y && point.y <= area.topLeft.y + area.length;
        return xStatus && yStatus;
    }

    public Dictionary<int, List<System.Tuple<int, int, neighborType>>> generateGraph()
    {

        for (var i = 0; i < roomNodes.Count; i++)
        {
            RoomNode currentNode = roomNodes[i];

            // Make search size smaller
            roomNodesQueue.Dequeue();
            int cnKey = i;
            
            foreach (RoomNode potentialNeighbor in roomNodesQueue)
            {
                neighborType type = isNeighborNode(currentNode, potentialNeighbor);

                if (type != neighborType._null)
                {
                    // Make a key and a random weight
                    int neigborKey = roomNodes.IndexOf(potentialNeighbor);
                    int randomWeight = Random.Range(0, 100);

                    // Add the neighbor and random weight
                    roomGraph[cnKey].Add(new System.Tuple<int, int, neighborType>(neigborKey, randomWeight, type));

                    
                    roomGraph[neigborKey].Add(new System.Tuple<int, int, neighborType>(cnKey, randomWeight, type));

                }
            }

        }
        return roomGraph;
    }

    public enum neighborType
    {
        xNeighbor = 0,
        yNeighbor = 1,
        _null = -1
    }

}
