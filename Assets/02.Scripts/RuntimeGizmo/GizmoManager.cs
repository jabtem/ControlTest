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
        }
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
                SetTargetDir(transformType, snapManager.snapObj.transform);


                switch(selectedAxies)
                {
                    case GizmoAxis.X:
                        if (gizmoType.Equals(GizmoType.Rotation))
                            target.Rotate(target.right, InputManager.Instance.PointerDelta.x - InputManager.Instance.PointerDelta.y, Space.World);
                        else
                        {
                            Plane plane = new Plane(target.up, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray,out float distance))
                            {
                                Vector3 hitpoint = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = hitpoint-target.position;
                                }


                                Vector3 proj = Vector3.Project(hitpoint-(target.position+clickPosition), target.right);

                                target.position +=proj;

                                
                            }
  
                        }


                        break;
                    case GizmoAxis.Y:
                        if (gizmoType.Equals(GizmoType.Rotation))
                            target.Rotate(target.up, InputManager.Instance.PointerDelta.x - InputManager.Instance.PointerDelta.y ,Space.World);
                        else
                        //target.Translate(target.up * ((InputManager.Instance.PointerDelta.x + InputManager.Instance.PointerDelta.y)*0.01f), Space.World);
                        {
                            Plane plane = new Plane(target.forward, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray, out float distance))
                            {
                                Vector3 hitpoint = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = hitpoint - target.position;
                                }


                                Vector3 proj = Vector3.Project(hitpoint - (target.position + clickPosition), target.up);

                                target.position += proj;


                            }
                        }
                        break;

                    case GizmoAxis.Z:
                        if (gizmoType.Equals(GizmoType.Rotation))
                            target.Rotate(target.forward, InputManager.Instance.PointerDelta.x - InputManager.Instance.PointerDelta.y,Space.World);
                        else
                        //target.Translate(target.forward * ((InputManager.Instance.PointerDelta.x + InputManager.Instance.PointerDelta.y) * 0.01f), Space.World);
                        {
                            Plane plane = new Plane(target.up, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray, out float distance))
                            {
                                Vector3 hitpoint = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = hitpoint - target.position;
                                }


                                Vector3 proj = Vector3.Project(hitpoint - (target.position + clickPosition), target.forward);

                                target.position += proj;


                            }

                        }
                        break;
                    case GizmoAxis.XY:
                        if (gizmoType.Equals(GizmoType.Transform))
                        {
                            Plane plane = new Plane(target.forward, target.position);
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
                            Plane plane = new Plane(target.right, target.position);
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
                            Plane plane = new Plane(target.up, target.position);
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
