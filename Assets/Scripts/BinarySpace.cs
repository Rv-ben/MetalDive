using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinarySpace 
{
    public RoomNode rootNode { set; get; }

    public int minWidth;

    public int minLength;

    public BinarySpace(int spaceWidth, int spaceLength, int minSpaceWidth, int minSpaceLength)
    {
        rootNode = new RoomNode(new Vector2Int(0, 0), spaceWidth, spaceLength, null);
        this.minWidth = minSpaceWidth;
        this.minLength = minSpaceLength;
        this.rootNode.calcBottomRight();
    }

    public void partionSpace(RoomNode node)
    {   
        Orientation orientation = (Orientation)(Random.Range(0, 2));

        if(orientation == Orientation.Vertical)
        {
            int randomX = Random.Range(this.minWidth, node.width);
            Vector2Int newRoomTopLeft = new Vector2Int(randomX, node.topLeft.y);
            if(! (node.width >= this.minWidth * 2))
            {
                node.left = null;
                node.right = null;
            }
            else
            {
                node.left = new RoomNode(node.topLeft, randomX, node.length, node);
                node.right = new RoomNode(newRoomTopLeft, node.width - randomX, node.length, node);
            }

        }
        else
        {
            int randomY = Random.Range(this.minLength, node.length);
            Vector2Int newRoomTopLeft = new Vector2Int(node.topLeft.x, randomY);
            if ( !(node.length >= this.minLength * 2))
            {
                node.left = null;
                node.right = null;
            }
            else
            {
                node.left = new RoomNode(node.topLeft, node.width, randomY, node);
                node.right = new RoomNode(newRoomTopLeft, node.width, node.length - randomY, node);
            }
        }
        if (node.left != null)
        {
            partionSpace((RoomNode)node.left);
        }
        if (node.right != null)
        {
            partionSpace((RoomNode)node.right);
        }
        
    }
    
}
