using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomGraph 
{
    public Dictionary<int, List<System.Tuple<int, int, neighborType>>> roomGraph;
    public Queue<RoomNode> roomNodesQueue;
    public List<RoomNode> roomNodes;
    public List<int> reachableRooms;
    public float minCorridorLength;

    public RoomGraph (List<RoomNode> roomNodes, float minCorridorLength)
    {
        this.roomGraph = new Dictionary<int, List<System.Tuple<int, int, neighborType>>>();
        this.roomNodesQueue = new Queue<RoomNode>(roomNodes);
        this.roomNodes = roomNodes;
        this.reachableRooms = new List<int>();
        this.minCorridorLength = minCorridorLength;


        // Make a key for each room node

        for(var i = 0; i < roomNodes.Count; i++ )
        {
            roomGraph.Add(i, new List<System.Tuple<int, int, neighborType>>() );
        }
    }

    public neighborType IsNeighborNode (RoomNode node1, RoomNode node2)
    {
        Vector2 topLeftNode1 = node1.topLeft;

        bool xNeighbor = IsInArea(new Vector2(topLeftNode1.x + 4.5f, topLeftNode1.y), node2);
        bool yNeighbor = IsInArea(new Vector2(topLeftNode1.x , topLeftNode1.y + 4.5f), node2);

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

    public bool IsInArea (Vector2 point, RoomNode area)
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
                neighborType type = IsNeighborNode(currentNode, potentialNeighbor);

                if (type != neighborType._null)
                {
                    // Make a key and a random weight
                    int neigborKey = roomNodes.IndexOf(potentialNeighbor);
                    int randomWeight = Random.Range(0, 3);

                    // Add the neighbor and random weight
                    roomGraph[cnKey].Add(new System.Tuple<int, int, neighborType>(neigborKey, randomWeight, type));

                    
                    roomGraph[neigborKey].Add(new System.Tuple<int, int, neighborType>(cnKey, randomWeight, type));

                }
            }

        }
        return roomGraph;
    }

    public List<int> RandomWalk(int startNode, int steps)
    {
        List<int> path = new List<int>();

        int currentNode = startNode;

        for (var i = 0; i < steps; i++)
        {
            path.Add(currentNode);
            this.reachableRooms.Add(currentNode);

            var possibleChoices = GetPossibleChoices(currentNode, path);

            if(possibleChoices.Count == 0)
            {
                break;
            }

            int randomChoice = Random.Range(0, possibleChoices.Count);
            currentNode = possibleChoices[randomChoice];

        }

        //path.Add(currentNode);
        //this.reachableRooms.Add(currentNode);

        return path;

    }

    public List<int> RandomWalkUntilDead(int startNode)
    {
        List<int> path = new List<int>();

        int currentNode = startNode;

        do
        {
            path.Add(currentNode);
            this.reachableRooms.Add(currentNode);

            var possibleChoices = GetPossibleChoices(currentNode, path);

            if (possibleChoices.Count == 0)
            {
                break;
            }

            if (this.roomNodes.Count == this.reachableRooms.Count)
            {
                break;
            }

            int randomChoice = Random.Range(0, possibleChoices.Count);
            currentNode = possibleChoices[randomChoice];

            if (this.reachableRooms.Contains(currentNode))
            {
                path.Add(currentNode);
                this.reachableRooms.Add(currentNode);
                break;
            }

        } while (true);

        return path;
    }

    public List<int> GetPossibleChoices(int node, List<int> path)
    {
        List<int> possibleChoices = new List<int>();
        List<int> reachAble = new List<int>();
        roomGraph[node].ForEach(delegate (System.Tuple<int, int, neighborType> choice)
        {
            if (!path.Contains(choice.Item1))
            {
                if (this.reachableRooms.Contains(choice.Item1))
                {
                    reachAble.Add(choice.Item1);
                }
                possibleChoices.Add(choice.Item1);
            }
        });

        if (reachAble.Count > 0)
        {
            return reachAble;
        }

        return possibleChoices;
    }

    public List<CorridorNode> generateCorridors()
    {
        List<CorridorNode> corridors = new List<CorridorNode>();

        corridors.AddRange(getCorridors(RandomWalk(0, 11)));

        for(var i = 0; i < roomNodes.Count; i++)
        {
            if (!reachableRooms.Contains(i))
            {
                //corridors.AddRange(getCorridors(RandomWalkUntilDead(i)));
            }
        }

        return corridors;
    }

    public List<CorridorNode> getCorridors(List<int> path)
    {
        List <CorridorNode> corridors = new List<CorridorNode>();

        for(var i = 0; i < path.Count - 1; i++)
        {
            corridors.Add(GetCorridorNode(path[i], path[i + 1]));
        }

        return corridors;
    }

    public CorridorNode GetCorridorNode(int node1Key, int node2Key)
    {
        RoomNode node1 = roomNodes[node1Key];
        RoomNode node2 = roomNodes[node2Key];
        neighborType neighborTypeN1N2 = GetNeighborType(node1Key, node2Key);

        float lowerBound;
        float upperBound;

        Vector2 corridorTopLeft = new Vector2(0,0);
        Vector2 corridorBottomRight = new Vector2(0, 0);

        if (neighborType.xNeighbor == neighborTypeN1N2)
        {
            lowerBound = GetLowerBound(node1.topLeft.y, node2.topLeft.y);
            upperBound = GetUpperBound(node1.topLeft.y, node2.topLeft.y, node1.length, node2.length);

            corridorTopLeft = new Vector2( node1.topLeft.x + node1.width, Random.Range(lowerBound, upperBound - this.minCorridorLength));
            corridorBottomRight = new Vector2( node2.topLeft.x , corridorTopLeft.y + this.minCorridorLength);
        }
        else if(neighborType.yNeighbor == neighborTypeN1N2)
        {
            lowerBound = GetLowerBound(node1.topLeft.x, node2.topLeft.x);
            upperBound = GetUpperBound(node1.topLeft.x, node2.topLeft.x, node1.width, node2.width);
             
            corridorTopLeft = new Vector2( Random.Range( lowerBound, upperBound - this.minCorridorLength) , node1.topLeft.y + node1.length );
            corridorBottomRight = new Vector2( corridorTopLeft.x + this.minCorridorLength, node2.topLeft.y);
        }

        return new CorridorNode(corridorTopLeft, corridorBottomRight);
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
        if (position1 + distance1 >= position1 + distance2)
            return position2 + distance2;
        else
            return position1 + distance1;
    }

    public neighborType GetNeighborType(int node1Key, int node2Key)
    {
        var listOfEdges = roomGraph[node1Key];
        neighborType neighborTypeN1N2 = neighborType._null;

        foreach(System.Tuple<int, int, neighborType> edge in listOfEdges)
        {
            if(edge.Item1 == node2Key)
            {
                neighborTypeN1N2 = edge.Item3;
                break;
            }
        }

        return neighborTypeN1N2;
    }

    public float Distance(int room1, int room2)
    {
        return (this.roomNodes[room1].topLeft - this.roomNodes[room2].topLeft).magnitude;
    } 
}

public enum neighborType
{
    xNeighbor = 0,
    yNeighbor = 1,
    _null = -1
}
