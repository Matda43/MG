using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_GridBox2 : MonoBehaviour
{
    public List<GridSphere> spheres;

    [Range(1f, 20f)]
    public float pas;

    public float defaultWeight;
    public float minWeight;
    public float maxWeight;

    GridBox2 gb;
    float pasRemember;

    List<Vector3> centersSphereRemember = new List<Vector3>();
    Vector3 dimension = new Vector3(100, 100, 100);
    Vector3 positionRemember;

    float defaultWeightRemember;
    float minWeightRemember;
    float maxWeightRemember;


    void Start()
    {
        
        this.gb = new GridBox2(getTopLeftFront(), getBottomRightBack());
        this.gb.createGrid(this.transform.position, this.pas, this.defaultWeight);
        foreach (GridSphere gs in spheres)
        {
            this.gb.draw(gs, this.pas, this.minWeight, this.maxWeight);
            this.centersSphereRemember.Add(gs.getCenter());
        }
        this.positionRemember = this.transform.position;
        this.pasRemember = pas;
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


    void Update()
    {
        if (this.pasRemember != this.pas)
        {
            this.gb.createGrid(this.transform.position, this.pas , this.defaultWeight);
            foreach (GridSphere gs in this.spheres)
            {
                this.gb.draw(gs, this.pas, this.minWeight, this.maxWeight);
            }
            this.positionRemember = this.transform.position;
            this.pasRemember = this.pas;
        }

        if(this.positionRemember != this.transform.position)
        {
            this.gb.changePosition(this.positionRemember, this.transform.position);
            foreach (GridSphere gs in this.spheres)
            {
                this.gb.draw(gs, this.pas, this.minWeight, this.maxWeight);
            }
            this.positionRemember = this.transform.position;
        }

        if (this.defaultWeightRemember != this.defaultWeight)
        {
            this.gb.changeDefaultWeight(this.defaultWeightRemember, this.defaultWeight);
            foreach (GridSphere gs in this.spheres)
            {
                this.gb.draw(gs, this.pas, this.minWeight, this.maxWeight);
            }
            this.defaultWeightRemember = defaultWeight;
        }

        for (int i = 0; i < this.spheres.Count; i++)
        {
            if(this.spheres[i].getCenter() != this.centersSphereRemember[i] || this.minWeightRemember != this.minWeight || this.maxWeightRemember != this.maxWeight)
            {
                this.gb.draw(this.spheres[i], this.pas, this.minWeight, this.maxWeight);
                this.centersSphereRemember[i] = this.spheres[i].getCenter();
                this.minWeightRemember = this.minWeight;
                this.maxWeightRemember = this.maxWeight;
            }
        }
    }
}
