using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuctureGen : MonoBehaviour
{

}

public abstract class ProceduralShape
{
    protected Mesh mesh_;
    protected Vector3[] vertices;
    protected float[] triangles;
    protected Vector2[] uvs;

    public ProceduralShape(Mesh mesh)
    {
        this.mesh_ = mesh;
    }
}

public class Plane : ProceduralShape
{
    private float sizeX, sizeY;

    public Plane(Mesh mesh, float sizeX, float sizeY): base(mesh)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        int totalVerticies = 1;
        CreateMesh();
    }

    private void CreateMesh()
    {
        CreateVertices();
    }

    private void CreateVertices()
    {
        //this.vertices = new Vector3[sizeX * sizeY];
        for(int y = 0; y < this.sizeY; y++)
        {
            for(int x = 0; x < this.sizeX; x++)
            {

            }
        }
    }
}
