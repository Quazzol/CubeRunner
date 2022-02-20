using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event Action Click;
    public static event Action<SwipeDirection> Swipe;
    public static event Action BackClicked;
    

    private static InputController _instance = null;
    private Vector3 _mouseClickPosition;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
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
            _mouseClickPosition = Input.mousePosition;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            Click?.Invoke();

            Vector3 clickDirection = Input.mousePosition - _mouseClickPosition;

            // Small touch not counts as swipe
            if (clickDirection.magnitude < 0.02f)
                return;

            if (clickDirection.y > 0 && clickDirection.y > 2f * Mathf.Abs(clickDirection.x))
            {
                Swipe?.Invoke(SwipeDirection.Up);
                return;
            }

            if (clickDirection.x > 0)
            {
                Swipe?.Invoke(SwipeDirection.Right);
                return;
            }
            
            if (clickDirection.x < 0)
            {
                Swipe?.Invoke(SwipeDirection.Left);
                return;
            }
        }
    }
}