using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subdivision
{

    public List<Vector3> subdivide(List<Vector3> listPoint, float ratio1, float ratio2)
    {
        List<Vector3> subListPoint = new List<Vector3>();
        int sizeList = listPoint.Count;
        if (sizeList > 0 && listPoint[0] == listPoint[sizeList-1])
        {
            sizeList--;
        }

        for (int i = 0; i < sizeList; i++)
        {
            Vector3 p0 = listPoint[i];
            Vector3 p1 = listPoint[(i + 1) % sizeList];

            float p0x = p0.x;
            float p0y = p0.y;
            float p0z = p0.z;

            float p1x = p1.x;
            float p1y = p1.y;
            float p1z = p1.z;

            float rx1 = ratio1 * p0x + ratio2 * p1x;
            float ry1 = ratio1 * p0y + ratio2 * p1y;
            float rz1 = ratio1 * p0z + ratio2 * p1z;

            Vector3 v1 = new Vector3(rx1, ry1, rz1);

            float rx2 = ratio2 * p0x + ratio1 * p1x;
            float ry2 = ratio2 * p0y + ratio1 * p1y;
            float rz2 = ratio2 * p0z + ratio1 * p1z;

            Vector3 v2 = new Vector3(rx2, ry2, rz2);

            subListPoint.Add(v1);
            subListPoint.Add(v2);
        }
        if (subListPoint.Count > 0)
        {
            subListPoint.Add(subListPoint[0]);
        }
        return subListPoint;
    }

}
