using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_Sphere : MonoBehaviour
{
    public Material mat;
    public float radius;
    public int nbParallel;
    public int nbMeridian;
    public bool debug;

    void Start()
    {
        if (radius <= 0 || nbParallel <= 0 || nbMeridian <= 0)
        {
            return;
        }

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Sphere sphere = new Sphere(new Vector3(0, 0, 0), radius, nbParallel, nbMeridian);

        if (debug)
        {
            Debug.Log("Cylinder informations : Radius = " + radius + " | NbParallel = " + nbParallel + " | Nb Meridian = " + nbMeridian + " | Nb points = " + sphere.getPoints().Length);
        }

        Mesh msh = new Mesh();

        msh.vertices = sphere.getPoints();
        msh.triangles = sphere.getTriangles();

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }
}
