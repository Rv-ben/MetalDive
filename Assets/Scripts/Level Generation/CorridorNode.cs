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
    /// <param name="width"></param>
    /// <param name="length"></param>
    /// <param name="parentNode"></param>
    public CorridorNode(Vector2 topLeft, Vector2 bottomRight) : base(null)
    {
        this.topLeft = topLeft;
        this.bottomRight = bottomRight;
    }


    public void CalculateLength()
    {
        this.length = bottomRight.y - topLeft.y;
    }

    public void CalulateWidth()
    {
        this.width = bottomRight.x - topLeft.x;
    }

}
