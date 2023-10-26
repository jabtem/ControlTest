using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{

    private void OnEnable()
    {
        InputManager.instance.Input_Move += PlayerMove;
    }
    private void OnDisable()
    {
        InputManager.instance.Input_Move -= PlayerMove;
    }

    private void PlayerMove(Vector2 vec)
    {
        if (vec.sqrMagnitude > 0.1f)
        {
            Vector3 moveDir = new Vector3(InputManager.instance.moveDir.x, 0f, InputManager.instance.moveDir.y);
            transform.Translate(3f * Time.deltaTime * moveDir, Space.World);
        }
    }

    //void Update()
    //{
    //    if(InputManager.Instance.moveDir.sqrMagnitude>0.1f)
    //    {
    //        Vector3 moveDir = new Vector3(InputManager.Instance.moveDir.x, 0f, InputManager.Instance.moveDir.y);
    //        transform.Translate(3f * Time.deltaTime * moveDir, Space.World);
    //    }
    //}
}
