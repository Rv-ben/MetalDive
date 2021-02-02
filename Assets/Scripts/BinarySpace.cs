using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>BinarySpace</c> 
/// models a space that is split in to smaller segments
/// </summary>
public class BinarySpace 
{
    public RoomNode rootNode { set; get; }

    public int minWidth;

    public int minLength;

    /// <summary>
    /// Init function 
    /// method <c>BinarySpace</c>
    /// </summary>
    /// <param name="spaceWidth">The width of the entire space</param>
    /// <param name="spaceLength">The lenght of the entire space</param>
    /// <param name="minSpaceWidth">Minimum width a space can be</param>
    /// <param name="minSpaceLength">Minimum length a space can be</param>
    public BinarySpace(int spaceWidth, int spaceLength, int minSpaceWidth, int minSpaceLength)
    {
        rootNode = new RoomNode(new Vector2Int(0, 0), spaceWidth, spaceLength, null);
        this.minWidth = minSpaceWidth;
        this.minLength = minSpaceLength;
        this.rootNode.CalcBottomRight();
    }

    /// <summary>
    /// method <c>PartionSpace</c>
    /// Determines if a space should be split, and the orientation
    /// </summary>
    /// <param name="node">A parent node</param>
    public void PartionSpace(RoomNode node)
    {
        Orientation orientation = Orientation.null_;

        bool lengthStatus = node.length >= this.minLength * 2;
        bool widthStatus = node.width >= this.minWidth * 2;

        if (lengthStatus && widthStatus)
        {
            orientation = (Orientation)(Random.Range(0, 2));
        }
        else if (widthStatus)
        {
            orientation = Orientation.Vertical;
        }
        else if (lengthStatus)
        {
            orientation = Orientation.Horizontal;
        }
        else
        {
            node.left = null;
            node.right = null;
        }

        DivideSpace(node, orientation);

        if (node.left != null)
        {
            PartionSpace((RoomNode)node.left);
        }
        if (node.right != null)
        {
            PartionSpace((RoomNode)node.right);
        }
        
    }

    /// <summary>
    /// method <c>DivideSpace</c>
    /// divides a space into two smaller spaces
    /// </summary>
    /// <param name="node">A parent node</param>
    /// <param name="orientation">A orientation a space should be split</param>
    public void DivideSpace(RoomNode node, Orientation orientation)
    {
        if(orientation == Orientation.Vertical)
        {

            int topRightX = node.topLeft.x + node.width;

            // Random X position to cut down
            int randomX = Random.Range(node.topLeft.x + this.minWidth, topRightX - this.minWidth);
            // Init top left position for new node
            Vector2Int newRoomTopLeft = new Vector2Int(randomX, node.topLeft.y);

            // Init new nodes 
            node.left = new RoomNode(node.topLeft, randomX - node.topLeft.x, node.length, node);
            node.right = new RoomNode(newRoomTopLeft, topRightX - randomX, node.length, node);
        }
        else if(orientation == Orientation.Horizontal)
        {

            int bottomLeftY = node.topLeft.y + node.length;

            // Random Y position to cut down
            int randomY = Random.Range(node.topLeft.y + this.minLength, bottomLeftY - this.minLength);
            // Init top left position for new node
            Vector2Int newRoomTopLeft = new Vector2Int(node.topLeft.x, randomY);

            node.left = new RoomNode(node.topLeft, node.width, randomY - node.topLeft.y , node);
            node.right = new RoomNode(newRoomTopLeft, node.width, bottomLeftY - randomY, node); ;
        }
    }
    
}
