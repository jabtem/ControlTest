using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCtrl : MonoBehaviour
{
    Ray ray;
    RaycastHit rayHit;
    GameObject previewObj;
    int layerMask;
    //이전에 선택한 프리뷰 개체
    GameObject prePreview;

    
    
    private void Start()
    {
        //Ignore Preview Layer
        layerMask = ~(1 << LayerMask.NameToLayer("Preview"));
    }

    private void Update()
    {
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.blue);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHit, 150f,layerMask) && previewObj !=null)
        {

            if (rayHit.collider.TryGetComponent<FunitureInfo>(out FunitureInfo info))
            {
                if (info.stackOK)
                {
                    previewObj.transform.position = new Vector3(rayHit.point.x, rayHit.collider.bounds.size.y + 0.1f, rayHit.point.z);
                }
                else
                {
                    previewObj.transform.position = new Vector3(rayHit.point.x, 0f, rayHit.point.z);
                }
            }
            else
                previewObj.transform.position = rayHit.point;

            //rayHit.collider.ClosestPoint
            //previewObj.transform.position = new Vector3(rayHit.point.x, 0f, rayHit.point.z);
        }
    }

    public void SetObj(GameObject go)
    {
        prePreview = previewObj;
        previewObj = go;
        if(prePreview !=null)
        {
            if(prePreview.activeSelf)
            {
                prePreview.SetActive(false);
            }
        }
    }
}
