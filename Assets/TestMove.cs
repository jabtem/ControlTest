using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.MoveDir.sqrMagnitude>0.1f)
        {
            Vector3 moveDir = new Vector3(InputManager.Instance.MoveDir.x, 0f, InputManager.Instance.MoveDir.y);
            transform.Translate(3f * Time.deltaTime * moveDir, Space.World);
        }
    }
}
