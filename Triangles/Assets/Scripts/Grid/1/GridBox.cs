using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridBox
{
    Vector3 posTopLeftFront;
    Vector3 posBottomRightBack;
    Box box;

    
    public GridBox(Vector3 posTopLeftFront, Vector3 posBottomRightBack)
    {
        this.posTopLeftFront = posTopLeftFront;
        this.posBottomRightBack = posBottomRightBack;
        this.box = new Box(getCenter(), getDimension());
    }

    Vector3 getDimension()
    {
        Vector3 dimensions = posTopLeftFront - posBottomRightBack;
        return new Vector3(Mathf.Abs(dimensions.x), Mathf.Abs(dimensions.y), Mathf.Abs(dimensions.z));
    }

    Vector3 getCenter()
    {
        return (posTopLeftFront + posBottomRightBack) / 2f;
    }

    public void createChildBoxAndDraw(Vector3 centerSphere, float radiusSphere, int stepMax, int step)
    {
        this.box.createChildBox(centerSphere, radiusSphere, stepMax, step);
        this.box.drawBox(centerSphere, radiusSphere, stepMax);
    }
}
