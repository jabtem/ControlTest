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

}
