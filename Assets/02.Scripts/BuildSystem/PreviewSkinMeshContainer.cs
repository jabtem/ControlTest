using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSkinMeshContainer : PreviewContainerBase
{


    SkinnedMeshRenderer[] skinnedMeshRenderer;



    void OnEnable()
    {
        if (previewObj != null)
        {
            ReusePreview();
        }
        else if (previewObj == null)
        {
            CreatePreview();
        }
    }

    /************* PreviewTrail******************
    ************* 프리뷰 생성로직 **********************/
    public override void CreatePreview()
    {

        previewObj = new GameObject[skinnedMeshRenderer.Length];

        for (int i = 0; i < skinnedMeshRenderer.Length; i++)
        {
            //Mesh mesh = new Mesh();
            

            GameObject obj = new GameObject($"{target.gameObject.name} preview");
            obj.layer = LayerMask.NameToLayer("Preview");
            previewObj[i] = obj;
            MeshFilter mf = obj.AddComponent<MeshFilter>();
            MeshRenderer mr = obj.AddComponent<MeshRenderer>();

            //mf.mesh = mesh;
            skinnedMeshRenderer[i].BakeMesh(mf.mesh);
            mr.material = previewMat;
            //obj.transform.position = target.position;
            ////메쉬가 회전해있는경우 고려
            obj.transform.rotation = skinnedMeshRenderer[i].gameObject.transform.rotation;
            obj.transform.SetParent(this.transform);
        }
    }



    /****************ReusePreview*******************
     ****************프리뷰 재사용 로직*******************/
    public override void ReusePreview()
    {
        for (int i = 0; i < skinnedMeshRenderer.Length; i++)
        {
            MeshFilter mf = previewObj[i].GetComponent<MeshFilter>();
            MeshRenderer mr = previewObj[i].GetComponent<MeshRenderer>();
            skinnedMeshRenderer[i].BakeMesh(mf.mesh);
            mr.material = previewMat;
            //motionTrailObj[i].transform.position = targetTr.position;
            previewObj[i].transform.rotation = skinnedMeshRenderer[i].gameObject.transform.rotation;
        }


    }
    public override void PreviewSet(GameObject go, Material mat)
    {
        target = go;
        previewMat = mat;
        skinnedMeshRenderer = target.GetComponentsInChildren<SkinnedMeshRenderer>();
    }
}