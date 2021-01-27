using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a node with position
/// </summary>
public class RoomNode : Node
{
   public RoomNode(Vector2Int tl, Vector2Int br, Node parentNode, int index): base(parentNode)
    {
        this.tl = tl;
        this.br = br;

        this.treeLayer = index;
    }

    public int width { get => (int)(tl.x - br.x); }
    public int length { get => (int)(br.y - tl.y); }
}
