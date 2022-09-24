using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_Plane : MonoBehaviour
{
    public Material mat;
    public int width;
    public int height;
    public float pasX;
    public float pasY;

    // Use this for initialization
    void Start()
    {
        if (width <= 0 || height <= 0 || pasX <= 0 || pasY <= 0)
        {
            return;
        }

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Plane plane = new Plane(new Vector3(0, 0, 0), width, height, pasX, pasY);

        Mesh msh = new Mesh();

        msh.vertices = plane.getPoints();
        msh.triangles = plane.getTriangles();

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }
}
