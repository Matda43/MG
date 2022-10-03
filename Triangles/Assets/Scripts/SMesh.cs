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
    Dictionary<int, GameObject> dictNormals;
    public void chargementMaillage(string path)
    {
        dictNormals = new Dictionary<int, GameObject>();
        FileInfo theSourceFile = new FileInfo(path);
        StreamReader reader = theSourceFile.OpenText();
        this.format = reader.ReadLine();
        string[] tab = reader.ReadLine().Split();
        this.nbVertices = int.Parse(tab[0]);
        this.nbFaces = int.Parse(tab[1]);
        this.nbEdges = int.Parse(tab[2]);
        this.vertices = new Vector3[this.nbVertices];
        this.triangles = new int[nbFaces*3];
        for(int i = 0; i < nbVertices; i++)
        {
            tab = reader.ReadLine().Split();
            float x = float.Parse(tab[0], System.Globalization.CultureInfo.InvariantCulture);
            float y = float.Parse(tab[1], System.Globalization.CultureInfo.InvariantCulture);
            float z = float.Parse(tab[2], System.Globalization.CultureInfo.InvariantCulture);
            this.vertices[i] = new Vector3(x, y, z);
        }
        for (int i = 0; i < nbFaces*3; i+=3)
        {
            tab = reader.ReadLine().Split();
            int a = int.Parse(tab[1]);
            int b = int.Parse(tab[2]);
            int c = int.Parse(tab[3]);
            this.triangles[i + 0] = a;
            this.triangles[i + 1] = b;
            this.triangles[i + 2] = c;
        }
        reader.Close();

        Vector3 centre = centreDeGravite();
        centrerModel(centre);
        normaliserSommets();
    }

    public void traceMaillage(GameObject gameObject, Material mat)
    {
        Mesh msh = new Mesh();

        msh.vertices = getVertices();
        msh.triangles = getTriangles();

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    Vector3 centreDeGravite()
    {
        Vector3 centre = Vector3.zero;
        if (vertices.Length == 0)
        {
            return centre;
        }
        foreach(Vector3 v in vertices)
        {
            centre += v;
        }
        return centre / vertices.Length;
    }

    void centrerModel(Vector3 centre)
    {
        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= centre;
        }
    }

    void normaliserSommets()
    {
        float valeurMax = 0;
        foreach(Vector3 v in vertices)
        {
            valeurMax = Math.Max(valeurMax, Math.Max(Math.Abs(v.x), Math.Max(Math.Abs(v.y), Math.Abs(v.z))));     
        }
        if(valeurMax > 0)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] /= valeurMax;
            }
        }
    }

    public void resize(GameObject g, float new_size)
    {
        Vector3[] new_vertices = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            new_vertices[i] = vertices[i] * new_size;
        }
        g.GetComponent<MeshFilter>().mesh.vertices = new_vertices;
    }

    public void calculNormalTriangles(GameObject gameObject)
    {
        Vector3[] vertices = gameObject.GetComponent<MeshFilter>().mesh.vertices;
        int j = 0;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            if (dictNormals.ContainsKey(j))
            {
                dictNormals[j].transform.position = calculNormalTriangle(vertices[triangles[i + 0]], vertices[triangles[i + 1]], vertices[triangles[i + 2]]);
            }
            else
            {
                GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
                g.transform.position = calculNormalTriangle(vertices[triangles[i + 0]], vertices[triangles[i + 1]], vertices[triangles[i + 2]]);
                g.transform.localScale = Vector3.one / 5;
                dictNormals[j] = g;
            }
            j++;
        }
    }

    Vector3 calculNormalTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (p1 + p2 + p3) / 3;
    }


    public void writer(string path, GameObject gameObject)
    {
        Vector3[] vertices = gameObject.GetComponent<MeshFilter>().mesh.vertices;
        int[] triangles = gameObject.GetComponent<MeshFilter>().mesh.triangles;

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
