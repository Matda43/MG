using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBox3 : MonoBehaviour
{
    Vector3 posTopLeftFront;
    Vector3 posBottomRightBack;
    Vector3 dimensions;

    Dictionary<Vector3, Box3> boxes;
    Vector3 nbCubes;

    Dictionary<Vector3, float> goInScene;
    Dictionary<Vector3, GameObject> goList;

    public GridBox3(Vector3 posTopLeftFront, Vector3 posBottomRightBack)
    {
        this.posTopLeftFront = posTopLeftFront;
        this.posBottomRightBack = posBottomRightBack;
        this.dimensions = getDimensions();
        this.goList = new Dictionary<Vector3, GameObject>();
        this.goInScene = new Dictionary<Vector3, float>();
    }

    Vector3 getDimensions()
    {
        Vector3 dimensions = posTopLeftFront - posBottomRightBack;
        return new Vector3(Mathf.Abs(dimensions.x), Mathf.Abs(dimensions.y), Mathf.Abs(dimensions.z));
    }

    public void createGrid(Transform transform, float step, float minWeight, float maxWeight)
    {
        this.nbCubes = new Vector3((int)(dimensions.x / step) + 1, (int)(dimensions.y / step) + 1, (int)(dimensions.z / step) + 1);
        Vector3 centerGrid = (nbCubes / 2 * step);
        this.boxes = new Dictionary<Vector3, Box3>();

        for (int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    Vector3 position = new Vector3(i, j, k);
                    Vector3 realPosition = new Vector3((i + 0.5f) * step, (j + 0.5f) * step, (k + 0.5f) * step) - centerGrid + transform.position;
                    this.boxes.Add(position, new Box3(realPosition, minWeight, maxWeight));
                }
            }
        }
    }

    public void update(GridSphere3 sphere)
    {
        List<Vector3> positions = sphere.getListPosition();
        Vector3 position;
        bool res;
        for (int i = 0; i < nbCubes.x; i++)
        {
            for (int j = 0; j < nbCubes.y; j++)
            {
                for (int k = 0; k < nbCubes.z; k++)
                {
                    position = new Vector3(i, j, k);
                    res = sphere.isInSphere(this.boxes[position].getPosition());
                    if (res)
                    {
                        if (!positions.Contains(position))
                        {
                            this.boxes[position].addWeight(sphere.getWeight());
                            sphere.addPositionInList(position);
                            if (this.goInScene.ContainsKey(position))
                            {
                                this.goInScene[position] = this.boxes[position].getWeight();
                            }
                            else
                            {
                                this.goInScene.Add(position, this.boxes[position].getWeight());
                            }
                        }
                    }
                    else
                    {
                        if (positions.Contains(position))
                        {
                            sphere.removePositionInList(position);
                        }
                    }
                }
            }
        }
    }

    public void draw(float step, Transform transform)
    {
        foreach (var item in goInScene)
        {
            float weight = item.Value;
            Vector3 key = this.boxes[item.Key].getPosition();
            if (goList.ContainsKey(key))
            {
                if (step > weight)
                {
                    GameObject.Destroy(goList[key]);
                    goList.Remove(key);
                }
            }
            else
            {
                if (weight >= step)
                {
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    go.transform.parent = transform;
                    go.transform.position = key;
                    go.transform.localScale = Vector3.one;
                    goList.Add(key, go);
                }
            }
            
        }
    }
}
