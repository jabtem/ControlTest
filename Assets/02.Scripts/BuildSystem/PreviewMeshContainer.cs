using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewMeshContainer : PreviewContainerBase
{

    MeshFilter[] meshFilters;

    void OnEnable()
    {
        if (previewObj != null && target != null)
        {
            ReusePreview();
        }
        else if (previewObj == null && target != null)
        {
            CreatePreview();
        }
    }


    /************* PreviewTrail******************
    ************* 프리뷰 생성로직 **********************/
    public override void CreatePreview()
    {
        previewObj = new GameObject[meshFilters.Length];
        MeshRenderer targetMeshren;
        target.TryGetComponent<MeshRenderer>(out targetMeshren);

        for (int i = 0; i < meshFilters.Length; i++)
        {
            //Mesh mesh = new Mesh();
            //skinnedMeshRenderer[i].BakeMesh(mesh);

            GameObject obj = new GameObject($"{target.gameObject.name} preview");
            obj.layer = LayerMask.NameToLayer("Preview");
            previewObj[i] = obj;
            MeshFilter mf = obj.AddComponent<MeshFilter>();
            MeshRenderer mr = obj.AddComponent<MeshRenderer>();
            mf.mesh = meshFilters[i].sharedMesh;

            Material[] mat = targetMeshren.sharedMaterials;

            for(int j=0; j< mat.Length; j++)
            {
                mat[j] = previewMat;
            }

            mr.materials = mat;
            MeshCollider meshCol;
            meshCol = obj.AddComponent<MeshCollider>();
            meshCol.convex = true;
            //obj.transform.position = targetTr.position;
            ////메쉬가 회전해있는경우 고려
            //obj.transform.rotation = meshFilters[i].gameObject.transform.rotation;
            obj.transform.SetParent(this.transform);
        }
    }



    /****************ReusePreview*******************
     ****************프리뷰 재사용 로직*******************/
    public override void ReusePreview()
    {

        for (int i = 0; i < meshFilters.Length; i++)
        {

            MeshRenderer targetMeshren;
            target.TryGetComponent<MeshRenderer>(out targetMeshren);

            MeshFilter mf = previewObj[i].GetComponent<MeshFilter>();
            MeshRenderer mr = previewObj[i].GetComponent<MeshRenderer>();
            mf.mesh = meshFilters[i].sharedMesh;
            Material[] mat = targetMeshren.sharedMaterials;

            for (int j = 0; j < mat.Length; j++)
            {
                mat[j] = previewMat;
            }

            mr.materials = mat;
            //motionTrailObj[i].transform.position = targetTr.position;
            //motionTrailObj[i].transform.rotation = meshFilters[i].gameObject.transform.rotation;
        }


    }

    public override void PreviewSet(GameObject go, Material mat)
    {
        target = go;
        previewMat = mat;
        meshFilters = target.GetComponentsInChildren<MeshFilter>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        previewMat.color = new Color(1f, 4 / 255f, 0f, 215 / 255f);
    }
    private void OnCollisionExit(Collision collision)
    {
        previewMat.color = new Color(1f/255f, 1f, 0f, 215 / 255f);
    }
}