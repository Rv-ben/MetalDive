using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>RoomTree</c>
/// Represents a binary tree of rooms
/// </summary>
public class RoomTree
{
    public RoomNode rootNode;

    private List<RoomNode> leaves;

    /// <summary>
    /// method <c>RoomTree</c>
    /// Init RoomTree
    /// </summary>
    /// <param name="rootNode">RootNode of the tree</param>
    public RoomTree(RoomNode rootNode)
    {
        this.rootNode = rootNode;
        leaves = new List<RoomNode>();
    }

    /// <summary>
    /// method <c>GetLeaves</c>
    /// </summary>
    /// <returns>List of RoomNodes</returns>
    public List<RoomNode> GetLeaves()
    {
        FindLeaves(rootNode);
        return leaves;
    }

    /// <summary>
    /// method <c>FindLeaves</c>
    /// Finds and appends leaf nodes to this.leaves
    /// </summary>
    /// <param name="currentNode">RoomNode with left and right</param>
    private void FindLeaves(RoomNode currentNode)
    {
        bool isLeaf = currentNode.left == null && currentNode.right == null;
        if (isLeaf)
        {
            leaves.Add(currentNode);
        }
        else
        {
            FindLeaves((RoomNode)currentNode.left);
            FindLeaves((RoomNode)currentNode.right);
        }
    }
}
