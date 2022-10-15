using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSphere : MonoBehaviour
{
    public float radius;
    public float weight;

    List<Vector3> positions = new List<Vector3>();

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
}
