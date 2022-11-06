using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_BernsteinCubique : MonoBehaviour
{
    public GameObject ptsControl;
    public GameObject curve1;
    public GameObject curve2;
    public Material squareColor;

    [Range(0.01f, 1f)]
    public float pas;
    float pasRemember;

    LineRenderer ptsControl_line;
    LineRenderer curve_line1;
    LineRenderer curve_line2;

    Bernstein bernstein;
    GameObject square;

    int positionSquare = 0;
    bool changed = false;

    void Start()
    {
        ptsControl_line = ptsControl.GetComponent<LineRenderer>();
        
        curve_line1 = curve1.GetComponent<LineRenderer>();
        curve_line1.startWidth = 0.1f;
        curve_line1.endWidth = 0.1f;

        curve_line2 = curve2.GetComponent<LineRenderer>();
        curve_line2.startWidth = 0.1f;
        curve_line2.endWidth = 0.1f;

        square = GameObject.CreatePrimitive(PrimitiveType.Cube);
        square.transform.position = ptsControl_line.GetPosition(positionSquare);
        square.transform.localScale = Vector3.one * 0.1f;
        square.GetComponent<Renderer>().material = squareColor;

        bernstein = new Bernstein();
        B();
        pasRemember = pas;
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Z))
        {
            Vector3 new_position = ptsControl_line.GetPosition(positionSquare);
            new_position.y += 0.01f;
            ptsControl_line.SetPosition(positionSquare, new_position);
            changed = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 new_position = ptsControl_line.GetPosition(positionSquare);
            new_position.y -= 0.01f;
            ptsControl_line.SetPosition(positionSquare, new_position);
            changed = true;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            Vector3 new_position = ptsControl_line.GetPosition(positionSquare);
            new_position.x += 0.01f;
            ptsControl_line.SetPosition(positionSquare, new_position);
            changed = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 new_position = ptsControl_line.GetPosition(positionSquare);
            new_position.x -= 0.01f;
            ptsControl_line.SetPosition(positionSquare, new_position);
            changed = true;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            positionSquare = 0;
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            positionSquare = 1;
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            positionSquare = 2;
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            positionSquare = 3;
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            positionSquare = 4;
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            positionSquare = 5;
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            positionSquare = 6;
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }

        if (pasRemember != pas)
        {
            pasRemember = pas;
            changed = true;
        }

        if (changed)
        {
            B();
        }
    }


    void B()
    {
        List<Vector3> list1 = new List<Vector3>();
        List<Vector3> list2 = new List<Vector3>();

        int nbPoint = Mathf.CeilToInt(1 / pas) + 1;
        int n = ptsControl_line.positionCount;

        for (int i = 0; i < nbPoint; i++)
        {
            Vector3 new_point1 = new Vector3();
            for (int j = 0; j < 4; j++)
            {
                float b = bernstein.B(j, 4 - 1, pas * i);
                new_point1 += ptsControl_line.GetPosition(j) * b;
            }
            list1.Add(new_point1);

            Vector3 new_point2 = new Vector3();
            for (int j = 3; j < n; j++)
            {
                float b = bernstein.B(j - 3, 4 - 1, pas * i);
                new_point2 += ptsControl_line.GetPosition(j) * b;
            }
            list2.Add(new_point2);
        }
        curve_line1.positionCount = list1.Count;
        curve_line1.SetPositions(list1.ToArray());

        curve_line2.positionCount = list2.Count;
        curve_line2.SetPositions(list2.ToArray());

        square.transform.position = ptsControl_line.GetPosition(positionSquare);
        changed = false;
    }
}
