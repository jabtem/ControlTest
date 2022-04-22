using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowCamara 
{
    Transform target { get; set; }

    void CameraFunction();
    void CamRotation();
}
