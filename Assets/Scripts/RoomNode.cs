using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>RoomNode</c>
/// Represents a node with position
/// </summary>
public class RoomNode : Node
{   
    /// <summary>
    /// method <c>RoomNode</c>
    /// Init RoomNode
    /// </summary>
    /// <param name="topLeft">top left coords</param>
    /// <param name="width">width of a room</param>
    /// <param name="length">length of a room</param>
    /// <param name="parentNode">parent of roomnode</param>
   public RoomNode(Vector2 topLeft, float width, float length, Node parentNode): base(parentNode)
    {
        this.topLeft = topLeft;
        this.width = width;
        this.length = length;
        CalcBottomRight();
    }

    /// <summary>
    /// method <c>CalBottomRight</c>
    /// </summary>
    public void CalcBottomRight()
    {
        this.bottomRight.Set(this.topLeft.x + this.width, this.topLeft.y + this.length);
    }

    /// <summary>
    /// method <c>Shrink</c>
    /// 
    /// </summary>
    /// <param name="widthPercentage"></param>
    /// <param name="lengthPercentage"></param>
    public void Shrink(float widthPercentage, float lengthPercentage)
    {
        this.width *= widthPercentage;
        this.length *= lengthPercentage;
        CalcBottomRight();
    }

}
