using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomGraph 
{
    public Dictionary<int, List<System.Tuple<int, int, neighborType>>> roomGraph;
    public RoomGraphHelper graphHelper;
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

        graphHelper = new RoomGraphHelper(roomGraph);
        this.generateGraph();
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
                neighborType type = graphHelper.IsNeighborNode(currentNode, potentialNeighbor);

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
            //fix
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

        for (var i = 0; i < roomGraph[node].Count; i++)
        {
            if (!path.Contains(roomGraph[node][i].Item1))
            {
                if (reachableRooms.Contains(roomGraph[node][i].Item1))
                {
                    reachAble.Add(roomGraph[node][i].Item1);
                    return reachAble;
                }
                possibleChoices.Add(roomGraph[node][i].Item1);
            }
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
                corridors.AddRange(getCorridors(RandomWalkUntilDead(i)));
            }
        }

        return corridors;
    }

    public List<CorridorNode> getCorridors(List<int> path)
    {
        List <CorridorNode> corridors = new List<CorridorNode>();

        for(var i = 0; i < path.Count - 1; i++)
        {   
            CorridorNode corridor = GetCorridorNode(path[i], path[i + 1]);
            RoomNode room1 = roomNodes[path[i]];
            RoomNode room2 = roomNodes[path[i+1]];
            room1.AddCorridor(corridor);
            room2.AddCorridor(corridor);

            corridors.Add(corridor);
        }

        return corridors;
    }

    public CorridorNode GetCorridorNode(int node1Key, int node2Key)
    {
        neighborType neighborTypeN1N2 = graphHelper.GetNeighborType(node1Key, node2Key);

        float lowerBound;
        float upperBound;

        Vector2 corridorTopLeft = new Vector2(0,0);
        Vector2 corridorBottomRight = new Vector2(0, 0);

        Orientation direction = Orientation.null_;

        if (neighborType.xNeighbor == neighborTypeN1N2)
        {
            direction = Orientation.Horizontal;
            var nodes = graphHelper.GetXOrientation(roomNodes[node1Key], roomNodes[node2Key]);
            var node1 = nodes.Item1;
            var node2 = nodes.Item2;

            lowerBound = graphHelper.GetLowerBound(node1.topLeft.y, node2.topLeft.y);
            upperBound = graphHelper.GetUpperBound(node1.topLeft.y, node2.topLeft.y, node1.length, node2.length);

            corridorTopLeft = new Vector2( node1.topLeft.x + node1.width, Random.Range(lowerBound, upperBound - this.minCorridorLength));
            corridorBottomRight = new Vector2( node2.topLeft.x , corridorTopLeft.y + this.minCorridorLength);
        }
        else if(neighborType.yNeighbor == neighborTypeN1N2)
        {
            direction = Orientation.Vertical;
            var nodes = graphHelper.GetYOrientation(roomNodes[node1Key], roomNodes[node2Key]);
            var node1 = nodes.Item1;
            var node2 = nodes.Item2;

            lowerBound = graphHelper.GetLowerBound(node1.topLeft.x, node2.topLeft.x);
            upperBound = graphHelper.GetUpperBound(node1.topLeft.x, node2.topLeft.x, node1.width, node2.width);
             
            corridorTopLeft = new Vector2( Random.Range( lowerBound, upperBound - this.minCorridorLength) , node1.topLeft.y + node1.length );
            corridorBottomRight = new Vector2( corridorTopLeft.x + this.minCorridorLength, node2.topLeft.y);
        }

        return new CorridorNode(corridorTopLeft, corridorBottomRight, direction);
    }

}
