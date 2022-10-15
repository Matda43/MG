using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box2
{
    Vector3 position;

    float defaultWeight;
    float weight;

    Dictionary<int, float> spheres;

    public Box2(Vector3 position, float defaultWeight)
    {
        this.position = position;
        
        this.defaultWeight = defaultWeight;
        this.weight = defaultWeight;

        this.spheres = new Dictionary<int, float>();
    }

    public void setDefaultWeight(float new_defaultWeight)
    {
        this.weight -= this.defaultWeight;
        this.defaultWeight = new_defaultWeight;
        this.weight += this.defaultWeight;
    }


    public void addSphere(GridSphere sphere)
    {
        int key = sphere.GetInstanceID();
        if (!this.spheres.ContainsKey(key))
        {
            this.spheres.Add(key, sphere.weight);
            this.weight += spheres[key];
        }
    }

    public void removeSphere(GridSphere sphere)
    {
        int key = sphere.GetInstanceID();
        if (this.spheres.ContainsKey(key))
        {
            this.weight -= spheres[key];
            this.spheres.Remove(key);
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
