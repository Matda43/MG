using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_GridBox2 : MonoBehaviour
{
    public List<GridSphere> spheres;

    public float defaultWeight;
    public float minWeight;
    public float maxWeight;

    GridBox2 gb;

    List<Vector3> centersSphereRemember = new List<Vector3>();
    Vector3 dimension = new Vector3(20, 20, 20);

    float defaultWeightRemember;
    float minWeightRemember;
    float maxWeightRemember;
    float pas = 1;

    void Start()
    {
        
        this.gb = new GridBox2(getTopLeftFront(), getBottomRightBack());
        this.gb.createGrid(this.transform, this.pas, this.defaultWeight);
        foreach (GridSphere gs in spheres)
        {
            this.gb.update(gs);
            this.gb.draw(this.minWeight, this.maxWeight, this.transform);
            this.centersSphereRemember.Add(gs.getCenter());
        }
        this.defaultWeightRemember = defaultWeight;
        this.minWeightRemember = this.minWeight;
        this.maxWeightRemember = this.maxWeight;
    }
    
    
    Vector3 getTopLeftFront()
    {
        return this.transform.position + this.dimension;
    }

    Vector3 getBottomRightBack()
    {
        return this.transform.position - this.dimension;
    }


    int cpt = 0;

    void Update()
    {
        if (cpt > 1)
        {
            bool change = false;
            if (this.defaultWeightRemember != this.defaultWeight)
            {
                this.gb.changeDefaultWeight(this.defaultWeight);
                this.defaultWeightRemember = defaultWeight;
                change = true;
            }

            if(this.minWeightRemember != this.minWeight)
            {
                this.minWeightRemember = this.minWeight;
                change = true;
            }

            if (this.maxWeightRemember != this.maxWeight)
            {
                this.maxWeightRemember = this.maxWeight;
                change = true;
            }

            for (int i = 0; i < this.spheres.Count; i++)
            {
                if (change || this.spheres[i].getCenter() != this.centersSphereRemember[i])
                {
                    this.gb.update(this.spheres[i]);
                    this.gb.draw(this.minWeight, this.maxWeight, this.transform);
                    this.centersSphereRemember[i] = this.spheres[i].getCenter();
                }
            }

            cpt = 0;
        }
        else
        {
            cpt++;
        }
    }
}
