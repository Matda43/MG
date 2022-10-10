using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box2
{
    Vector3 position;
    float weight;

    public Box2(Vector3 position, float weight)
    {
        this.position = position;
        this.weight = weight;
    }

    public void incWeight(float w)
    {
        this.weight += w;
    }

    public void decWeight(float w)
    {
        this.weight -= w;
    }

    public float getWeight()
    {
        return this.weight;
    }

    public Vector3 getPosition()
    {
        return this.position;
    }
}
