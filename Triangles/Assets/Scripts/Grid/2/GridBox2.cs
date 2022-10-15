using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GridBox2
{
    Vector3 posTopLeftFront;
    Vector3 posBottomRightBack;
    Vector3 dimensions;

    Dictionary<Vector3, Box2> boxes;
    Vector3 nbCubes;

    Dictionary<Vector3, float> goInScene;
    Dictionary<Vector3, GameObject> goList;

    public GridBox2(Vector3 posTopLeftFront, Vector3 posBottomRightBack)
    {
        this.posTopLeftFront = posTopLeftFront;
        this.posBottomRightBack = posBottomRightBack;
        this.dimensions = getDimensions();
        this.goList = new Dictionary<Vector3, GameObject>();
    }

    Vector3 getDimensions()
    {
        Vector3 dimensions = posTopLeftFront - posBottomRightBack;
        return new Vector3(Mathf.Abs(dimensions.x), Mathf.Abs(dimensions.y), Mathf.Abs(dimensions.z));
    }

    public void createGrid(Transform transform, float step, float defaultWeight)
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
                    Vector3 realPosition = new Vector3((i + 0.5f) * step, (j + 0.5f) * step, (k + 0.5f) * step) - centerGrid + transform.position;
                    this.boxes.Add(position, new Box2(realPosition, defaultWeight));    
                }
            }
        }
    }

    public void changeDefaultWeight(float newDefaultWeight)
    {
        foreach (Vector3 key in boxes.Keys)
        {
            this.boxes[key].setDefaultWeight(newDefaultWeight);
        }
    }

    public void update(GridSphere sphere)
    {
        this.goInScene = new Dictionary<Vector3, float>();
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
                            this.boxes[position].addSphere(sphere);
                            sphere.addPositionInList(position);
                            this.goInScene.Add(this.boxes[position].getPosition(), this.boxes[position].getWeight());
                        }
                    }
                    else
                    {
                        if (positions.Contains(position))
                        {
                            this.boxes[position].removeSphere(sphere);
                            sphere.removePositionInList(position);
                            this.goInScene.Add(this.boxes[position].getPosition(), this.boxes[position].getWeight());
                        }
                    }
                }
            }
        }
    }

    public void draw(float minWeight, float maxWeight, Transform transform)
    {
        foreach(var item in goInScene)
        {

            float weight = item.Value;

            if (goList.ContainsKey(item.Key))
            {
                if (minWeight > weight || weight > maxWeight)
                {
                    GameObject.Destroy(goList[item.Key]);
                    goList.Remove(item.Key);
                }
            }            
            else
            {
                if (weight >= minWeight && weight <= maxWeight)
                {
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    go.transform.parent = transform;
                    go.transform.position = item.Key;
                    go.transform.localScale = Vector3.one;
                    goList.Add(item.Key, go);
                }
            }
        }
    }
}
