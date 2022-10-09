using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_Plane : MonoBehaviour
{
    public Material mat;

    [Range(1, 100)]
    public int width;

    [Range(1, 100)]
    public int height;

    [Range(1f, 100f)]
    public float pasX;

    [Range(1f, 100f)]
    public float pasY;

    public bool debug;

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Plane plane = new Plane(new Vector3(10, 0, 0), width, height, pasX, pasY);

        if (debug)
        {
            Debug.Log("Plane informations : Width = " + width + " | Height = " + height + " | Square = " + width * height + " | Nb points = " + plane.getPoints().Length);
        }

        Mesh msh = new Mesh();

        msh.vertices = plane.getPoints();
        msh.triangles = plane.getTriangles();

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}
