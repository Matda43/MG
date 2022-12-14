using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_Bernstein : MonoBehaviour
{
    public GameObject ptsControl;
    public GameObject curve;
    public Material squareColor;

    [Range(0.01f, 1f)]
    public float pas;
    float pasRemember;

    LineRenderer ptsControl_line;
    LineRenderer curve_line;

    Bernstein bernstein;
    GameObject square;

    int positionSquare = 0;
    bool changed = false;

    void Start()
    {
        ptsControl_line = ptsControl.GetComponent<LineRenderer>();
        
        curve_line = curve.GetComponent<LineRenderer>();
        curve_line.startWidth = 0.1f;
        curve_line.endWidth = 0.1f;

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
            Debug.Log("CC");
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            positionSquare = 1;
            Debug.Log("CC");
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            positionSquare = 2;
            Debug.Log("CC");
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            positionSquare = 3;
            Debug.Log("CC");
            square.transform.position = ptsControl_line.GetPosition(positionSquare);
        }

        if(pasRemember != pas)
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
        List<Vector3> list = new List<Vector3>();
        int nbPoint = Mathf.CeilToInt(1 / pas) + 1;
        int n = ptsControl_line.positionCount;

        for (int i = 0; i < nbPoint; i++)
        {
            Vector3 new_point = new Vector3();
            for (int j = 0; j < n; j++)
            {
                float b = bernstein.B(j, n-1, pas * i);
                new_point += ptsControl_line.GetPosition(j) * b;
            }
            list.Add(new_point);
        }
        curve_line.positionCount = list.Count;
        curve_line.SetPositions(list.ToArray());

        square.transform.position = ptsControl_line.GetPosition(positionSquare);
        changed = false;
    }
}
