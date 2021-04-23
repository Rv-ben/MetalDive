using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall
{
    public Vector2 startPoint;

    public Orientation orientation;

    public float length;

    public Wall(Vector2 startPoint, float length, Orientation orientation)
    {
        this.startPoint = startPoint;
        this.length = length;
        this.orientation = orientation;
    }
}
