using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;



public class BinarySpacePartitioner 
{
    public RoomNode rootNode;
    private int dungeonWidth;
    private int dungeonLength;

    public BinarySpacePartitioner(int dungeonWidth, int dungeonLength)
    {
        this.rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(dungeonWidth, dungeonLength), null, 0);
    }

    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(this.rootNode);
        listToReturn.Add(this.rootNode);
        int iterations = 0;

        while(iterations < maxIterations && graph.Count > 0)
        {
            iterations++;
            RoomNode currentNode = graph.Dequeue();
            
            if(currentNode.width >= roomWidthMin*2 || currentNode.length >= roomLengthMin * 2)
            {
                SplitTheSpace(currentNode, listToReturn, roomLengthMin, roomWidthMin, graph);
            }
        }
        return listToReturn;
    }

    public void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn, int roomLengthMin, int roomWidthMin, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(
            currentNode.bottomLeftAreaCorner,
            currentNode.topRightAreaCorner,
            roomWidthMin,
            roomLengthMin);

        RoomNode node1, node2;
        if(line.orientation == Orientation.Horizontal)
        {
            node1 = new RoomNode(currentNode.bottomLeftAreaCorner,
                new Vector2Int(currentNode.topRightAreaCorner.x, line.coordinates.y)
                , currentNode
                , currentNode.treeLayerIndex + 1);

            node2 = new RoomNode(new Vector2Int(currentNode.bottomLeftAreaCorner.x, line.coordinates.y)
                , currentNode.topRightAreaCorner
                , currentNode
                , currentNode.treeLayerIndex + 1);
        }
        else
        {
            node1 = new RoomNode(currentNode.bottomLeftAreaCorner,
                new Vector2Int(line.coordinates.x, currentNode.topRightAreaCorner.y)
                , currentNode
                , currentNode.treeLayerIndex + 1);

            node2 = new RoomNode(new Vector2Int(line.coordinates.x,currentNode.bottomLeftAreaCorner.y)
                , currentNode.topRightAreaCorner
                , currentNode
                , currentNode.treeLayerIndex + 1);
        }

        AddNewNodeToCollections(listToReturn, graph, node1);
        AddNewNodeToCollections(listToReturn, graph, node2);
    }

    public void AddNewNodeToCollections(List<RoomNode> listToReturn, Queue<RoomNode> graph, RoomNode node)
    {
        listToReturn.Add(node);
        graph.Enqueue(node);
    }

    public Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        Orientation orientation;
        bool lengthStatus = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * roomLengthMin;
        bool widthStatus = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * roomLengthMin;

        if (lengthStatus && widthStatus)
        {
            orientation = (Orientation)(Random.Range(0, 2));
        }else if (widthStatus)
        {
            orientation = Orientation.Vertical;
        }
        else
        {
            orientation = Orientation.Horizontal;
        }

        return new Line(orientation, GetCoordinatesForOrientation(
            orientation,
            bottomLeftAreaCorner,
            topRightAreaCorner,
            roomWidthMin,
            roomLengthMin));
    }

    public Vector2Int GetCoordinatesForOrientation(Orientation orienation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        Vector2Int coordinates = Vector2Int.zero;
        if (orienation == Orientation.Horizontal)
        {
            coordinates = new Vector2Int(0, Random.Range(
                (bottomLeftAreaCorner.y + roomLengthMin),
                (topRightAreaCorner.y - roomLengthMin)));
        }
        else
        {
            coordinates = new Vector2Int(Random.Range(
                (bottomLeftAreaCorner.x + roomWidthMin),
                (topRightAreaCorner.x - roomWidthMin))
                ,0);
        }
        return coordinates;
    }

}
