using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{

    public event Action Input_ObjectClick;
    public event Action Input_ObjectMove;

    public static InputManager Instance;

    public InputSetting inputSet;

    public Button testButt;
    public Button testButt2;
    Vector2 pointerDelta;

    public Vector2 PointerDelta
    {
        get => pointerDelta;
    }

    Vector3 moveDir;
    public Vector3 MoveDir
    {
        get => moveDir;
    }

    private void Awake()
    {
        inputSet = new InputSetting();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        inputSet.BuilMenu.PointerMove.performed += OnPointerMove;
        inputSet.BuilMenu.ObjectSnap.started += (context) =>
        {
            Input_ObjectClick?.Invoke();
        };

        inputSet.Player.Move.performed += (context) =>
        {
            moveDir = context.ReadValue<Vector3>();
        };

        testButt.onClick.AddListener(() => { ActionMapChange(inputSet.BuilMenu); });
        testButt2.onClick.AddListener(() => { ActionMapChange(inputSet.Player); });
    }

    private void OnEnable()
    {
        ActionMapChange(inputSet.Player);
    }


    public void ActionMapChange(InputActionMap actionMap)
    {
        if (actionMap.enabled)
            return;
        inputSet.Disable();
        actionMap.Enable();
    }

    public void OnPointerMove(InputAction.CallbackContext context)
    {
        pointerDelta = context.ReadValue<Vector2>();
        Input_ObjectMove?.Invoke();
    }
}
