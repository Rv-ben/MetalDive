﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : Node
{
   public RoomNode(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, Node parentNode, int index): base(parentNode)
    {
        this.bottomLeftAreaCorner = bottomLeftAreaCorner;
        this.topRightAreaCorner = topRightAreaCorner;

        this.bottomRightAreaCorner = new Vector2Int(topRightAreaCorner.x,bottomLeftAreaCorner.y);
        this.topLeftAreaCorner = new Vector2Int(bottomLeftAreaCorner.x,topRightAreaCorner.y)

        this.treeLayerIndex = index;
    }

    public int width { get => (int)(topRightAreaCorner.x - bottomLeftAreaCorner.x); }
    public int length { get => (int)(toRightAreaCorner.y - bottomLeftAreaCorner.y); }
}
