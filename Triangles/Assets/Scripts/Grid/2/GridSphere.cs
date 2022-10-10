using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSphere : MonoBehaviour
{
    public float radius;
    public float weight;
    List<Vector3> gosPosition = new List<Vector3>();

    public Vector3 getCenter()
    {
        return this.transform.position;
    }

    public List<Vector3> getGOPositionList()
    {
        return this.gosPosition;
    }

    public void addGOPosition(Vector3 pos)
    {
        if (!gosPosition.Contains(pos))
        {
            gosPosition.Add(pos);
        }
    }

    public void clearGOsPosition()
    {
        this.gosPosition = new List<Vector3>();
    }
}
