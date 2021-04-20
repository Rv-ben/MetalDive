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

    public List<CorridorNode> topCorridors { get;}
    public List<CorridorNode> leftCorridors { get;}
    public List<CorridorNode> bottomCorridors { get; }
    public List<CorridorNode> rightCorridors { get; }

    public RoomNode(Vector2 topLeft, float width, float length, Node parentNode): base(parentNode)
    {
        this.topLeft = topLeft;
        this.width = width;
        this.length = length;
        this.topCorridors = new List<CorridorNode>();
        this.leftCorridors = new List<CorridorNode>();
        this.bottomCorridors = new List<CorridorNode>();
        this.rightCorridors = new List<CorridorNode>();
        CalcBottomRight();
    }

    /// <summary>
    /// method <c>CalBottomRight</c>
    /// </summary>
    public void CalcBottomRight()
    {
        this.bottomRight = new Vector2(this.topLeft.x + this.width, this.topLeft.y + this.length);
    }

    /// <summary>
    /// method <c>Shrink</c>
    /// Shrinks room length and width based on a percentage
    /// </summary>
    /// <param name="widthPercentage"></param>
    /// <param name="lengthPercentage"></param>
    public void Shrink(float widthPercentage, float lengthPercentage)
    {
        this.width = this.width - this.width * widthPercentage;
        this.length = this.length - this.length * lengthPercentage;
        CalcBottomRight();
    }

    /// <summary>
    /// Get a random position room.
    /// </summary>
    /// <returns>a Vector2</returns>
    public Vector2 GetRandomPosition()
    {
        float x = this.topLeft.x + UnityEngine.Random.Range(0, this.width);
        float y = this.topLeft.y + UnityEngine.Random.Range(0, this.length);

        return new Vector2(x, y);
    }

    /// <summary>
    /// Add a new corridor
    /// </summary>
    /// <param name="corridor"></param>
    public void AddCorridor(CorridorNode corridor) 
    {
        Orientation corridorOrientation = corridor.GetOrientation();
        Vector2 topLeft = corridor.topLeft;
        
        if (corridorOrientation == Orientation.Horizontal)
        {   
            // Right side
            if (topLeft.x == this.topLeft.x + this.width) 
            {
                this.rightCorridors.Add(corridor);
            }
            else
            {
                this.leftCorridors.Add(corridor);
            }
        }
        else
        {
            if (topLeft.y == this.topLeft.y + this.length)
            {
                this.bottomCorridors.Add(corridor);
            }
            else
            {
                this.topCorridors.Add(corridor);
            }
        }
    }

}
