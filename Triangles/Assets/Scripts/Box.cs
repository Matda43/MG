using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box
{
    Vector3 center;
    Vector3 dimensions;
    List<Box> boxes;
    int currentStep = 0;
    GameObject g;

    public Box(Vector3 center, Vector3 dimensions)
    {
        this.center = center;
        //Debug.Log(this.center);
        this.dimensions = dimensions;
        this.boxes = new List<Box>();
        this.g = null;
    }

    float getVectorLenght(Vector3 center, Vector3 centerSphere)
    {
        Vector3 distanceFromCenterSphere = center - centerSphere;
        return Mathf.Sqrt(Mathf.Pow(distanceFromCenterSphere.x, 2) + Mathf.Pow(distanceFromCenterSphere.y, 2) + Mathf.Pow(distanceFromCenterSphere.z, 2));
    }

    bool isInSphere(Vector3 centerSphere, float radius, float distanceFromCenterSphere)
    {
        float distanceFromCenterCube = getVectorLenght(this.center, this.center+(dimensions/2));
        return distanceFromCenterSphere + distanceFromCenterCube + distanceFromCenterCube/8 <= radius; // distanceFromCenterCube/8 to correct errors
    }

    public void createChildBox(Vector3 centerSphere, float radiusSphere, int stepMax, int step)
    {
        this.currentStep = step;
        if (step < stepMax)
        {
            if (boxes.Count == 0)
            {
                float distanceFromCenterSphere = getVectorLenght(this.center, centerSphere);
                float distanceFromCenterCube = getVectorLenght(this.center, this.center + (dimensions / 2)); // to correct errors
                if (distanceFromCenterSphere - distanceFromCenterCube <= radiusSphere && !isInSphere(centerSphere, radiusSphere, distanceFromCenterSphere))
                {
                    Vector3 halfBox = dimensions / 2f;
                    Vector3 quarterBox = halfBox / 2f;

                    Vector3 centerTopLeftFront = center + new Vector3(+quarterBox.x, +quarterBox.y, +quarterBox.z);
                    Vector3 centerTopRightFront = center + new Vector3(+quarterBox.x, +quarterBox.y, -quarterBox.z);
                    Vector3 centerTopLeftBack = center + new Vector3(-quarterBox.x, +quarterBox.y, +quarterBox.z);
                    Vector3 centerTopRightBack = center + new Vector3(-quarterBox.x, +quarterBox.y, -quarterBox.z);

                    Vector3 centerBottomLeftFront = center + new Vector3(+quarterBox.x, -quarterBox.y, +quarterBox.z);
                    Vector3 centerBottomRightFront = center + new Vector3(+quarterBox.x, -quarterBox.y, -quarterBox.z);
                    Vector3 centerBottomLeftBack = center + new Vector3(-quarterBox.x, -quarterBox.y, +quarterBox.z);
                    Vector3 centerBottomRightBack = center + new Vector3(-quarterBox.x, -quarterBox.y, -quarterBox.z);

                    this.boxes.Add(new Box(centerTopLeftFront, halfBox));
                    this.boxes.Add(new Box(centerTopRightFront, halfBox));
                    this.boxes.Add(new Box(centerTopLeftBack, halfBox));
                    this.boxes.Add(new Box(centerTopRightBack, halfBox));

                    this.boxes.Add(new Box(centerBottomLeftFront, halfBox));
                    this.boxes.Add(new Box(centerBottomRightFront, halfBox));
                    this.boxes.Add(new Box(centerBottomLeftBack, halfBox));
                    this.boxes.Add(new Box(centerBottomRightBack, halfBox));

                    step = step + 1;

                    foreach (Box box in this.boxes)
                    {
                        box.createChildBox(centerSphere, radiusSphere, stepMax, step);
                    }
                }
            }
            else
            {
                step = step + 1;
                foreach (Box box in this.boxes)
                {
                    box.createChildBox(centerSphere, radiusSphere, stepMax, step);
                }
            }
        }
    }

    public void drawBox(Vector3 centerSphere, float radiusSphere, int stepMax)
    {
        if (currentStep == stepMax)
        {
            float distanceFromCenterSphere = getVectorLenght(this.center, centerSphere);
            if (g == null && distanceFromCenterSphere <= radiusSphere && !isInSphere(centerSphere, radiusSphere, distanceFromCenterSphere))
            {
                GameObject parent = GameObject.Find("Grid");
                g = GameObject.CreatePrimitive(PrimitiveType.Cube);
                g.transform.parent = parent.transform;
                g.transform.position = parent.transform.position + this.center;
                g.transform.localScale = this.dimensions;
            }

            foreach (Box box in boxes)
            {
                box.drawBox(centerSphere, radiusSphere, stepMax);
            }
        }
        else
        {
            if(g != null)
            {
                UnityEngine.Object.Destroy(g);
                g = null;
            }
            foreach (Box box in boxes)
            {
                box.drawBox(centerSphere, radiusSphere, stepMax);
            }
            //this.boxes = new List<Box>(); to show every level of box
        }
    }
}
