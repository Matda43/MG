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
    Vector3 centerGrid;
    Dictionary<Vector3, int> dictWeight;
    Dictionary<int, List<Vector3>> dictGOCube;

    public GridBox2(Vector3 posTopLeftFront, Vector3 posBottomRightBack)
    {
        this.posTopLeftFront = posTopLeftFront;
        this.posBottomRightBack = posBottomRightBack;
        this.dimensions = getDimensions();
        this.dictGOCube = new Dictionary<int, List<Vector3>>();
    }

    Vector3 getDimensions()
    {
        Vector3 dimensions = posTopLeftFront - posBottomRightBack;
        return new Vector3(Mathf.Abs(dimensions.x), Mathf.Abs(dimensions.y), Mathf.Abs(dimensions.z));
    }

    public void createGrid(Vector3 center, float step)
    {
        this.nbCubes = new Vector3((int)(dimensions.x / step) + 1, (int)(dimensions.y / step) + 1, (int)(dimensions.z / step) + 1);
        this.centerGrid = (nbCubes/2 * step);
        this.boxes = new Vector3[(int)(nbCubes.x), (int)(nbCubes.y), (int)(nbCubes.z)];
        this.dictWeight = new Dictionary<Vector3, int>();
        for(int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    this.boxes[i,j,k] = new Vector3((i + 0.5f) * step, (j + 0.5f) * step, (k + 0.5f) * step) - centerGrid + center;
                    this.dictWeight.Add(this.boxes[i, j, k], 10);
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
        for(int i = 0; i < sphere.transform.childCount; i++)
        {
            UnityEngine.GameObject.Destroy(sphere.transform.GetChild(i).gameObject);
        }
        foreach(Vector3 v in dictGOCube[sphere.GetInstanceID()])
        {
            dictWeight[v] -= 5;
        }
    }

    public void draw(GridSphere sphere, float step)
    {
        if (!dictGOCube.ContainsKey(sphere.GetInstanceID()))
        {
            dictGOCube.Add(sphere.GetInstanceID(), new List<Vector3>());
        }
        else
        {
            deleteGameObject(sphere);
            dictGOCube[sphere.GetInstanceID()] = new List<Vector3>();
        }

        for (int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    bool res1 = isInSphere(sphere.getCenter(), sphere.radius, this.boxes[i, j, k]);                 
                    if (res1)
                    {
                        dictWeight[this.boxes[i, j, k]] += 5;
                        Debug.Log(dictWeight[this.boxes[i, j, k]]);
                        if (dictWeight[this.boxes[i, j, k]] > 15)
                        {
                            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            g.transform.parent = sphere.transform;
                            g.transform.position = this.boxes[i, j, k];
                            g.transform.localScale = new Vector3(step, step, step);
                            dictGOCube[sphere.GetInstanceID()].Add(this.boxes[i, j, k]);
                        }
                    }
                }
            }
        }
    }
}
