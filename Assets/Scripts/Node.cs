using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node 
{
    private List<Node> childrenNodeList;

    public List<Node> ChildrenNodeList { get => childrenNodeList; }

    public bool Visted { get; set; }

    public Vector2Int bottomLeftAreaCorner { get; set; }

    public Vector2Int bottomRightAreaCorner { get; set; }

    public Vector2Int topRightAreaCorner { get; set; }

    public Vector2Int topLeftAreaCorner { get; set; }

    public Node parent { get; set; }

    public int treeLayerIndex { get; set; }

    public Node(Node parentNode)
    {
        childrenNodeList = new List<Node>();
        this.parent = parentNode;

        if(parentNode != null)
        {
            parentNode.AddChild(this);
        }
    }

    public void AddChild(Node node)
    {
        childrenNodeList.Add(node);
    }

    public void RemoveChild(Node node)
    {
        childrenNodeList.Remove(node);
    }
}
