using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSphere : MonoBehaviour
{
    public float radius;

    // Start is called before the first frame update
    void Start()
    {

    }

    public Vector3 getCenter()
    {
        return this.transform.position;
    }
}
