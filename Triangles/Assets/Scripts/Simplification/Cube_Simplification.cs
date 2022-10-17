using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Simplification
{
    Vector3 realPosition;
    float size;
    Vector3 posTopLeftFront;
    Vector3 posBottomRightBack;

    int indiceFirstPoint;
    public Cube_Simplification(Vector3 realPosition, float size)
    {
        this.realPosition = realPosition;
        this.size = size;
        this.posTopLeftFront = this.realPosition + new Vector3(size / 2, size / 2, size / 2) ;
        this.posBottomRightBack = this.realPosition - new Vector3(size / 2, size / 2, size / 2);
        this.indiceFirstPoint = -1;
    }

    public bool isInBox(Vector3 pos)
    {
        return pos.x <= posTopLeftFront.x && pos.x > posBottomRightBack.x
            && pos.y <= posTopLeftFront.y && pos.y > posBottomRightBack.y
            && pos.z <= posTopLeftFront.z && pos.z > posBottomRightBack.z;
    }

    public void setFirstPointIndice(int new_indice)
    {
        this.indiceFirstPoint = new_indice;
    }

    public int getFirstPosition()
    {
        return this.indiceFirstPoint;
    }

    public bool hasAPoint()
    {
        return indiceFirstPoint > 0;
    }
}

