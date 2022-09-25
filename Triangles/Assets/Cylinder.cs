using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class Cylinder
{
    private Vector3 center;
    private float radius;
    private int height;
    private float pasY;
    private int nbMeridian;
    
    private float teta;
    private Vector3[] points;
    private int[] triangles;

    public Cylinder(Vector3 center, float radius, int height, float pasY, int nbMeridian)
    {
        this.center = center;
        this.radius = radius;
        this.height = height;
        this.pasY = pasY;
        this.nbMeridian = nbMeridian;

        this.teta = 2f * Mathf.PI / nbMeridian;
        this.points = makePoints();
        this.triangles = makeTriangles();
    }

    public Vector3[] makePoints()
    {
        Vector3[] points = new Vector3[nbMeridian * (height + 1) + 2];
        float teta_current = 0;
        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x < nbMeridian; x++)
            {
                Vector3 point = new Vector3(center.x + Mathf.Cos(teta_current) * radius, center.y + y * pasY, center.z + Mathf.Sin(teta_current) * radius);
                teta_current -= teta;
                points[y * nbMeridian + x] = point;
                //Debug.Log(point);
            }
        }

        Vector3 bottom = new Vector3(center.x, center.y, center.z);
        Vector3 top = new Vector3(center.x, center.y + height, center.z);
        points[(height + 1) * nbMeridian] = bottom;
        points[(height + 1) * nbMeridian + 1] = top;
        return points;
    }

    int[] makeTriangles()
    {
        int nbSquares = nbMeridian * height;
        int nbTriangles = nbSquares * 2;
        int[] triangles = new int[nbTriangles * 3 + 2 * nbMeridian * 3];

        int inc = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < nbMeridian; x++)
            {
                if (x < nbMeridian - 1)
                {
                    //First triangle
                    triangles[inc + 0] = y * nbMeridian + x;
                    triangles[inc + 1] = y * nbMeridian + x + 1;
                    triangles[inc + 2] = (y + 1) * nbMeridian + x;
                    //Debug.Log(triangles[inc] + "," + triangles[inc + 1] + "," + triangles[inc + 2]);

                    //Second triangle
                    triangles[inc + 3] = y * nbMeridian + x + 1;
                    triangles[inc + 4] = (y + 1) * nbMeridian + x + 1;
                    triangles[inc + 5] = (y + 1) * nbMeridian + x;
                    //Debug.Log(triangles[inc + 3] + "," + triangles[inc + 4] + "," + triangles[inc + 5]);
                }
                else
                {
                    //First triangle
                    triangles[inc + 0] = y * nbMeridian + x;
                    triangles[inc + 1] = y * nbMeridian;
                    triangles[inc + 2] = (y + 1) * nbMeridian + x;
                    //Debug.Log(triangles[inc] + "," + triangles[inc + 1] + "," + triangles[inc + 2]);

                    //Second triangle
                    triangles[inc + 3] = y * nbMeridian;
                    triangles[inc + 4] = (y + 1) * nbMeridian;
                    triangles[inc + 5] = (y + 1) * nbMeridian + x;
                    //Debug.Log(triangles[inc + 3] + "," + triangles[inc + 4] + "," + triangles[inc + 5]);
                }
                inc = inc + 6;
            }
        }

        //Bottom
        for(int i = 0; i < nbMeridian; i++)
        {
            triangles[inc + 0] = (height + 1) * nbMeridian;
            triangles[inc + 1] = (i + 1) % nbMeridian;
            triangles[inc + 2] = i;
            //Debug.Log(triangles[inc] + "," + triangles[inc + 1] + "," + triangles[inc + 2]);
            inc = inc + 3;
        }

        //Top
        for (int i = 0; i < nbMeridian; i++)
        {
            triangles[inc + 0] = (height * nbMeridian + i);
            triangles[inc + 1] = (height * nbMeridian) + ((i + 1) % nbMeridian);
            triangles[inc + 2] = (height + 1) * nbMeridian + 1;
            //Debug.Log(triangles[inc] + "," + triangles[inc + 1] + "," + triangles[inc + 2]);
            inc = inc + 3;
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
