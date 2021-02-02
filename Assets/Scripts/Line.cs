using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// Represents a line, can only be horizontal or vetical 
/// </summary>
public class Line 
{
    /// <summary>
    /// Line constructor 
    /// </summary>
    /// <param name="orientation">Orientation of the line(horizontal or veritcal)</param>
    /// <param name="coordinates">2D position of the line</param>
    public Line(Orientation orientation, Vector2Int coordinates)
    {
        this.orientation = orientation;
        this.coordinates = coordinates;
    }

    public Orientation orientation { get; set; }
    public Vector2Int coordinates { get ; set ; }
}

/// <summary>
/// Enum for orientation
/// </summary>
public enum Orientation
{
    Horizontal = 0,
    Vertical = 1,
    null_ = -1
}
