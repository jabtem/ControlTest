using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerAction : MonoBehaviour
{
    NavMeshAgent myNav;
    RaycastHit rayHit;
    Ray ray;
    Vector3 movePoint;

    int groundLayer;
    Collider col;
    Rigidbody rigid;


    //���� ����ġ, 1��Ī ī�޶� ������
    public Transform fpsCamRig;

    [SerializeField]
    [Range(0f, 100f)]
    float moveSpeed;

    [SerializeField]
    [Range(0f, 100f)]
    float rotateSpeed;

    [SerializeField]
    PovType _type;
    public PovType type
    {
        get => _type;
        set
        {
            _type = value;
            if(CameraManager.instance !=null)
            {
                CameraManager.instance.SetCamera(this,_type);
            }
            PlayerInit();
        }
    }


    //ȸ�� �� �÷��̾� �����̼�
    Quaternion preRoation;

    //ī�޶� ���������� ������Ʈ �ʱ�ȭ
    void PlayerInit()
    {
        if (type.Equals(PovType.TPS))
        {

            if (!myNav.enabled)
            {
                myNav.enabled = true;
            }

            if(rigid.useGravity)
            {
                rigid.useGravity = false;
                col.enabled = false;
            }

        }
        else if (type.Equals(PovType.FPS))
        {
            if(myNav.enabled)
            {
                myNav.enabled = false;
            }

            if (!rigid.useGravity)
            {
                rigid.useGravity = true;
                col.enabled = true;
            }
        }
    }

    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out myNav);
        TryGetComponent<Rigidbody>(out rigid);
        TryGetComponent<Collider>(out col);

    
        groundLayer = (1 << LayerMask.NameToLayer("Ground"));
    }

    private void Start()
    {;
        type = _type;
    }
    private void Update()
    {
        Move();
    }


    void Move()
    {
        if (type.Equals(PovType.TPS))
        {
            TpsPlayerMove();
        }
        else if (type.Equals(PovType.FPS))
        {
            FpsPlayerMove();
        }
    }

    void FpsPlayerMove()
    {

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");


        Vector3 dir = new Vector3(hor, 0f, ver);



        float fpsCamY = CameraManager.instance.fpsCam.transform.rotation.eulerAngles.y; 
        transform.rotation = Quaternion.Euler(0, fpsCamY, 0);

        if (dir.magnitude <0.1f)
        {
            if (CameraManager.instance.playerMove)
            {

                //ĳ���� ȸ�������� ��ȭ�� �������
                if (!(preRoation.eulerAngles.y - transform.rotation.eulerAngles.y).Equals(0))
                {
                    CameraManager.instance.FpsCamAngleUpdate();
                    preRoation = transform.rotation;
                }

                CameraManager.instance.playerMove = false;
            }

            return;
        }
        else if(dir.magnitude >=0.1f && !CameraManager.instance.playerMove)
        {
            CameraManager.instance.playerMove = true;
            //�̵� ���� ĳ������ ����
            preRoation = transform.rotation;
        }


        Vector3 fpsCamFoward = new Vector3(CameraManager.instance.fpsCam.transform.forward.x, 0f, CameraManager.instance.fpsCam.transform.forward.z);
        Vector3 fpsCamRight = CameraManager.instance.fpsCam.transform.right;



        //�����¿� �̵� ���� ���� ���
        Vector3 moveDir = (fpsCamFoward * ver) + (fpsCamRight * hor);


        //Translate(�̵� ���� * Time.deltaTime * ������ * �ӵ�, ������ǥ)
        transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.World);
    }

    void TpsPlayerMove()
    {
#if UNITY_EDITOR
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.blue);
#endif


        if (!MouseManager.instance.isMouseMove && MouseManager.instance.leftClikUp)
        {
            ray = CameraManager.instance.tpsCam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out rayHit, 150f, groundLayer))
            {
                movePoint = rayHit.point;

                myNav.destination = movePoint;
                myNav.speed = moveSpeed;
                myNav.stoppingDistance = 0.0f;
            }
        }
    }


    [ContextMenu("CameraSwitch")]
    public void Switch()
    {
        type = _type;
    }
}
