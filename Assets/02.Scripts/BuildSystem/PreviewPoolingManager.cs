using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPoolingManager : MonoBehaviour
{
    public static PreviewPoolingManager instance;

    Stack<GameObject> motionTrailContainerStack = new Stack<GameObject>();


    //프리팹을 key 프리뷰를 value로 묶는다
    Dictionary<GameObject, GameObject> previewContainerDic = new Dictionary<GameObject, GameObject>();

    public Material previewMat;
    SkinnedMeshRenderer checkSkinMesh;
    GameObject previewTarget;
    PreviewCtrl previewCtrl;

    private void Awake()
    {
        //SingleTion
        if (instance == null)
            instance = this;
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }

        TryGetComponent<PreviewCtrl>(out previewCtrl);
    }



    GameObject CreateMotionTrailContainer(GameObject key)
    {
        GameObject mtContainerObj = new GameObject($"{previewTarget.name} previwContianer ");

        
        //스킨메쉬를가진 오브젝트의경우
        if(previewTarget.TryGetComponent<SkinnedMeshRenderer>(out checkSkinMesh))
        {
            PreviewSkinMeshContainer mtContainer = mtContainerObj.AddComponent<PreviewSkinMeshContainer>();
            mtContainer.PreviewSet(key, previewMat);
        }
        else
        {
            PreviewMeshContainer mtContainer = mtContainerObj.AddComponent<PreviewMeshContainer>();
            mtContainer.PreviewSet(key, previewMat);
        }



        //mtContainer.ColorSet(color);
        mtContainerObj.transform.SetParent(this.transform);
        mtContainerObj.SetActive(false);
        previewContainerDic.Add(key, mtContainerObj);
        //motionTrailContainerStack.Push(mtContainerObj);

        return mtContainerObj;
    }



    public void GetMotionTrailContainer(GameObject key)
    {
        previewTarget = key;
        GameObject reqObject = null;


        //Already Key Exist
        if(previewContainerDic.ContainsKey(key))
        {
            reqObject = previewContainerDic[key];
        }
        //Key X
        else
        {
            reqObject = CreateMotionTrailContainer(key);
        }

        //reqObject = motionTrailContainerStack.Pop();
        //스킨메쉬를가진 오브젝트의경우
        //if (previewTarget.TryGetComponent<SkinnedMeshRenderer>(out checkSkinMesh))
        //{
        //    PreviewSkinMeshContainer mtContainer = reqObject.GetComponent<PreviewSkinMeshContainer>();
        //    mtContainer.PreviewSet(reqObject, buildImpossibleMat);
        //}
        //else
        //{
        //    PreviewMeshContainer mtContainer = reqObject.GetComponent<PreviewMeshContainer>();
        //    mtContainer.PreviewSet(reqObject, buildImpossibleMat);
        //}
        Rigidbody rigid = reqObject.AddComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.freezeRotation = true;
        previewCtrl.SetObj(reqObject);
        reqObject.gameObject.SetActive(true);
        
    }

    public void SetMotionTrailContainer(GameObject obj)
    {
        obj.SetActive(false);
        //motionTrailContainerStack.Push(obj);
    }



}