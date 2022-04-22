using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    static MouseManager _instance;
    public static MouseManager instance
    {
        get => _instance;
        private set => _instance = value;
    }

    Vector2 _mouseButtonDownPoint;
    public Vector2 mouseButtonDownPoint
    {
        get => _mouseButtonDownPoint;
    }
    Vector2 _mouseButtonDragPoint;
    public Vector2 mouseButtonDragPoint
    {
        get => _mouseButtonDragPoint;
    }

    Vector2 preDragPoint;
    float mouseMoveDistance;

    bool _isMouseMove;
    public bool isMouseMove
    {
        get => _isMouseMove;
    }

    bool _leftClikDown;
    public bool leftClikDown
    {
        get => _leftClikDown;
    }

    bool _leftClikUp;
    public bool leftClikUp
    {
        get => _leftClikUp;
    }

    bool _leftClickHold;
    public bool leftClickHold
    {
        get => _leftClickHold;
    }




    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {

        _leftClikDown = Input.GetMouseButtonDown(0);
        _leftClickHold = Input.GetMouseButton(0);
        _leftClikUp = Input.GetMouseButtonUp(0);

        //LeftClickEvent
        if (_leftClikDown)
        {
            _isMouseMove = false;

            mouseMoveDistance = 0f;
            _mouseButtonDownPoint = Input.mousePosition;
        }
        else if(_leftClickHold)
        {
            preDragPoint = _mouseButtonDragPoint;
            _mouseButtonDragPoint = Input.mousePosition;

            if(!preDragPoint.Equals(_mouseButtonDragPoint))
            {
                mouseMoveDistance += (_mouseButtonDownPoint - _mouseButtonDragPoint).sqrMagnitude;
                if (mouseMoveDistance > 200f && !_isMouseMove)
                {
                    _isMouseMove = true;
                }
            }
        }
        
    }
}
