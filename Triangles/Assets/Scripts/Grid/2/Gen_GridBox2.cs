using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_GridBox2 : MonoBehaviour
{
    public List<GridSphere> spheres;

    [Range(1f, 20f)]
    public float pas;

    GridBox2 gb;
    float pasRemember;

    List<Vector3> centersSphereRemember = new List<Vector3>();
    Vector3 dimension = new Vector3(100, 100, 100);
    Vector3 positionRemember;

    void Start()
    {
        
        gb = new GridBox2(getTopLeftFront(), getBottomRightBack());
        foreach (GridSphere gs in spheres)
        {
            gb.createGrid(this.transform.position, pas);
            gb.draw(gs, pas);
            centersSphereRemember.Add(gs.getCenter());
        }
        this.positionRemember = this.transform.position;
        pasRemember = pas;
        
    }
    
    
    Vector3 getTopLeftFront()
    {
        return this.transform.position + dimension;
    }

    Vector3 getBottomRightBack()
    {
        return this.transform.position - dimension;
    }


    void Update()
    {
        if (pasRemember != pas || positionRemember != this.transform.position)
        {
            gb.createGrid(this.transform.position, pas);
            foreach (GridSphere gs in spheres)
            {
                gb.draw(gs, pas);
            }
            positionRemember = this.transform.position;
            pasRemember = pas;
        }

        for(int i = 0; i < spheres.Count; i++)
        {
            if(spheres[i].getCenter() != centersSphereRemember[i])
            {
                gb.draw(spheres[i], pas);
                centersSphereRemember[i] = spheres[i].getCenter();
            }
        }
    }
}
