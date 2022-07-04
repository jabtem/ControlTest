using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformGizmo : GizmoBase
{

    public MeshRenderer XY;
    public MeshRenderer XZ;
    public MeshRenderer YZ;
    protected override void Awake()
    {
        base.Awake();

        Material m = new Material(mat.shader)
        {
            color = new Color(1f, 0f, 0f, 0.5f)
            
        };
        YZ.material = m;

        m = new Material(mat.shader)
        {
            color = new Color(0f, 1f, 0f, 0.5f)
        };
        XZ.material = m;

        m = new Material(mat.shader)
        {
            color = new Color(0f, 0f, 1f, 0.5f)
        };
        XY.material = m;
    }


    protected override void Reset()
    {
        base.Reset();

        XY = transform.Find("TwoAxis/XY").GetComponent<MeshRenderer>();
        YZ = transform.Find("TwoAxis/YZ").GetComponent<MeshRenderer>();
        XZ = transform.Find("TwoAxis/XZ").GetComponent<MeshRenderer>();
    }
}
