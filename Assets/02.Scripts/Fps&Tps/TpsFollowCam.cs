using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsFollowCam : MonoBehaviour,IFollowCamara
{

    //����ٴ� ���
    Transform _target;
    public Transform target
    {
        get => _target;
        set => _target = value;
    }
    public float distance = 10.0f;

    //ī�޶� ȸ���ӵ�
    public float rotateSpeed = 5f;


    public float height = 5.0f;

    public float heightDamping = 2.0f;

    public float rotationDaping = 3.0f;

    float _rotAngle;


    public float preRoationDaping;

    //ȸ���� ī�޶�ޱ�
    Vector3 rot;

    public float RotAngle
    {
        get => _rotAngle;
        private set => _rotAngle = value;
    }

    private void Awake()
    {
        preRoationDaping = rotationDaping;
    }

    private void Update()
    {
        CamRotation();
    }

    void LateUpdate()
    {
        CameraFunction();
    }


    public void CamRotation()
    {

        //ȸ���� ī�޶�ޱ�
        rot = transform.rotation.eulerAngles;

        float x = Input.GetAxis("Mouse X");

        //PC�� ������ ī�޶� ����
        if(MouseManager.instance.leftClikDown)
        {
            rotationDaping = preRoationDaping;
            
        }
        else if (MouseManager.instance.leftClickHold)
        {

            rot.y += x * rotateSpeed;

            Quaternion q = Quaternion.Euler(rot);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f);

            if(MouseManager.instance.isMouseMove && !rotationDaping.Equals(0))
            {
                preRoationDaping = rotationDaping;
                rotationDaping = 0f;
            }
        }

    }

    public void CameraFunction()
    {
        if (!target)
            return;


        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDaping * Time.deltaTime);

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        Vector3 tempDis = target.position;
        tempDis -= currentRotation * Vector3.forward * distance;

        tempDis.y = currentHeight;

        transform.position = tempDis;

        transform.LookAt(target);
    }
}