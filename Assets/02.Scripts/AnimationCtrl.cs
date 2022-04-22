using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCtrl : MonoBehaviour
{
    Animator anim;

    int hashId;


    private void Awake()
    {
        anim = transform.GetComponentInChildren<Animator>();
    }


    void AnimSetBool(string parameter , int id)
    {

    }
}
