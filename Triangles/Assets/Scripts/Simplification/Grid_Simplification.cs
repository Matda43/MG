using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Simplification
{
    Vector3 posTopLeftFront;
    Vector3 posBottomRightBack;
    Vector3 dimensions;
    Vector3 nbCubes;
    Dictionary<Vector3, Cube_Simplification> cubes;
    Vector3 centerGrid;

    Vector3 getDimensions()
    {
        Vector3 dimensions = posTopLeftFront - posBottomRightBack;
        return new Vector3(Mathf.Abs(dimensions.x), Mathf.Abs(dimensions.y), Mathf.Abs(dimensions.z));
    }

    public void createGrid(Vector3[] vertices, Transform transform, float step)
    { 
        this.posTopLeftFront = getMax(vertices);
        this.posBottomRightBack = getMin(vertices);
        this.dimensions = getDimensions();

        this.nbCubes = new Vector3((int)(dimensions.x / step) + 2, (int)(dimensions.y / step) + 2, (int)(dimensions.z / step) + 2);
        this.centerGrid = (nbCubes / 2 * step);

        this.cubes = new Dictionary<Vector3, Cube_Simplification>();

        for (int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    Vector3 position = new Vector3(i, j, k);
                    Vector3 realPosition = new Vector3((i + 0.5f) * step, (j + 0.5f) * step, (k + 0.5f) * step) - centerGrid + transform.position;
                    this.cubes.Add(position, new Cube_Simplification(realPosition, step));
                }
            }
        }
    }

    Vector3 getMin(Vector3[] vertices)
    {
        Vector3 res = vertices[0];
        foreach(Vector3 v in vertices)
        {
            res.x = res.x > v.x ? v.x : res.x;
            res.y = res.y > v.y ? v.y : res.y;
            res.z = res.z > v.z ? v.z : res.z;
        }
        return res;
    }

    Vector3 getMax(Vector3[] vertices)
    {
        Vector3 res = vertices[0];
        foreach (Vector3 v in vertices)
        {
            res.x = res.x < v.x ? v.x : res.x;
            res.y = res.y < v.y ? v.y : res.y;
            res.z = res.z < v.z ? v.z : res.z;
        }
        return res;
    }

    public void changePositionAndResize(Transform transform, GameObject go)
    {
        Vector3[] vertices = go.GetComponent<MeshFilter>().mesh.vertices;
        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += transform.position;
            vertices[i] *= 20;
        }
        go.GetComponent<MeshFilter>().mesh.vertices = vertices;
    }


    public void simplification(GameObject go)
    {
        Vector3[] vertices = go.GetComponent<MeshFilter>().mesh.vertices;
        int[] triangles = go.GetComponent<MeshFilter>().mesh.triangles;

        int[] matriceIdentification = new int[vertices.Length];
        for (int assign = 0; assign < vertices.Length; assign++)
        {
            matriceIdentification[assign] = assign;
        }

        for (int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    Vector3 position = new Vector3(i, j, k);

                    for (int pos = 0; pos < vertices.Length; pos++)
                    {

                        if (cubes[position].isInBox(vertices[pos]))
                        {
                            if (!cubes[position].hasAPoint())
                            {
                                cubes[position].setFirstPointIndice(pos);
                            }
                            else
                            {
                                matriceIdentification[pos] = cubes[position].getFirstPosition();
                            }

                        }
                    }
                }
            }
        }

        for (int tri = 0; tri < triangles.Length; tri++)
        {
            triangles[tri] = matriceIdentification[triangles[tri]];
        }

        int nbTriangles = 0;

        for (int tri = 0; tri < triangles.Length; tri += 3)
        {
            nbTriangles += 3;
            if (triangles[tri] == triangles[tri + 1] || triangles[tri] == triangles[tri + 2] || triangles[tri + 2] == triangles[tri + 1])
            {
                nbTriangles -= 3;
                triangles[tri] = -1;
                triangles[tri + 1] = -1;
                triangles[tri + 2] = -1;
            }
        }
        int[] newTriangles = new int[nbTriangles];
        int indexNewTriangle = 0;
        for (int index = 0; index < triangles.Length; index++)
        {
            if (triangles[index] >= 0)
            {
                newTriangles[indexNewTriangle] = triangles[index];
                indexNewTriangle++;
                
            }
        }

        go.GetComponent<MeshFilter>().mesh.triangles = newTriangles;
    }
}
