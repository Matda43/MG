using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_Mesh : MonoBehaviour
{
    public Material mat;

    public enum Skeleton
    {
        [InspectorName("Buddha")]
        buddha,
        [InspectorName("Bunny")]
        bunny,
        [InspectorName("Cube")]
        cube,
        [InspectorName("Max")]
        max,
        [InspectorName("Plan")]
        plan,
        [InspectorName("Triceratops")]
        triceratops
    };

    public Skeleton skeleton;

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        SMesh s = new SMesh();
        s.reader("Assets/Models/" + skeleton.ToString() + ".off");

        Mesh msh = new Mesh();

        msh.vertices = s.getVertices();
        msh.triangles = s.getTriangles();

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}
