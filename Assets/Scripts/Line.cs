using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Line 
{

    public Line(Orientation orientation, Vector2Int coordinates)
    {
        this.orientation = orientation;
        this.coordinates = coordinates;
    }

    public Orientation orientation { get; set; }
    public Vector2Int coordinates { get ; set ; }
}

public enum Orientation
{
    Horizontal = 0,
    Vertical = 1
}
