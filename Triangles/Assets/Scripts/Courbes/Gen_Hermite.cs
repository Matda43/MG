using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Gen_Hermite : MonoBehaviour
{
    public Vector3 P0;
    Vector3 P0_remember;

    public Vector3 P1;
    Vector3 P1_remember;

    public Vector3 V0;
    Vector3 V0_remember;

    public Vector3 V1;
    Vector3 V1_remember;

    [Range(0.01f, 1f)]
    public float pas;
    float pas_remember;

    LineRenderer lineRenderer;
    Hermite hermite;
    List<Vector3> list;

    bool change = false;

    void Start()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        this.hermite = new Hermite();
        p();
        P0_remember = P0;
        P1_remember = P1;
        V0_remember = V0;
        V1_remember = V1;
        pas_remember = pas;
    }

    void Update()
    {
        change = false;
        if(P0_remember != P0)
        {
            P0_remember = P0;
            change = true;
        }
        if (P1_remember != P1)
        {
            P1_remember = P1;
            change = true;
        }
        if (V0_remember != V0)
        {
            V0_remember = V0;
            change = true;
        }
        if (V1_remember != V1)
        {
            V1_remember = V1;
            change = true;
        }
        if(pas_remember != pas)
        {
            pas_remember = pas;
            change = true;
        }
        if (change)
        {
            p();
        }
         
    }

    void p()
    {
        list = new List<Vector3>();
        list.Add(P0);

        int nbPoint = (int)(1 / pas);
        for (int i = 0; i < nbPoint; i++)
        {
            Vector3 pu = this.hermite.p(P0, P1, V0, V1, pas*i);
            list.Add(pu);
        }
        list.Add(P1);
        lineRenderer.positionCount = list.Count;
        lineRenderer.SetPositions(list.ToArray());
    }
}
