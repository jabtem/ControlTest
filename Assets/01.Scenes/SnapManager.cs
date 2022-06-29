using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapManager : MonoBehaviour
{

    public GameObject snapObj;
    int layerMask;
    public Vector3 rayDir;
    public Vector3 rayOrigin;
    private void OnEnable()
    {
        InputManager.Instance.Input_ObjectClick += SnapCheck;
        InputManager.Instance.Input_ObjectMove += SnapObjMove;
    }


    private void OnDisable()
    {
        InputManager.Instance.Input_ObjectClick -= SnapCheck;
        InputManager.Instance.Input_ObjectMove -= SnapObjMove;
    }

    public void SnapCheck()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Obstacle"));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if(snapObj == null )
        {
            if(Physics.Raycast(ray,out RaycastHit hit, 150f,layerMask))
            {
                if (hit.collider.transform.parent != null)
                {
                    snapObj = hit.collider.transform.parent.gameObject;
                }
                else
                    snapObj = hit.collider.gameObject;


                Collider[] cols = snapObj.GetComponentsInChildren<Collider>();

                foreach(var col in cols)
                {
                    col.isTrigger = true;
                }

                if (snapObj.TryGetComponent<Rigidbody>(out Rigidbody rigid))
                    rigid.isKinematic = true;


            }
        }
        else if(snapObj !=null)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 150f, layerMask))
            {

                Collider[] cols = snapObj.GetComponentsInChildren<Collider>();

                foreach (var col in cols)
                {
                    col.isTrigger = false;
                }

                if (snapObj.TryGetComponent<Rigidbody>(out Rigidbody rigid))
                    rigid.isKinematic = false;
                snapObj = null;
            }
        }
    }

    public void SnapObjMove()
    {

        if(InputManager.Instance.PointerDelta != Vector2.zero)
        {
            layerMask = ~(1 << LayerMask.NameToLayer("Player"));

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            rayOrigin = ray.origin;
            rayDir = ray.direction;

            //if (snapObj != null)
            //{
            //    if (Physics.Raycast(ray, out RaycastHit hit, 150f, layerMask, QueryTriggerInteraction.Ignore))
            //    {
            //        //snapObj.transform.position = hit.point;
            //    }
            //}
        }

    }
}
