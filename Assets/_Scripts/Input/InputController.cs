using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event Action Click;
    public static event Action<float>Swerve;
    public static event Action BackClicked;
    

    private static InputController _instance = null;
    private float _mouseClickPosition;
    private Camera _mainCamera;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        _mainCamera = Camera.main;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackClicked?.Invoke();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _mouseClickPosition = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            float distanceMovedPercent = GetInWorldMovementPercent(Input.mousePosition.x - _mouseClickPosition);

            // Small touch not counts as swipe
            if (Mathf.Abs(distanceMovedPercent) < 0.001f)
                return;

            Swerve?.Invoke(distanceMovedPercent);
            _mouseClickPosition = Input.mousePosition.x;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            Click?.Invoke();
        }
    }

    private float GetInWorldMovementPercent(float distanceMoved)
    {
        return distanceMoved / _mainCamera.scaledPixelWidth;
    }
}