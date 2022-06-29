using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoBase : MonoBehaviour
{
    public Material mat;

    public MeshRenderer X;
    public MeshRenderer Y;
    public MeshRenderer Z;


    private void Awake()
    {
        Material m = new Material(mat.shader)
        {
            color = Color.red
        };
        X.material = m;

        Material m1 = new Material(mat.shader)
        {
            color = Color.green
        };
        Y.material = m1;

        Material m2 = new Material(mat.shader)
        {
            color = Color.blue
        };
        Z.material = m2;


    }

}
