using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bernstein
{
    public float B(int i, int n, float u)
    {
        return (factorial(n) / (factorial(i) * factorial(n - 1))) * Mathf.Pow(u, i) * Mathf.Pow((1 - u), (n - 1));
    }

    float factorial(int n)
    {
        float fact = 1;
        for (int x = 1; x <= n; x++)
        {
            fact *= x;
        }
        return fact;
    }
}
