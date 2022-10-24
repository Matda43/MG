using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Gen_Subdivision : MonoBehaviour
{
    public Material mat;

    [Range(1, 10)]
    public int subdivision;
    int remember_subdivision;

    [Range(0f, 0.5f)]
    public float ratio;
    float remember_ratio;

    LineRenderer lineRenderer;

    Subdivision sub;
    List<Vector3> list;
    List<Vector3> subList;

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        sub = new Subdivision();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        list = new List<Vector3>() { new Vector3(-30, 0, 0), new Vector3(-30, 10, 10), new Vector3(-20, 10, -10), new Vector3(-20, -5, 5), new Vector3(-30,0,0) };

        subList = sub.subdivide(list, (1-ratio), ratio);
        lineRenderer.positionCount = subList.Count;
        lineRenderer.SetPositions(subList.ToArray());

        remember_ratio = ratio;
    }

    private void Update()
    {
        if(remember_ratio != ratio)
        {
            subList = sub.subdivide(list, (1 - ratio), ratio);
            lineRenderer.positionCount = subList.Count;
            lineRenderer.SetPositions(subList.ToArray());
            remember_ratio = ratio;
        }
        if(remember_subdivision != subdivision)
        {
            int div = 0;
            if (subdivision < remember_subdivision)
            {
                div = subdivision;
                subList = list;
            }
            if (subdivision > remember_subdivision)
            {
                div = subdivision - remember_subdivision;
            }

            for (int i = 0; i < div; i++)
            {
                subList = sub.subdivide(subList, (1 - ratio), ratio);
                lineRenderer.positionCount = subList.Count;
                lineRenderer.SetPositions(subList.ToArray());
            }

            remember_subdivision = subdivision;
        }
    }
}
