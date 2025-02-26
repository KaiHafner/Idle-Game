using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    #region Variables

    private Vector3 _origin;
    private Vector3 _difference;
    private Camera _mainCamera;
    private bool _isDragging;
    private Bounds _cameraBounds;
    private Vector3 _targetPosition;

    public float moveSpeed = 10f;

    #endregion

    private void Awake() => _mainCamera = Camera.main;

    private void Start()
    {
        var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
        );
    }

    private void Update()
    {
        HandleKeyboardInput();
    }

    private void LateUpdate()
    {
        HandleMouseDrag();
    }

    private void HandleKeyboardInput()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Keyboard.current.wKey.isPressed) moveDirection.y += 1;
        if (Keyboard.current.sKey.isPressed) moveDirection.y -= 1;
        if (Keyboard.current.aKey.isPressed) moveDirection.x -= 1;
        if (Keyboard.current.dKey.isPressed) moveDirection.x += 1;

        _targetPosition = transform.position + moveDirection.normalized * moveSpeed * Time.deltaTime;
        _targetPosition = GetCameraBounds(_targetPosition);
        transform.position = _targetPosition;
    }

    private void HandleMouseDrag()
    {
        if (Mouse.current.rightButton.isPressed || Mouse.current.leftButton.isPressed)
        {
            if (!_isDragging)
            {
                _isDragging = true;
                _origin = GetMousePosition(); // Store initial drag position
            }

            _difference = GetMousePosition() - _origin;
            _targetPosition = transform.position - _difference;
            _targetPosition = GetCameraBounds(_targetPosition);
            transform.position = _targetPosition;
        }
        else
        {
            _isDragging = false;
        }
    }

    private Vector3 GetCameraBounds(Vector3 targetPos)
    {
        return new Vector3(
            Mathf.Clamp(targetPos.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(targetPos.y, _cameraBounds.min.y, _cameraBounds.max.y),
            transform.position.z
        );
    }

    private Vector3 GetMousePosition()
    {
        return _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
}
