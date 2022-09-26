using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere
{
    private Vector3 center;
    private float radius;
    private int nbParallel;
    private int nbMeridian;

    private float teta;
    private float phi;
    private Vector3[] points;
    private int[] triangles;

    public Sphere(Vector3 center, float radius, int nbParallel, int nbMeridian)
    {
        this.center = center;
        this.radius = radius;
        this.nbParallel = nbParallel;
        this.nbMeridian = nbMeridian;

        this.teta = 2f * Mathf.PI / this.nbMeridian;
        this.phi = 1f * Mathf.PI / this.nbParallel;
        this.points = makePoints();
        this.triangles = makeTriangles();
    }

    public Vector3[] makePoints()
    {
        Vector3[] points = new Vector3[nbMeridian * nbParallel];
        float teta_current = 0;
        float phi_current = phi/2;
        for (int y = 0; y < nbParallel; y++)
        {
            for (int x = 0; x < nbMeridian; x++)
            {
                Vector3 point = new Vector3(center.x + radius * Mathf.Sin(phi_current) * Mathf.Cos(teta_current), center.y + radius * Mathf.Cos(phi_current), center.z + radius * Mathf.Sin(phi_current) * Mathf.Sin(teta_current));

                GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
                g.transform.position = point;
                
                teta_current += teta;
                points[y * nbMeridian + x] = point;
                //Debug.Log(point);
            }
            phi_current += phi;
        }

        /*
        Vector3 bottom = new Vector3(center.x, center.y, center.z);
        Vector3 top = new Vector3(center.x, center.y + height * pasY, center.z);
        points[(height + 1) * nbMeridian] = bottom;
        points[(height + 1) * nbMeridian + 1] = top;
        */
        return points;
    }

    int[] makeTriangles()
    {
        int nbSquares = nbMeridian * nbParallel;
        int nbTriangles = nbSquares * 2;
        int[] triangles = new int[nbTriangles * 3];

        int inc = 0;
        for (int y = 0; y < nbParallel; y++)
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
