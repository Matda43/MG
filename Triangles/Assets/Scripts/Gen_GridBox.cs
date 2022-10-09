using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_GridBox : MonoBehaviour
{
    public Vector3 center;
    public float radius;
    [Range(0,6)]
    public int precision;

    GridBox gb;
    int precisionRemember;

    void Start()
    {
        gb = new GridBox(getTopLeftFront(), getBottomRightBack());
        gb.createChildBoxAndDraw(center, radius, precision, 0);
        precisionRemember = precision;
    }

    Vector3 getTopLeftFront()
    {
        return new Vector3(center.x + radius, center.y + radius, center.z + radius);
    }

    Vector3 getBottomRightBack()
    {
        return new Vector3(center.x - radius, center.y - radius, center.z - radius);
    }


    void Update()
    {
        if(precisionRemember != precision)
        {
            gb.createChildBoxAndDraw(center, radius, precision, 0);
            precisionRemember = precision;
        }
    }
}
