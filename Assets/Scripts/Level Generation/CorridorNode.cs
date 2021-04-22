using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorNode : Node
{
    /// <summary>
    /// method <c>CorridorNode</c>
    /// init corridorNode
    /// </summary>
    /// <param name="topLeft"></param>
    /// <param name="width">X axis</param>
    /// <param name="length"Y axis></param>
    /// <param name="parentNode"></param>

    public Orientation orientation;
    public CorridorNode(Vector2 topLeft, Vector2 bottomRight, Orientation orientation) : base(null)
    {
        this.topLeft = topLeft;
        this.bottomRight = bottomRight;
        this.orientation = orientation;
    }


    public void CalculateLength()
    {
        this.length = bottomRight.y - topLeft.y;
    }

    public void CalulateWidth()
    {
        this.width = bottomRight.x - topLeft.x;
    }

    public Orientation GetOrientation()
    {
        return this.orientation;
    } 

}
