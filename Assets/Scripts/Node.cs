using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Node class
/// </summary>
public abstract class Node 
{
    public bool visted { get; set; }

    public int treeLayer { get; set; }

    public Vector2Int tl { get; set; }

    public Vector2Int br { get; set; }

    public Node parent { get; set; }

    public Node left { get; set; }

    public Node right { get; set; }

    public Node(Node parentNode)
    {
        this.parent = parentNode;
    }

}
