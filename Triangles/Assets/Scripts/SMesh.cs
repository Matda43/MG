using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class SMesh
{

    string format;
    int nbVertices;
    int nbFaces;
    int nbEdges;
    Vector3[] vertices;
    int[] triangles;

    public void reader(string path)
    {
        FileInfo theSourceFile = new FileInfo(path);
        StreamReader reader = theSourceFile.OpenText();
        this.format = reader.ReadLine();
        string[] tab = reader.ReadLine().Split();
        this.nbVertices = int.Parse(tab[0]);
        this.nbFaces = int.Parse(tab[1]);
        this.nbEdges = int.Parse(tab[2]);
        this.vertices = new Vector3[this.nbVertices];
        this.triangles = new int[nbFaces*3];
        string text = reader.ReadLine();
        int iv = 0;
        int it = 0;
        while (text != null)
        {
            tab = text.Split();
            if (tab.Length > 0)
            {
                if (tab.Length == 3)
                {
                    float x = float.Parse(tab[0], System.Globalization.CultureInfo.InvariantCulture);
                    float y = float.Parse(tab[1], System.Globalization.CultureInfo.InvariantCulture);
                    float z = float.Parse(tab[2], System.Globalization.CultureInfo.InvariantCulture);
                    this.vertices[iv] = new Vector3(x, y, z);
                    iv++;
                }
                else
                {
                    int a = int.Parse(tab[1]);
                    int b = int.Parse(tab[2]);
                    int c = int.Parse(tab[3]);
                    this.triangles[it+0] = a;
                    this.triangles[it+1] = b;
                    this.triangles[it+2] = c;
                    it += 3;
                }
            }
            text = reader.ReadLine();
        }
        reader.Close();
    }


    public void writer(string path, Vector3[] vertices, int[] triangles)
    {
        
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("OFF");
        writer.WriteLine(vertices.Length + " " + (int)(triangles.Length/3) + " " + "0");
        foreach (Vector3 v in vertices)
        {
            writer.WriteLine(v.x.ToString("F", CultureInfo.InvariantCulture) + " " + v.y.ToString("F", CultureInfo.InvariantCulture) + " " + v.z.ToString("F", CultureInfo.InvariantCulture));
        }
        for(int it = 0; it < triangles.Length; it+=3)
        {
            writer.WriteLine("3" + " " + triangles[it+0] + " " + triangles[it+1] + " " + triangles[it+2]);
        }
        writer.Close();
    }


    public Vector3[] getVertices()
    {
        return this.vertices;
    }

    public int[] getTriangles()
    {
        return this.triangles;
    }


}
