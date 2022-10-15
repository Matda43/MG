using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSphere3 : MonoBehaviour
{
    public float radius;

    public float weight_to_add;
    public float weight_to_delete;

    public bool mode_add;
    public bool mode_delete;

    float weight = 0;


    List<Vector3> positions = new List<Vector3>();

    int cpt = 0;

    void Start()
    {
        if (mode_add != mode_delete)
        {
            if (mode_add)
            {
                weight = weight_to_add;
            }
            else
            {
                weight = weight_to_delete;
            }
        }
    }


    void Update()
    {
        if (cpt > 1)
        {
            if (mode_add && mode_delete || !mode_add && !mode_delete)
            {
                weight = 0;
            }
            else
            {
                if (mode_add)
                {
                    weight = weight_to_add;
                }
                else
                {
                    weight = weight_to_delete;
                }
            }

            cpt = 0;
        }
        else
        {
            cpt++;
        }
    }

    public Vector3 getCenter()
    {
        return this.transform.position;
    }

    public bool isInSphere(Vector3 centerCube)
    {
        float squareX = Mathf.Pow((centerCube.x - this.getCenter().x), 2);
        float squareY = Mathf.Pow((centerCube.y - this.getCenter().y), 2);
        float squareZ = Mathf.Pow((centerCube.z - this.getCenter().z), 2);
        float squareR = Mathf.Pow(radius, 2);
        return (squareX + squareY + squareZ) - squareR < 0;
    }

    public void addPositionInList(Vector3 new_position)
    {
        if (!this.positions.Contains(new_position))
        {
            this.positions.Add(new_position);
        }
    }

    public void removePositionInList(Vector3 position)
    {
        if (this.positions.Contains(position))
        {
            this.positions.Remove(position);
        }
    }

    public List<Vector3> getListPosition()
    {
        return this.positions;
    }

    public float getWeight()
    {
        return this.weight;
    }
}
