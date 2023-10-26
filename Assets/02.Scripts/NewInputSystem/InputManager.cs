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
    public event Action<Vector2> Input_Move;

    public static InputManager instance;
    private InputSetting _inputSet;
    public Button housingModeButt;
    public Button playerModeButt;

    private bool _isObjectClick = false;
    public bool isObjectClick
    {
        get => _isObjectClick;
    }

    Vector2 _pointerDelta;
    public Vector2 pointerDelta
    {
        get => _pointerDelta;
    }

    Vector2 _moveDir;
    public Vector2 moveDir
    {
        get => _moveDir;
    }

    private void Awake()
    {
        _inputSet = new InputSetting();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        _inputSet.Housing.PointerMove.performed += OnPointerMove;
        _inputSet.Housing.ObjectSnap.started += (context) =>
        {
            Input_ObjectClickDown?.Invoke();
            _isObjectClick = true;
        };
        _inputSet.Housing.ObjectSnap.canceled += (context) =>
        {
            Input_ObjectClickUp?.Invoke();
            _isObjectClick = false;
        };

        _inputSet.Player.Move.performed += (context) =>
        {
            _moveDir = context.ReadValue<Vector2>();
        };

        housingModeButt.onClick.AddListener(() => { ActionMapChange(_inputSet.Housing); });
        playerModeButt.onClick.AddListener(() => { ActionMapChange(_inputSet.Player); });
    }

    private void OnEnable()
    {
        ActionMapChange(_inputSet.Player);
    }

    public void ActionMapChange(InputActionMap actionMap)
    {
        if (actionMap.enabled)
            return;
        _inputSet.Disable();
        actionMap.Enable();
    }

    public void OnPointerMove(InputAction.CallbackContext context)
    {
        _pointerDelta = context.ReadValue<Vector2>();
        Input_ObjectMove?.Invoke();
    }

    private void Update()
    {
        Input_Move?.Invoke(_moveDir);
    }
}
