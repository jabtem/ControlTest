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

        XY = transform.Find("XYPlane/XY").GetComponent<MeshRenderer>();
        YZ = transform.Find("YZPlnae/YZ").GetComponent<MeshRenderer>();
        XZ = transform.Find("XZPlane/XZ").GetComponent<MeshRenderer>();
    }

    public override void AxisColorChange(GizmoAxis selectAxis)
    {
        switch (selectAxis)
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
            case GizmoAxis.XY:
                X.material.color = Color.yellow;
                Y.material.color = Color.yellow;
                XY.material.color = Color.yellow;
                break;
            case GizmoAxis.XZ:
                X.material.color = Color.yellow;
                Z.material.color = Color.yellow;
                XZ.material.color = Color.yellow;
                break;
            case GizmoAxis.YZ:
                Y.material.color = Color.yellow;
                Z.material.color = Color.yellow;
                YZ.material.color = Color.yellow;
                break;

        }
    }


    public override void AxisColorReset()
    {
        base.AxisColorReset();

        XY.material.color = new Color(0f, 0f, 1f, 0.5f);
        XZ.material.color = new Color(0f, 1f, 0f, 0.5f);
        YZ.material.color = new Color(1f, 0f, 0f, 0.5f);
    }
}
