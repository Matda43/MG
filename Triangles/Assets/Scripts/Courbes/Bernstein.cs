using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bernstein
{
    public float B(int i, int n, float t)
    {
        return (factorial(n) / (factorial(i) * factorial(n - i))) * Mathf.Pow(t, i) * Mathf.Pow((1 - t), (n - i));
    }

    float factorial(int n)
    {
        return n > 1 ? n * factorial(n - 1) : 1;
    }
}
