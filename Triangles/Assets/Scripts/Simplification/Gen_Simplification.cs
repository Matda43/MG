using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_Simplification : MonoBehaviour
{
    public Material mat;

    [Range(0.5f, 10f)]
    public float simplification;
    float remember_simplification;

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
    Grid_Simplification gs;
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        s = new SMesh();
        s.chargementMaillage("Assets/Models/" + skeleton.ToString() + ".off");
        
        gs = new Grid_Simplification();
        s.traceMaillage(gameObject, mat);
        gs.changePositionAndResize(this.transform, gameObject);

        gs.createGrid(gameObject.GetComponent<MeshFilter>().mesh.vertices, this.transform, simplification);
        remember_simplification = simplification;

        gs.simplification(gameObject);
    }

    private void Update()
    {
        if (remember_simplification != simplification)
        {
            gs.createGrid(gameObject.GetComponent<MeshFilter>().mesh.vertices, this.transform, simplification);
            gs.simplification(gameObject);
            remember_simplification = simplification;
        }
    }
}
