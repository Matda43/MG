using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_GridBox3 : MonoBehaviour
{
    public GridSphere3 sphere;

    public float step;

    public float minWeight;
    public float maxWeight;

    GridBox3 gb;

    Vector3 centerSphereRemember;
    Vector3 dimension = new Vector3(20, 20, 20);

    float pas = 1;

    void Start()
    {

        this.gb = new GridBox3(getTopLeftFront(), getBottomRightBack());
        this.gb.createGrid(this.transform, this.pas, this.minWeight, this.maxWeight);
        this.gb.update(sphere);
        this.gb.draw(this.step, this.transform);
        this.centerSphereRemember = sphere.getCenter();
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
            if(this.sphere.getCenter() != this.centerSphereRemember)
            {
                this.gb.update(this.sphere);
                this.gb.draw(this.step, this.transform);
                this.centerSphereRemember = this.sphere.getCenter();
            }
            cpt = 0;
        }
        else
        {
            cpt++;
        }
    }
}
