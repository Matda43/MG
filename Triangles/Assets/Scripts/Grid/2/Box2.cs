using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box2
{
    Vector3 position;
    GameObject g = null;
    float defaultWeight;
    List<GridSphere> spheres;
    float weight;

    float minWeight;
    float maxWeight;

    public Box2(Vector3 position, float defaultWeight, float minWeight, float maxWeight)
    {
        this.position = position;
        this.defaultWeight = defaultWeight;
        this.weight = defaultWeight;
        this.spheres = new List<GridSphere>();
        this.minWeight = minWeight;
        this.maxWeight = maxWeight;
    }

    public void updatePosition(Vector3 new_position)
    {
        this.position += new_position;
    }

    public void updateDefaultWeight(float new_defaultWeight)
    {
        this.weight += (new_defaultWeight - this.defaultWeight);
        this.defaultWeight = new_defaultWeight;
    }


    public bool addSphere(GridSphere sphere)
    {
        if (!this.spheres.Contains(sphere))
        {
            this.spheres.Add(sphere);
            this.weight += sphere.weight;
            if (this.weight > maxWeight)
            {
                if (g != null)
                {
                    GameObject.Destroy(g);
                    g = null;
                }
            }
            else
            {
                if (g == null)
                {
                    GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    g.transform.parent = sphere.transform;
                    g.transform.position = this.position;
                    g.transform.localScale = Vector3.one;
                }
            }
            return true;
        }
        return false;
    }

    public void removeSphere(GridSphere sphere)
    {
        bool res = this.spheres.Remove(sphere);
        if (res)
        {
            this.weight -= sphere.weight;
            if (this.weight < this.minWeight)
            {
                if (g != null)
                {
                    GameObject.Destroy(g);
                    g = null;
                }
            }
            else
            {
                if(g == null)
                {
                    GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    g.transform.parent = sphere.transform;
                    g.transform.position = this.position;
                    g.transform.localScale = Vector3.one;
                }
            }
        }
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
