using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hermite
{
    public Vector3 p(Vector3 P0, Vector3 P1, Vector3 V0, Vector3 V1, float u)
    {
        float F1 = 2 * Mathf.Pow(u, 3) - 3 * Mathf.Pow(u, 2) + 1;
        float F2 = -2 * Mathf.Pow(u, 3) + 3 * Mathf.Pow(u, 2);
        float F3 = Mathf.Pow(u, 3) - 2 * Mathf.Pow(u, 2) + u;
        float F4 = Mathf.Pow(u, 3) - Mathf.Pow(u, 2);
        return F1 * P0 + F2 * P1 + F3 * V0 + F4 * V1;
    }
}
