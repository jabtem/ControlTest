using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FpsPlayerAction : MonoBehaviour
{
    Collider col;
    Rigidbody rigid;

    [SerializeField]
    [Range(0f, 100f)]
    float moveSpeed;

    [SerializeField]
    [Range(0f, 100f)]
    float rotateSpeed;

    [SerializeField]
    Camera fpsCam;


    //ȸ�� �� �÷��̾� �����̼�
    Quaternion preRoation;

    //ī�޶� ���������� ������Ʈ �ʱ�ȭ
    void PlayerInit()
    {
        rigid.useGravity = true;
        col.enabled = true;
    }

    private void Awake()
    {

        TryGetComponent<Rigidbody>(out rigid);
        TryGetComponent<Collider>(out col);

    }

    private void Start()
    {
        PlayerInit();
    }
    private void Update()
    {
        Move();
    }


    void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");


        Vector3 dir = new Vector3(hor, 0f, ver);



        float fpsCamY = fpsCam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, fpsCamY, 0);

        if (dir.magnitude < 0.1f)
        {
            preRoation = transform.rotation;
            return;
        }
        else if (dir.magnitude >= 0.1f )
        {
            //�̵� ���� ĳ������ ����
            preRoation = transform.rotation;
        }


        Vector3 fpsCamFoward = new Vector3(fpsCam.transform.forward.x, 0f, fpsCam.transform.forward.z);
        Vector3 fpsCamRight = fpsCam.transform.right;



        //�����¿� �̵� ���� ���� ���
        Vector3 moveDir = (fpsCamFoward * ver) + (fpsCamRight * hor);


        //Translate(�̵� ���� * Time.deltaTime * ������ * �ӵ�, ������ǥ)
        transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.World);
    }

}
