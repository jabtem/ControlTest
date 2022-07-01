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
    public GizmoAxies selectedAxies;
    TargetDirection targetDirection;

    public Vector3 clickPosition;
    public Vector3 clickTargetPos;
    private void Reset()
    {
        rotationGizmo = transform.Find("Rotation").gameObject;
    }


    private void OnEnable()
    {
        InputManager.Instance.Input_ObjectMove += GizmoMove;
        InputManager.Instance.Input_ObjectClickUp += Test;
    }

    private void OnDisable()
    {
        InputManager.Instance.Input_ObjectMove -= GizmoMove;
        InputManager.Instance.Input_ObjectClickUp -= Test;
    }

    public void Test()
    {
        clickPosition = Vector3.zero;
        clickTargetPos = Vector3.zero;
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

    public void SelectedAxies(GizmoAxies axies)
    {
        switch(axies)
        {
            case GizmoAxies.X:
                selectedAxies = GizmoAxies.X;
                break;
            case GizmoAxies.Y:
                selectedAxies = GizmoAxies.Y;
                break;
            case GizmoAxies.Z:
                selectedAxies = GizmoAxies.Z;
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
                    case GizmoAxies.X:
                        if (gizmoType.Equals(GizmoType.Rotation))
                            target.Rotate(target.right, InputManager.Instance.PointerDelta.x - InputManager.Instance.PointerDelta.y, Space.World);
                        else
                        {
                            Plane plane = new Plane(target.up, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray,out float distance))
                            {
                                Vector3 d = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = d-target.position;
                                    clickTargetPos = target.position - clickPosition;
                                }


                                Vector3 proj = Vector3.Project(d-(target.position+clickPosition), target.right);

                                target.position +=proj;

                                
                            }
  
                        }

                        break;
                    case GizmoAxies.Y:
                        if (gizmoType.Equals(GizmoType.Rotation))
                            target.Rotate(target.up, InputManager.Instance.PointerDelta.x - InputManager.Instance.PointerDelta.y ,Space.World);
                        else
                        //target.Translate(target.up * ((InputManager.Instance.PointerDelta.x + InputManager.Instance.PointerDelta.y)*0.01f), Space.World);
                        {
                            Plane plane = new Plane(target.forward, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray, out float distance))
                            {
                                Vector3 d = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = d - target.position;
                                    clickTargetPos = target.position - clickPosition;
                                }


                                Vector3 proj = Vector3.Project(d - (target.position + clickPosition), target.up);

                                target.position += proj;


                            }
                        }
                        break;

                    case GizmoAxies.Z:
                        if (gizmoType.Equals(GizmoType.Rotation))
                            target.Rotate(target.forward, InputManager.Instance.PointerDelta.x - InputManager.Instance.PointerDelta.y,Space.World);
                        else
                        //target.Translate(target.forward * ((InputManager.Instance.PointerDelta.x + InputManager.Instance.PointerDelta.y) * 0.01f), Space.World);
                        {
                            Plane plane = new Plane(target.up, target.position);
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (plane.Raycast(ray, out float distance))
                            {
                                Vector3 d = ray.GetPoint(distance);
                                if (clickPosition.Equals(Vector3.zero))
                                {
                                    clickPosition = d - target.position;
                                    clickTargetPos = target.position - clickPosition;
                                }


                                Vector3 proj = Vector3.Project(d - (target.position + clickPosition), target.forward);

                                target.position += proj;


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
