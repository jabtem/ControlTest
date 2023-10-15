using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{

    public event Action Input_ObjectClickDown;
    public event Action Input_ObjectMove;
    public event Action Input_ObjectClickUp;

    public static InputManager Instance;

    public InputSetting inputSet;

    public Button testButt;
    public Button testButt2;

    public bool isObjectClick = false;

    Vector2 pointerDelta;

    public Vector2 PointerDelta
    {
        get => pointerDelta;
    }

    Vector2 moveDir;
    public Vector2 MoveDir
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

        inputSet.Housing.PointerMove.performed += OnPointerMove;
        inputSet.Housing.ObjectSnap.started += (context) =>
        {
            Input_ObjectClickDown?.Invoke();
            isObjectClick = true;
        };
        inputSet.Housing.ObjectSnap.canceled += (context) =>
        {
            Input_ObjectClickUp?.Invoke();
            isObjectClick = false;
        };

        inputSet.Player.Move.performed += (context) =>
        {
            moveDir = context.ReadValue<Vector2>();
        };

        testButt.onClick.AddListener(() => { ActionMapChange(inputSet.Housing); });
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
