using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBox2
{
    Vector3 posTopLeftFront;
    Vector3 posBottomRightBack;
    Vector3 dimensions;
    Vector3[,,] boxes;
    Vector3 nbCubes;
    Dictionary<Vector3, float> dictWeight;

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

    public void createGrid(Vector3 center, float step, float defaultWeight)
    {
        this.nbCubes = new Vector3((int)(dimensions.x / step) + 1, (int)(dimensions.y / step) + 1, (int)(dimensions.z / step) + 1);
        Vector3 centerGrid = (nbCubes/2 * step);
        this.boxes = new Vector3[(int)(nbCubes.x), (int)(nbCubes.y), (int)(nbCubes.z)];
        this.dictWeight = new Dictionary<Vector3, float>();

        for(int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    this.boxes[i,j,k] = new Vector3((i + 0.5f) * step, (j + 0.5f) * step, (k + 0.5f) * step) - centerGrid + center;
                    this.dictWeight.Add(new Vector3(i, j, k), defaultWeight);
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


    void deleteGameObject(GridSphere sphere)
    {
        if (sphere.getGOPositionList().Count > 0)
        {
            for (int i = 0; i < sphere.transform.childCount; i++)
            {
                UnityEngine.GameObject.Destroy(sphere.transform.GetChild(i).gameObject);
            }
            foreach (Vector3 v in sphere.getGOPositionList())
            {
                dictWeight[v] -= sphere.weight;
            }
            sphere.clearGOsPosition();
        }
    }

    public void changePosition(Vector3 oldCenter, Vector3 newCenter)
    {
        for (int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    this.boxes[i, j, k] += (newCenter - oldCenter);
                }
            }
        }
    }


    public void changeDefaultWeight(float oldDefaultWeight, float newDefaultWeight)
    {
        foreach(Vector3 key in dictWeight.Keys)
        {
            dictWeight[key] += (newDefaultWeight - oldDefaultWeight);
        }
    }

    public void draw(GridSphere sphere, float step, float minWeight, float maxWeight)
    {
        deleteGameObject(sphere);

        Vector3 scale = new Vector3(step, step, step);
        
        for (int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    bool res = isInSphere(sphere.getCenter(), sphere.radius, this.boxes[i, j, k]);                 
                    if (res)
                    {
                        Vector3 position = new Vector3(i, j, k);
                        dictWeight[position] += sphere.weight;

                        if (dictWeight[position] >= minWeight && dictWeight[position] <= maxWeight)
                        {
                            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            g.transform.parent = sphere.transform;
                            g.transform.position = this.boxes[i, j, k];
                            g.transform.localScale = scale;
                        }
                        sphere.addGOPosition(position);
                    }
                }
            }
        }
    }
}
