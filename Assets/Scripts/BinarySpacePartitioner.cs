using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class BinarySpacePartitioner 
{
    RoomNode rootNode;
    private int dungeonWidth;
    private int dungeonLength;

    public BinarySpacePartitioner(int dungeonWidth, int dungeonLength)
    {
        this.rootNode = new RoomNode(new Vector2Int(0,0), new Vector2Int(dungeonWidth,dungeonLength),null,0)
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
            
            if(currentNode.Width >= roomWidthMin*2 || currentNode.Length >= roomLengthMin * 2)
            {
                SplitTheSpace(currentNode, listToReturn, roomLengthMin, roomWidthMin, graph);
            }
        }
    }

    public void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn, int roomLengthMin, int roomWidthMin, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(
            currentNode.BottomLeftAreaCorner,
            currentNode.TopRightAreaCorner,
            roomWidthMin,
            roomLengthMin);
    }

    public void GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        Orientation orientation;
        bool lengthStatus = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * roomLengthMin;
        bool widthStatus = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * roomLengthMin;

        if (lengthStatus && widthStatus)
        {
            orientation = (Orientation)(Random.Range(0, 2));
        }else if (widthStatus)
        {
            orientation = Orientation.Veritcal;
        }
        else
        {
            orientation = Orientation.Horizontal;
        }

        return new Line(orientation, GetCoordinatesForOrientaion(
            orientation,
            bottomLeftAreaCorner,
            topRightAreaCorner,
            roomWidthMin,
            roomLengthMin))
    }

}
