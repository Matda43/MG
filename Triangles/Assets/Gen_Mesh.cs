using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_Mesh : MonoBehaviour
{
    public Material mat;

    void Start()
    {

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        string[] tab = new string[] { "buddha","bunny","cube","max","plan","triceratops" };

        SMesh s = new SMesh();
        s.reader("Assets/Models/triceratops.off");


        Mesh msh = new Mesh();

        msh.vertices = s.getPoints();
        msh.triangles = s.getTriangles();

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}
