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


    //회전 전 플레이어 로테이션
    Quaternion preRoation;

    //카메라 설정에따라 컴포넌트 초기화
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
            //이동 직후 캐릭터의 방향
            preRoation = transform.rotation;
        }


        Vector3 fpsCamFoward = new Vector3(fpsCam.transform.forward.x, 0f, fpsCam.transform.forward.z);
        Vector3 fpsCamRight = fpsCam.transform.right;



        //전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (fpsCamFoward * ver) + (fpsCamRight * hor);


        //Translate(이동 방향 * Time.deltaTime * 변위값 * 속도, 기준좌표)
        transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.World);
    }

}
