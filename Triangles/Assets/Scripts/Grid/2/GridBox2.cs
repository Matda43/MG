using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBox2
{
    Vector3 posTopLeftFront;
    Vector3 posBottomRightBack;
    Vector3 dimensions;
    Dictionary<Vector3, Box2> boxes;
    Vector3 nbCubes;

    public GridBox2(Vector3 posTopLeftFront, Vector3 posBottomRightBack)
    {
        this.posTopLeftFront = posTopLeftFront;
        this.posBottomRightBack = posBottomRightBack;
        this.dimensions = getDimensions();
    }

    Vector3 getDimensions()
    {
        Vector3 dimensions = posTopLeftFront - posBottomRightBack;
        return new Vector3(Mathf.Abs(dimensions.x), Mathf.Abs(dimensions.y), Mathf.Abs(dimensions.z));
    }

    public void createGrid(Vector3 center, float step, float defaultWeight, float minWeight, float maxWeight)
    {
        this.nbCubes = new Vector3((int)(dimensions.x / step) + 1, (int)(dimensions.y / step) + 1, (int)(dimensions.z / step) + 1);
        Vector3 centerGrid = (nbCubes/2 * step);
        this.boxes = new Dictionary<Vector3, Box2>();

        for(int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    Vector3 position = new Vector3(i, j, k);
                    Vector3 realPosition = new Vector3((i + 0.5f * step) * step, (j + 0.5f * step) * step, (k + 0.5f *step) * step) - centerGrid + center;
                    this.boxes.Add(position, new Box2(realPosition, defaultWeight, minWeight, maxWeight));
                }
            }
        }
    }

    bool isInSphere(Vector3 centerSphere, float radius, Vector3 centerCube)
    {
        float squareX = Mathf.Pow((centerCube.x - centerSphere.x), 2);
        float squareY = Mathf.Pow((centerCube.y - centerSphere.y), 2);
        float squareZ = Mathf.Pow((centerCube.z - centerSphere.z), 2);
        float squareR = Mathf.Pow(radius, 2);
        return (squareX + squareY + squareZ) - squareR < 0;
    }

    public void changePosition(Vector3 oldCenter, Vector3 newCenter)
    {
        for (int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    Vector3 position = new Vector3(i, j, k);
                    this.boxes[position].updatePosition(newCenter - oldCenter);
                }
            }
        }
    }

    public void changeDefaultWeight(float newDefaultWeight)
    {
        foreach (Vector3 key in boxes.Keys)
        {
            boxes[key].updateDefaultWeight(newDefaultWeight);
        }
    }

    void deleteGameObject(GridSphere sphere)
    {
        foreach (Vector3 v in sphere.getGOPositionList())
        {
            if (this.boxes.ContainsKey(v))
            {
                this.boxes[v].removeSphere(sphere);
            }
        }
        sphere.clearGOsPosition();
    }

    void assignSphereToAPositionAndUpdateWeight(Vector3 position, GridSphere sphere)
    {
        if (this.boxes.ContainsKey(position))
        {
            bool res = this.boxes[position].addSphere(sphere);
            if (res)
            {
                sphere.addGOPosition(position);
            }
        }
    }

    public void draw(GridSphere sphere, float step)
    {
        deleteGameObject(sphere);

        for (int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    Vector3 position = new Vector3(i, j, k);
                    bool res = isInSphere(sphere.getCenter(), sphere.radius, this.boxes[position].getPosition());                 
                    if (res)
                    {
                        assignSphereToAPositionAndUpdateWeight(position, sphere);
                    }
                }
            }
        }
    }
}
