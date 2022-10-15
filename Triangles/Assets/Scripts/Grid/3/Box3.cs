using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box3 : MonoBehaviour
{
    Vector3 position;
    float weight;
    float minWeight;
    float maxWeight;

    public Box3(Vector3 position, float minWeight, float maxWeight)
    {
        this.position = position;
        this.weight = 0;
        this.minWeight = minWeight;
        this.maxWeight = maxWeight;
    }

    public void addWeight(float weight_to_add)
    {
        if(weight + weight_to_add > maxWeight)
        {
            weight = maxWeight;
        }else if(weight + weight_to_add < minWeight)
        {
            weight = minWeight;
        }
        else
        {
            weight += weight_to_add;
        }
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
