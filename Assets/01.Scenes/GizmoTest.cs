using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;



struct TargetDirection
{
    public Vector3 right;
    public Vector3 up;
    public Vector3 forward;
}

public class GizmoTest : MonoBehaviour
{
    public Material mat;

    public SnapManager snapManager;

    public TransformType transformType;
    public GizmoType gizmoType;

    public bool onSegment;
    public bool intersects;
    float xDistance;
    float yDistance;
    float zDistance;

    TargetDirection targetDirection;
    private void OnEnable()
    {
        //RenderPipelineManager.endCameraRendering += EndCamRender;
    }

    private void OnDisable()
    {
        //RenderPipelineManager.endCameraRendering -= EndCamRender;
    }


    private void EndCamRender(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }
    private void Update()
    {
        //if (snapManager.snapObj != null)
        //{
        //    Transform target = snapManager.snapObj.transform;
        //    Vector3 t = Vector3.Project(target.position - snapManager.rayOrigin,snapManager.rayDir)+snapManager.rayOrigin;

        //    Debug.DrawRay(snapManager.rayOrigin, target.position - snapManager.rayOrigin,Color.yellow);
        //    Debug.DrawLine(snapManager.rayOrigin, t, Color.red);
        //    Debug.DrawLine(t, target.position, Color.green);


        //    Debug.Log((t - target.position).magnitude);
        //}
        //    Debug.DrawLine(snapManager.rayOrigin, snapManager.rayOrigin+snapManager.rayDir,Color.blue);


     }




    private void OnPostRender()
    {

        
        if(snapManager.snapObj != null)
        {
            Transform target = snapManager.snapObj.transform;
            //DrawRuntimeGizmos(gizmoType);
            GL.PushMatrix();
            mat.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Color(Color.green);
            //float degRad = Mathf.PI / 180;
            for (float theta = 0.0f; theta < (Mathf.PI*2); theta += 0.001f)
            {
                Vector3 ci = (new Vector3(Mathf.Cos(theta) * 3f + target.position.x, target.position.y, Mathf.Sin(theta) * 3f +target.position.z));

                ci = Quaternion.AngleAxis(target.eulerAngles.z, target.forward) * ci;
                ci = Quaternion.AngleAxis(target.eulerAngles.x, target.right) * ci;

                GL.Vertex3(ci.x, ci.y, ci.z);
            }


            GL.Color(Color.blue);
            //float degRad = Mathf.PI / 180;
            for (float theta = 0.0f; theta < ( Mathf.PI*2f); theta += 0.001f)
            {
                Vector3 ci = (new Vector3(Mathf.Cos(theta) * 3f + target.position.x, Mathf.Sin(theta) * 3f + target.position.y, target.position.z));
                //Quaternion q = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                //ci = q * ci;
                ci = Quaternion.AngleAxis(target.eulerAngles.y, Vector3.up) * ci;
                ci = Quaternion.AngleAxis(target.eulerAngles.x, target.right) * ci;

                GL.Vertex3(ci.x, ci.y, ci.z);
            }
            GL.Color(Color.red);
            //float degRad = Mathf.PI / 180;
            for (float theta = 0.0f; theta < (Mathf.PI*2f); theta += 0.001f)
            {
                Vector3 ci = (new Vector3(target.position.x, Mathf.Sin(theta) * 3f + target.position.y, Mathf.Cos(theta) * 3f + target.position.z));

                ci = Quaternion.AngleAxis(target.eulerAngles.y, Vector3.up) * ci;
                ci = Quaternion.AngleAxis(target.eulerAngles.z, target.forward) * ci;
                GL.Vertex3(ci.x, ci.y, ci.z);
            }


            GL.End();
            GL.PopMatrix();
        }

    }


    //벡터 사이 교차여부 확인
    public float LIneIntersection(Vector3 p1Start, Vector3 p1End, Vector3 p2Start, Vector3 p2End)
    {
        Vector3 l1 = p1End - p1Start; //r
        Vector3 l2 = p2End - p2Start; //s
        Vector3 l3 = p1Start - p2Start; //q

        //떨어진 두벡터 사이 수직하는 지점의 좌표 P0 = Line1 , P1 = Line2


        float dot_L3_L1 = Vector3.Dot(l3, l1);
        float dot_L3_L2 = Vector3.Dot(l3, l2);
        float dot_L2_L1 = Vector3.Dot(l2, l1);
        float dot_L1_L1 = Vector3.Dot(l1, l1);// 벡터 l1 크기 제곱
        float dot_L2_L2 = Vector3.Dot(l2, l2);// 벡터 l2 크기 제곱

        float t = ((dot_L3_L2 * dot_L2_L1) - (dot_L3_L1 * dot_L2_L2)) / ((dot_L1_L1 * dot_L2_L2) - (dot_L2_L1 * dot_L2_L1));
        float u = (dot_L3_L2 + t * dot_L2_L1) / dot_L2_L2;

        Vector3 p0 = p1Start + t * l1;
        Vector3 p1 = p2Start + u * l2;

        //서로 선분위에있는지
        onSegment = false;

        //교차여부(선끼리 닿았는지)
        intersects = false;


        if (0 <= t && t <= 1 && 0 <= u && u <= 1)
            onSegment = true;
        if ((p0 - p1).sqrMagnitude <= 0.01f)
            intersects = true;

        if (onSegment && intersects)
            return (p0 - p1).sqrMagnitude;
        else
            return 999f;


    }

