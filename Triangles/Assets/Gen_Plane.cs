using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_Plane : MonoBehaviour
{
    public Material mat;
    public int width;
    public int height;
    public float pasX;
    public float pasY;
    public bool debug;

    void Start()
    {
        if (width <= 0 || height <= 0 || pasX <= 0 || pasY <= 0)
        {
            return;
        }

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Plane plane = new Plane(new Vector3(0, 0, 0), width, height, pasX, pasY);

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
