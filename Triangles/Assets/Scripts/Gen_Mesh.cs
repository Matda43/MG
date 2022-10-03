using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_Mesh : MonoBehaviour
{
    public Material mat;

    [Range(0f, 100f)]
    public float size;
    float remember_size;
    public bool normals = false;

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
    SMesh s;

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        s = new SMesh();
        s.chargementMaillage("Assets/Models/" + skeleton.ToString() + ".off");
        s.traceMaillage(gameObject, mat);
        s.calculNormalTriangles(gameObject);

        remember_size = size;
    }

    private void Update()
    {
        if (remember_size != size)
        {
            s.resize(gameObject, size);
            remember_size = size;
            s.calculNormalTriangles(gameObject);
        }
    }
}
