using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Gen_Bernstein : MonoBehaviour
{
    public List<Vector3> Ps;

    [Range(0f, 1f)]
    public float pas;

    Bernstein bernstein;
    List<Vector3> list;
    LineRenderer lineRenderer;

    void Start()
    {
        bernstein = new Bernstein();

        
    }

    void Update()
    {
        B();
    }


    void B()
    {
        list = new List<Vector3>();

        if (Ps.Count > 1)
        {
            int n = Ps.Count;
            list.Add(Ps[0]);
            List<Vector3> l = new List<Vector3>(Ps.ToArray());
            for (int i = 0; i < n - 1; i++)
            {
                List<Vector3> l_ = new List<Vector3>();
                for (int j = 0; j < l.Count - 1; j++)
                {
                    float t = bernstein.B(i, n, pas * i);
                    Vector3 P = t * l[j] + (1 - t) * l[j + 1];
                    l_.Add(P);
                }
                l = l_;
            }

            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

            foreach (Vector3 v in l)
            {
                list.Add(v);
            }
            list.Add(Ps[Ps.Count - 1]);
        }

        lineRenderer.positionCount = list.Count;
        lineRenderer.SetPositions(list.ToArray());
    }
}