    public void DrawRuntimeGizmos(GizmoType type)
    {
        Transform target = snapManager.snapObj.transform;
        Debug.DrawLine(target.position, target.position + target.right, Color.red);

        mat.SetPass(0);


        switch(type)
        {
            case GizmoType.Transform:
                DrawTransformGizmo(target);
                break;
        }


    }

    private void DrawTransformGizmo(Transform target)
    {

        SetTargetDir(transformType, target);
        //X LINE
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(target.position);
        GL.Vertex(target.position + targetDirection.right);


        //Z LINE
        GL.Color(Color.blue);
        GL.Vertex(target.position);
        GL.Vertex(target.position + targetDirection.forward);

        //Y LINE
        GL.Color(Color.green);
        GL.Vertex(target.position);
        GL.Vertex(target.position + targetDirection.up);
        GL.End();

        GL.Begin(GL.TRIANGLES);
        //X LINE TRIANGLE
        GL.Color(Color.red);
        GL.Vertex(target.position + targetDirection.right - targetDirection.right * 0.1f + Vector3.up * 0.05f);
        GL.Vertex(target.position + targetDirection.right);
        GL.Vertex(target.position + targetDirection.right - targetDirection.right * 0.1f - Vector3.up * 0.05f);

        //Zline TRIANGLE
        GL.Color(Color.blue);
        GL.Vertex(target.position + targetDirection.forward - targetDirection.forward * 0.1f + Vector3.left * 0.05f);
        GL.Vertex(target.position + targetDirection.forward);
        GL.Vertex(target.position + targetDirection.forward - targetDirection.forward * 0.1f + Vector3.right * 0.05f);

        //Y LINE TRIANGLE
        GL.Color(Color.green);
        GL.Vertex(target.position + targetDirection.up - targetDirection.up * 0.1f + Vector3.left * 0.05f);
        GL.Vertex(target.position + targetDirection.up);
        GL.Vertex(target.position + targetDirection.up - targetDirection.up * 0.1f + Vector3.right * 0.05f);
        GL.End();

        xDistance = LIneIntersection(target.position, target.position + targetDirection.right, snapManager.rayOrigin, snapManager.rayOrigin + snapManager.rayDir * 100f);
        yDistance = LIneIntersection(target.position, target.position + targetDirection.up, snapManager.rayOrigin, snapManager.rayOrigin + snapManager.rayDir * 100f);
        zDistance = LIneIntersection(target.position, target.position + targetDirection.forward, snapManager.rayOrigin, snapManager.rayOrigin + snapManager.rayDir * 100f);

        Debug.Log("X : " + xDistance);
        Debug.Log("Y : " + yDistance);
        Debug.Log("Z : " + zDistance);

    }

    void SetTargetDir(TransformType type, Transform target)
    {
        switch(type)
        {
            case TransformType.Golbal:
                targetDirection.right = Vector3.right;
                targetDirection.up = Vector3.up;
                targetDirection.forward = Vector3.forward;
                break;
            case TransformType.Local:
                targetDirection.right = target.right;
                targetDirection.up = target.up;
                targetDirection.forward = target.forward;
                break;
        }
    }

    //public void calcIntersection(Vector3 a, Vector3 b, Vector3 c, Vector3 d, out Vector3 p0, out Vector3 p1, out bool onSegment, out bool intersects)
    //{
    //    Vector3 r = b - a;
    //    Vector3 s = d - c;
    //    Vector3 q = a - c;

    //    float dotqr = Vector3.Dot(q, r);
    //    float dotqs = Vector3.Dot(q, s);
    //    float dotrs = Vector3.Dot(r, s);
    //    float dotrr = Vector3.Dot(r, r);
    //    float dotss = Vector3.Dot(s, s);

    //    float denom = dotrr * dotss - dotrs * dotrs;
    //    float numer = dotqs * dotrs - dotqr * dotss;

    //    float t = numer / denom;
    //    float u = (dotqs + t * dotrs) / dotss;

    //    // The two points of intersection
    //    p0 = a + t * r;
    //    p1 = c + u * s;

    //    // Is the intersection occuring along both line segments and does it intersect
    //    onSegment = false;
    //    intersects = false;
    //    if (0 <= t && t <= 1 && 0 <= u && u <= 1) onSegment = true;
    //    if ((p0 - p1).sqrMagnitude <= 0.01f) intersects = true;
    //}
}
