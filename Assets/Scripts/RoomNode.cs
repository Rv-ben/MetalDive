using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a node with position
/// </summary>
public class RoomNode : Node
{
   public RoomNode(Vector2Int topLeft, int width, int length, Node parentNode): base(parentNode)
    {
        this.topLeft = topLeft;
        this.width = width;
        this.length = length;
        calcBottomRight();
    }

    public void calcBottomRight()
    {
        this.bottom_right.Set(topLeft.x + width, topLeft.y + length);
    }

}
