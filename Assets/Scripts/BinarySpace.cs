using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinarySpace 
{
    public RoomNode rootNode { set; get; }

    public int spaceWidth;

    public int spaceLength;

    public int minSpaceWidth;

    public int minSpaceLength;

    public BinarySpace(int spaceWidth, int spaceLength, int minSpaceWidth, int minSpaceLength)
    {
        this.spaceWidth = spaceWidth;
        this.spaceLength = spaceLength;
        this.minSpaceWidth = minSpaceWidth;
        this.minSpaceLength = minSpaceLength;
    }

    
}
