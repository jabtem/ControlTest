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
            transform.Translate(3f * Time.deltaTime * InputManager.Instance.MoveDir, Space.World);
        }
    }
}
