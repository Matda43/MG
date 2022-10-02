using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_Cylinder : MonoBehaviour
{
    public Material mat;
    
    [Range(1f, 100f)]
    public float radius;

    [Range(1, 100)]
    public int height;

    [Range(1f, 100f)]
    public float pasY;

    [Range(3, 100)]
    public int nbMeridian;

    public bool debug;
    public bool save = false;

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Cylinder cylinder = new Cylinder(new Vector3(15, 0, 0), radius, height, pasY, nbMeridian);

        if (debug)
        {
            Debug.Log("Cylinder informations : Radius = " + radius + " | Height = " + height + " | Nb Meridian = " + nbMeridian + " | Nb points = " + cylinder.getPoints().Length);
        }

        Mesh msh = new Mesh();

        msh.vertices = cylinder.getPoints();
        msh.triangles = cylinder.getTriangles();

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

        if (save)
        {
            SMesh s = new SMesh();
            s.writer("Assets/Models/cylinder.off", cylinder.getPoints(), cylinder.getTriangles());
        }
    }
}
