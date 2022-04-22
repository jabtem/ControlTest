using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PovType { FPS, TPS };
public class CameraManager : MonoBehaviour
{
    IFollowCamara currentCam;


    [SerializeField]
    Camera _tpsCam;
    public Camera tpsCam
    {
        get => _tpsCam;
    }
    [SerializeField]
    Camera _fpsCam;
    public Camera fpsCam
    {
        get => _fpsCam;
    }

    TpsFollowCam tpsFollow;
    FpsFollowCam fpsFollow;

    bool _playerMove;
    public bool playerMove
    {
        get => _playerMove;
        set => _playerMove = value;
    }

    static CameraManager _instance;
    public static CameraManager instance
    {
        get => _instance;
    }
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }

        transform.Find("ThirdPersonCam").TryGetComponent<Camera>(out _tpsCam);
        transform.Find("FirstPersonCam").TryGetComponent<Camera>(out _fpsCam);

        if(_tpsCam !=null)
        {
            tpsCam.TryGetComponent<TpsFollowCam>(out tpsFollow);
            _tpsCam.gameObject.SetActive(false);
        }

        if(_fpsCam !=null)
        {

            fpsCam.TryGetComponent<FpsFollowCam>(out fpsFollow);
            _fpsCam.gameObject.SetActive(false);
        }

    }


     void Update()
    {
        if (currentCam.target != null)
        {
            currentCam.CameraFunction();
        }
    }
    void LateUpdate()
    {
        currentCam.CamRotation();
    }

    public void SetCamera(PlayerAction player, PovType pov)
    {

        if(pov.Equals(PovType.FPS))
        {


            SetTarget(fpsFollow, player.fpsCamRig);
            //fpsCam.gameObject.transform.position = player.fpsCamRig.position;
            //fpsFollow.target = player.fpsCamRig;
            tpsCam.gameObject.SetActive(false);
            if (!fpsCam.gameObject.activeSelf)
            {
                fpsCam.gameObject.SetActive(true);
                fpsCam.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
        }
        else if (pov.Equals(PovType.TPS))
        {

            SetTarget(tpsFollow, player.transform);
            //currentCam = tpsFollow;
            //currentCam.target = player.transform;
            fpsCam.transform.SetParent(CameraManager.instance.transform);
            fpsCam.gameObject.SetActive(false);
            if (!tpsCam.gameObject.activeSelf)
            {
                tpsCam.gameObject.SetActive(true);
            }
        }
    }

    public void FpsCamAngleUpdate()
    {
        fpsFollow.AngleLimitUpdate();
    }


    void SetTarget(IFollowCamara cam, Transform target)
    {
        currentCam = cam;
        cam.target = target;
    }
}
