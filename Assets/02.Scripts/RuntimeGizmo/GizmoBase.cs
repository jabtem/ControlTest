using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoBase : MonoBehaviour
{
    public Material mat;

    public MeshRenderer X;
    public MeshRenderer Y;
    public MeshRenderer Z;


    protected virtual void Awake()
    {
        Material m = new Material(mat.shader)
        {
            color = Color.red
        };
        X.material = m;

        m = new Material(mat.shader)
        {
            color = Color.green
        };
        Y.material = m;

        m = new Material(mat.shader)
        {
            color = Color.blue
        };
        Z.material = m;


    }

    protected virtual void Reset()
    {
        X = transform.Find("X").GetComponent<MeshRenderer>();
        Y = transform.Find("Y").GetComponent<MeshRenderer>();
        Z = transform.Find("Z").GetComponent<MeshRenderer>();
    }

    public virtual void AxisColorChange(GizmoAxis selectAxis)
    {
        switch(selectAxis)
        {
            case GizmoAxis.X:
                X.material.color = Color.yellow;
                break;
            case GizmoAxis.Y:
                Y.material.color = Color.yellow;
                break;
            case GizmoAxis.Z:
                Z.material.color = Color.yellow;
                break;
        }
    }

    public virtual void AxisColorReset()
    {
        X.material.color = Color.red;
        Y.material.color = Color.green;
        Z.material.color = Color.blue;
    }

}
