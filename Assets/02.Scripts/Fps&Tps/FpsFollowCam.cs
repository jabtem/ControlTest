using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsFollowCam : MonoBehaviour, IFollowCamara
{

    [SerializeField]
    [Range(0f, 0.1f)]
    float rotateSensitive;

    //ȸ���� ī�޶�ޱ�
    Vector3 rot;

    [SerializeField]
    [Range(0f,10f)]
    //ī�޶� ȸ���ӵ�
    float rotateSpeed = 5f;

    //ī�޶� X�� ȸ������
    [SerializeField]
    [Range(0f, 180)]
    float defalutXangleLimit;


    float cameraXangleMaxLimit;
    float cameraXangleMinLimit;




    //���� ȸ������
    [SerializeField]
    [Range(0f, 180)]
    float defalutYangleLimit;

    //ī�޶� �󸶳� ȸ���ߴ��� üũ�� ��üũ�� �ʱ�ȭ����
    float camRotatedY;

    [SerializeField]
    Transform _target;
    public Transform target
    {
        get => _target;
        set => _target = value;
    }


    private void Awake()
    {

        cameraXangleMaxLimit = defalutXangleLimit;
        cameraXangleMinLimit = defalutXangleLimit * -1;
    }



    private void Update()
    {
        if (target != null)
        {
            CameraFunction();
        }
    }
    private void LateUpdate()
    {
        CamRotation();
    }

    public void CamRotation()
    {
        bool plyaerMove = CameraManager.instance.playerMove;

        if (MouseManager.instance.leftClickHold)
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            if (Mathf.Abs(x) > rotateSensitive)
            {
                float ang = x * rotateSpeed;
                rot.y += ang;
                camRotatedY += ang;

                if (!plyaerMove)
                {

                    if(rot.y >= cameraXangleMaxLimit)
                    {
                        rot.y = cameraXangleMaxLimit;
                    }
                    else if(rot.y <= cameraXangleMinLimit)
                    {
                        rot.y = cameraXangleMinLimit;
                    }

                }
                

            }
            if(Mathf.Abs(y) > rotateSensitive)
            {
                rot.x -= y * rotateSpeed;
                rot.x = (rot.x > 0) ? ((rot.x) > defalutYangleLimit ? defalutYangleLimit : rot.x) : ((rot.x) < -defalutYangleLimit ? -defalutYangleLimit : rot.x);
            }


            Quaternion q = Quaternion.Euler(rot);

            transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f);

            //transform.rotation = Quaternion.Euler(rot.x, rot.y, 0f);


        }
        else if(MouseManager.instance.leftClikUp)
        {
            if(!CameraManager.instance.playerMove)
            {
                AngleLimitUpdate(camRotatedY);
                camRotatedY = 0;
            }


        }

    }

    public void CameraFunction()
    {
        transform.position = target.position;
    }

    public void AngleLimitUpdate(float ang)
    {
        if (ang >= defalutXangleLimit)
        {
            ang = defalutXangleLimit;
        }
        else if(ang <= defalutXangleLimit*-1f)
        {
            ang = defalutXangleLimit * -1f;
        }


        cameraXangleMaxLimit += ang;

        cameraXangleMinLimit = cameraXangleMaxLimit - (defalutXangleLimit * 2f);

    }
    public void AngleLimitUpdate()
    {
        float ang = rot.y;

        cameraXangleMaxLimit = defalutXangleLimit + ang;
        cameraXangleMinLimit = cameraXangleMaxLimit - (defalutXangleLimit * 2f);
    }
}
