using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PreviewContainerBase : MonoBehaviour
{


    Material _preivewMat;

    public Material previewMat
    {
        get => _preivewMat;
        set => _preivewMat = value;
    }


    GameObject _target;
    public GameObject target
    {
        get => _target;
        set => _target = value;
    }


    GameObject[] _previewObj;
    public GameObject[] previewObj
    {
        get => _previewObj;
        set => _previewObj = value;
    }

    public abstract void CreatePreview();
    public abstract void ReusePreview();

    public virtual void PreviewSet(GameObject go, Material mat)
    {
        target = go;
        previewMat = mat;
    }

   


}
