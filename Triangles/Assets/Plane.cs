using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane
{
    private Vector3 origin;
    private int width;
    private int height;
    private float pasX;
    private float pasY;
    private Vector3[] points;
    private int[] triangles;

    public Plane(Vector3 origin, int width, int height, float pasX, float pasY)
    {
        this.origin = origin;
        this.width = width;
        this.height = height;
        this.pasX = pasX;
        this.pasY = pasY;
        this.points = makePoints();
        this.triangles = makeTriangles();

    }

    Vector3[] makePoints()
    {
        Vector3[] points = new Vector3[(width + 1) * (height + 1)];
        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                Vector3 point = new Vector3(origin.x + x * pasX, origin.y + y * pasY, origin.z + 0 * pasX);
                points[y * (width + 1) + x] = point;
                //Debug.Log(point);
            }
        }
        return points;
    }

    int[] makeTriangles()
    {
        int nbSquares = width * height;
        int nbTriangles = nbSquares * 2;
        int[] triangles = new int[nbTriangles * 3];

        int inc = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //First triangle
                triangles[inc + 0] = y * (width + 1) + x;
                triangles[inc + 1] = y * (width + 1) + x + 1;
                triangles[inc + 2] = (y + 1) * (width + 1) + x;

                //Debug.Log(triangles[inc] + "," + triangles[inc + 1] + "," + triangles[inc + 2]);

                //Second triangle
                triangles[inc + 3] = y * (width + 1) + x + 1;
                triangles[inc + 4] = (y + 1) * (width + 1) + x + 1;
                triangles[inc + 5] = (y + 1) * (width + 1) + x;

                //Debug.Log(triangles[inc + 3] + "," + triangles[inc + 4] + "," + triangles[inc + 5]);

                inc = inc + 6;
            }
        }
        return triangles;
    }

    public Vector3[] getPoints()
    {
        return this.points;
    }

    public int[] getTriangles()
    {
        return this.triangles;
    }

}