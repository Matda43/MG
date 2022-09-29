using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SMesh
{

    string format;
    int nbVertices;
    int nbFaces;
    int nbEdges;
    List<Vector3> points;
    List<Vector3> faces;

    public void reader(string path)
    {
        this.points = new List<Vector3>();
        this.faces = new List<Vector3>();
        FileInfo theSourceFile = new FileInfo(path);
        StreamReader reader = theSourceFile.OpenText();
        string text = reader.ReadLine();
        int i = 0;

        while(text != null)
        {
            string[] tab = text.Split();
            if (tab.Length > 0)
            {
                if (i == 0)
                {
                    this.format = text;
                }
                else
                {
                    if (i == 1 && tab.Length == 3)
                    {
                        this.nbVertices = int.Parse(tab[0]);
                        this.nbFaces = int.Parse(tab[1]);
                        this.nbEdges = int.Parse(tab[2]);
                    }else if(i > 1 && tab.Length == 3)
                    {
                        foreach (string s in tab)
                        {
                            Debug.Log(s);
                        }
                        float x = float.Parse(tab[0], System.Globalization.CultureInfo.InvariantCulture);
                        float y = float.Parse(tab[1], System.Globalization.CultureInfo.InvariantCulture);
                        float z = float.Parse(tab[2], System.Globalization.CultureInfo.InvariantCulture);
                        this.points.Add(new Vector3(x, y, z));
                    }else
                    {
                        int a = int.Parse(tab[1]);
                        int b = int.Parse(tab[2]);
                        int c = int.Parse(tab[3]);
                        this.faces.Add(new Vector3(a, b, c));
                    }
                }
                i++;
            }
            text = reader.ReadLine();
        }
    }

    public Vector3[] getPoints()
    {
        return this.points.ToArray();
    }

    public int[] getTriangles()
    {
        int[] triangles = new int[this.faces.Count * 3];
        int i = 0;
        foreach(Vector3 v in this.faces)
        {
            triangles[i] = (int)(v.x);
            triangles[i+1] = (int)(v.y);
            triangles[i+2] = (int)(v.z);
            i = i + 3;
        }
        return triangles;
    }


}
