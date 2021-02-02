using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Node</c>
/// Represents a node 
/// </summary>
public abstract class Node 
{
    public bool visted { get; set; }

    public int treeLayer { get; set; }

    public Vector2Int topLeft { get; set; }

    public Vector2Int bottom_right { get; set; }

    public Node parent { get; set; }

    public Node left { get; set; }

    public Node right { get; set; }
    public int width { get; set; }
    public int length { get; set; }

    /// <summary>
    /// method <c>Node</c>
    /// Inits node
    /// </summary>
    /// <param name="parentNode">A parent node</param>
    public Node(Node parentNode)
    {
        this.parent = parentNode;
    }

}
