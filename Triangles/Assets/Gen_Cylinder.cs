using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_Cylinder : MonoBehaviour
{
    public Material mat;
    public float radius;
    public int height;
    public float pasY;
    public int nbMeridian;
    public bool debug;

    void Start()
    {
        if (radius <= 0 || height <= 0 || pasY <= 0 || nbMeridian <= 0)
        {
            return;
        }

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Cylinder cylinder = new Cylinder(new Vector3(0, 0, 0), radius, height, pasY, nbMeridian);

        if (debug)
        {
            Debug.Log("Cylinder informations : Radius = " + radius + " | Height = " + height + " | Nb Meridian = " + nbMeridian + " | Nb points = " + cylinder.getPoints().Length);
        }

        Mesh msh = new Mesh();

        msh.vertices = cylinder.getPoints();
        msh.triangles = cylinder.getTriangles();

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }
}
