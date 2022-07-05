using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoManager : MonoBehaviour
{

    public Transform target;

    public SnapManager snapManager;
    [SerializeField]
    GameObject rotationGizmo;
    [SerializeField]
    GameObject transformGizmo;
    public TransformType transformType;
    public GizmoType gizmoType;
    public GizmoAxis selectedAxies;
    TargetDirection targetDirection;

    public Vector3 clickPosition;


    private void Reset()
    {
        rotationGizmo = transform.Find("Rotation").gameObject;
    }


    private void OnEnable()
    {
        InputManager.Instance.Input_ObjectMove += GizmoMove;
        InputManager.Instance.Input_ObjectClickUp += SelectInfoReset;
    }

    private void OnDisable()
    {
        InputManager.Instance.Input_ObjectMove -= GizmoMove;
        InputManager.Instance.Input_ObjectClickUp -= SelectInfoReset;
    }

    public void SelectInfoReset()
    {
        clickPosition = Vector3.zero;
        selectedAxies = GizmoAxis.None;
        SelectAxisColorReset();
    }

    private void Update()
    {
        if (snapManager.snapObj != null)
        {
            target = snapManager.snapObj.transform;

            if (transformType.Equals(TransformType.Local))
                transform.rotation = target.rotation;
            else
                transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.position = target.position;

            SetTargetDir(transformType, target);
            AxisPlaneUpdate();
            //Debug.Log(Camera.main.transform.forward * -1);
        }
    }

    public void AxisPlaneUpdate()
    {

        if(transformGizmo.TryGetComponent<TransformGizmo>(out TransformGizmo gizmo))
        {
            Vector3 forward = targetDirection.forward.normalized * 0.25f;
            Vector3 right = targetDirection.right.normalized * 0.25f;
            Vector3 up = targetDirection.up.normalized * 0.25f;

            Vector3 targetPos = target.position;

            //XY

            gizmo.XY.gameObject.transform.position = MinVector(targetPos + right + up, targetPos - right + up, targetPos - right - up, targetPos + right - up);
            //YZ
            gizmo.YZ.gameObject.transform.position = MinVector(targetPos + forward + up, targetPos - forward + up, targetPos - forward - up, targetPos + forward - up);

            //XZ
            gizmo.XZ.gameObject.transform.position = MinVector(targetPos + right + forward, targetPos - right + forward, targetPos - right - forward, targetPos + right - forward);
        }




    }

    Vector3 MinVector(Vector3 a,Vector3 b, Vector3 c, Vector3 d)
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 result = a;
        if ((cameraPosition - b).magnitude < (cameraPosition - result).magnitude)
        {
            result = b;
        }

        if ((cameraPosition - c).magnitude < (cameraPosition - result).magnitude)
        {
            result = c;
        }

        if ((cameraPosition - d).magnitude < (cameraPosition - result).magnitude)
        {
            result = d;
        }


        return result;
    }

    public void ShowGizmo()
    {
        if (snapManager.snapObj == null)
        {
            rotationGizmo.SetActive(false);
            transformGizmo.SetActive(false);
            return;
        }

        switch (gizmoType)
        {
            case GizmoType.Rotation:
                rotationGizmo.SetActive(true);
                transformGizmo.SetActive(false);
                break;
            case GizmoType.Transform:
                rotationGizmo.SetActive(false);
                transformGizmo.SetActive(true);
                break;
        }
    }


    public void SelectAxisColorChagne(GizmoAxis gizmoAxis )
    {
        if (gizmoType.Equals(GizmoType.Rotation))
        {
            if (rotationGizmo.TryGetComponent<GizmoBase>(out GizmoBase gizmoBase))
            {
                gizmoBase.AxisColorChange(gizmoAxis);
            }

        }
        else if (gizmoType.Equals(GizmoType.Transform))
        {
            if (transformGizmo.TryGetComponent<GizmoBase>(out GizmoBase gizmoBase))
            {
                gizmoBase.AxisColorChange(gizmoAxis);
            }
        }
    }

    public void SelectAxisColorReset()
    {
        if (gizmoType.Equals(GizmoType.Rotation))
        {
            if (rotationGizmo.TryGetComponent<GizmoBase>(out GizmoBase gizmoBase))
            {
                gizmoBase.AxisColorReset();

            }

        }
        else if (gizmoType.Equals(GizmoType.Transform))
        {
            if (transformGizmo.TryGetComponent<GizmoBase>(out GizmoBase gizmoBase))
            {
                gizmoBase.AxisColorReset();
            }
        }
    }


    public void SelectedAxis(GizmoAxis axies)
    {
        switch(axies)
        {
            case GizmoAxis.X:
                selectedAxies = GizmoAxis.X;
                SelectAxisColorChagne(selectedAxies);
                break;
            case GizmoAxis.Y:
                selectedAxies = GizmoAxis.Y;
                SelectAxisColorChagne(selectedAxies);
                break;
            case GizmoAxis.Z:
                selectedAxies = GizmoAxis.Z;
                SelectAxisColorChagne(selectedAxies);
                break;
            case GizmoAxis.XY:
                selectedAxies = GizmoAxis.XY;
                SelectAxisColorChagne(selectedAxies);
                break;
            case GizmoAxis.XZ:
                selectedAxies = GizmoAxis.XZ;
                SelectAxisColorChagne(selectedAxies);
                break;
            case GizmoAxis.YZ:
                selectedAxies = GizmoAxis.YZ;
                SelectAxisColorChagne(selectedAxies);
                break;
        }
    }




    public void GizmoMove()
    {
        if (InputManager.Instance.PointerDelta != Vector2.zero && InputManager.Instance.isObjectClick)
        {
            if(snapManager.snapObj != null)
            {



                switch(selectedAxies)
                {
                    case GizmoAxis.X:
                        if (gizmoType.Equals(GizmoType.Rotation))
                            target.Rotate(targetDirection.right, InputManager.Instance.PointerDelta.x - InputManager.Instance.PointerDelta.y, Space.World);
                        else
                        {
                            Plane plane = new Plane(targetDirection.up, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray,out float distance))
                            {
                                Vector3 hitpoint = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = hitpoint-target.position;
                                }


                                Vector3 proj = Vector3.Project(hitpoint-(target.position+clickPosition), targetDirection.right);

                                target.position +=proj;

                                
                            }
  
                        }


                        break;
                    case GizmoAxis.Y:
                        if (gizmoType.Equals(GizmoType.Rotation))
                        {
                            //target.Rotate(targetDirection.up, -InputManager.Instance.PointerDelta.x + InputManager.Instance.PointerDelta.y, Space.World);

                            if (clickPosition.Equals(Vector3.zero))
                            {
                                clickPosition = target.position+ targetDirection.right;
                            }

                            if(Vector3.Dot(targetDirection.up, Vector3.up) >=0)
                            {
                                target.Rotate(targetDirection.up, Vector3.Dot(InputManager.Instance.PointerDelta, clickPosition), Space.World);
                            }
                            else
                            {
                                target.Rotate(targetDirection.up, -Vector3.Dot(InputManager.Instance.PointerDelta, clickPosition), Space.World);
                            }

                        }

                        else
                        //target.Translate(target.up * ((InputManager.Instance.PointerDelta.x + InputManager.Instance.PointerDelta.y)*0.01f), Space.World);
                        {
                            Plane plane = new Plane(targetDirection.forward, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray, out float distance))
                            {
                                Vector3 hitpoint = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = hitpoint - target.position;
                                }


                                Vector3 proj = Vector3.Project(hitpoint - (target.position + clickPosition), targetDirection.up);

                                target.position += proj;


                            }
                        }
                        break;

                    case GizmoAxis.Z:
                        if (gizmoType.Equals(GizmoType.Rotation))
                            target.Rotate(targetDirection.forward, InputManager.Instance.PointerDelta.x - InputManager.Instance.PointerDelta.y,Space.World);
                        else
                        //target.Translate(target.forward * ((InputManager.Instance.PointerDelta.x + InputManager.Instance.PointerDelta.y) * 0.01f), Space.World);
                        {
                            Plane plane = new Plane(targetDirection.up, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray, out float distance))
                            {
                                Vector3 hitpoint = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = hitpoint - target.position;
                                }


                                Vector3 proj = Vector3.Project(hitpoint - (target.position + clickPosition), targetDirection.forward);

                                target.position += proj;


                            }

                        }
                        break;
                    case GizmoAxis.XY:
                        if (gizmoType.Equals(GizmoType.Transform))
                        {
                            Plane plane = new Plane(targetDirection.forward, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray, out float distance))
                            {
                                Vector3 hitpoint = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = hitpoint - target.position;
                                }

                                Vector3 sub =hitpoint- (target.position + clickPosition);

                                target.position += sub;


                            }

                        }
                        break;
                    case GizmoAxis.YZ:
                        if (gizmoType.Equals(GizmoType.Transform))
                        {
                            Plane plane = new Plane(targetDirection.right, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray, out float distance))
                            {
                                Vector3 hitpoint = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = hitpoint - target.position;
                                }

                                Vector3 sub = hitpoint - (target.position + clickPosition);

                                target.position += sub;


                            }

                        }
                        break;
                    case GizmoAxis.XZ:
                        if (gizmoType.Equals(GizmoType.Transform))
                        {
                            Plane plane = new Plane(targetDirection.up, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray, out float distance))
                            {
                                Vector3 hitpoint = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = hitpoint - target.position;
                                }

                                Vector3 sub = hitpoint - (target.position + clickPosition);

                                target.position += sub;


                            }

                        }
                        break;
                }
            }
        }
    }

    void SetTargetDir(TransformType type, Transform target)
    {
        switch (type)
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

}
