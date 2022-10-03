using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gen_Mesh;

public class Gen_Sphere : MonoBehaviour
{
    public Material mat;

    [Range(1f, 100f)]
    public float radius;

    [Range(1, 100)]
    public int nbParallel;

    [Range(1, 100)]
    public int nbMeridian;

    public bool debug;
    public bool save = false;

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Sphere sphere = new Sphere(new Vector3(-0.5f, -9.5f, -8.5f), radius, nbParallel, nbMeridian);

        if (debug)
        {
            Debug.Log("Cylinder informations : Radius = " + radius + " | NbParallel = " + nbParallel + " | Nb Meridian = " + nbMeridian + " | Nb points = " + sphere.getPoints().Length);
        }

        Mesh msh = new Mesh();

        msh.vertices = sphere.getPoints();
        msh.triangles = sphere.getTriangles();

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

        if (save)
        {
            SMesh s = new SMesh();
            s.writer("Assets/Models/sphere.off", gameObject);
        }
    }
}
