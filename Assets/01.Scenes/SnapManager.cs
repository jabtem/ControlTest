using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnapManager : MonoBehaviour
{

    public GameObject snapObj;
    public Vector3 rayDir;
    public Vector3 rayOrigin;
    public GizmoManager gizmoManager;

    private void OnEnable()
    {
        InputManager.Instance.Input_ObjectClickDown += SnapCheck;
        //InputManager.Instance.Input_ObjectMove += SnapObjMove;
    }


    private void OnDisable()
    {
        InputManager.Instance.Input_ObjectClickDown -= SnapCheck;
        //InputManager.Instance.Input_ObjectMove -= SnapObjMove;
    }
    public void SnapCheck()
    {
        int layerMask = ~(1 << LayerMask.NameToLayer("Obstacle")| 1<<LayerMask.NameToLayer("Player"));
        int gizmoLayer = 1 << LayerMask.NameToLayer("Gizmo");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray axiesCheckRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(axiesCheckRay,out RaycastHit axiexHit,150f, gizmoLayer))
        {
            gizmoManager.SelectedAxies((GizmoAxies)Enum.Parse(typeof(GizmoAxies), axiexHit.collider.name));
        }

        //if(snapObj == null )
        //{
        if (Physics.Raycast(ray, out RaycastHit hit, 150f, layerMask))
        {
            if (hit.collider.GetComponentInParent<GizmoBase>() != null)
            {
                return;
            }


            if (hit.collider.transform.parent != null)
            {
                snapObj = hit.collider.transform.parent.gameObject;
            }
            else
                snapObj = hit.collider.gameObject;




            Collider[] cols = snapObj.GetComponentsInChildren<Collider>();

            foreach (var col in cols)
            {
                col.isTrigger = true;
            }

            if (snapObj.TryGetComponent<Rigidbody>(out Rigidbody rigid))
                rigid.isKinematic = true;
            gizmoManager.ShowGizmo();

        }
        else
        {
            if(snapObj != null)
            {
                Collider[] cols = snapObj.GetComponentsInChildren<Collider>();

                foreach (var col in cols)
                {
                    //col.isTrigger = false;
                }

                //if (snapObj.TryGetComponent<Rigidbody>(out Rigidbody rigid))
                //    rigid.isKinematic = false;
                snapObj = null;
                gizmoManager.ShowGizmo();
            }

        }
    }

    //public void SnapObjMove()
    //{

    //    if(InputManager.Instance.PointerDelta != Vector2.zero && InputManager.Instance.isObjectClick)
    //    {
    //        int layerMask = ~(1 << LayerMask.NameToLayer("Player"));

    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        rayOrigin = ray.origin;
    //        rayDir = ray.direction;

    //        //if (snapObj != null)
    //        //{
    //        //    if (Physics.Raycast(ray, out RaycastHit hit, 150f, layerMask, QueryTriggerInteraction.Ignore))
    //        //    {
    //        //        //snapObj.transform.position = hit.point;
    //        //    }
    //        //}
    //    }

    //}
}
