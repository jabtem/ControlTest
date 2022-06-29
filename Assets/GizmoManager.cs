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

    private void Reset()
    {
        rotationGizmo = transform.Find("Rotation").gameObject;
    }


    private void OnEnable()
    {
        InputManager.Instance.Input_ObjectMove += GizmoMove;
    }

    private void OnDisable()
    {
        InputManager.Instance.Input_ObjectMove -= GizmoMove;
    }

    private void Update()
    {
        if (snapManager.snapObj != null)
        {
            target = snapManager.snapObj.transform;
            transform.rotation = target.rotation;
            transform.position = target.position;
        }
    }

    public void ShowGizmo()
    {
        if (snapManager.snapObj == null)
        {
            rotationGizmo.SetActive(false);
            return;
        }

            switch (gizmoType)
        {
            case GizmoType.Rotation:
                rotationGizmo.SetActive(true);
                //transformGizmo.gameObject.SetActive(false);
                break;
            case GizmoType.Transform:
                rotationGizmo.SetActive(false);
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
                        target.Rotate(Vector3.right, InputManager.Instance.PointerDelta.x + InputManager.Instance.PointerDelta.y);
                        break;
                    case GizmoAxies.Y:
                        target.Rotate(Vector3.up, -(InputManager.Instance.PointerDelta.x + InputManager.Instance.PointerDelta.y));
                        break;

                    case GizmoAxies.Z:
                        target.Rotate(Vector3.forward, -(InputManager.Instance.PointerDelta.x + InputManager.Instance.PointerDelta.y));
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
