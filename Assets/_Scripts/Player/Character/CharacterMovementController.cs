using System;
using UnityEngine;

public class CharacterMovementController : IDisposable
{
    public bool IsActive { get; set; }
    public const float MovementSpeed = 5f;
    public const float JumpPower = 6f;
    
    private Transform _transform;
    private Rigidbody _rigidbody;
    private CharacterSide _side;
    private Vector3 _sideDirection;
    private bool _isGrounded = true;

    public CharacterMovementController(Transform transform, Rigidbody rigidbody)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        InputController.Swipe += OnSwiped;
    }

    public void Reset()
    {
        _transform.position = Vector3.left;
        _side = CharacterSide.Left;
        _sideDirection = Vector3.zero;
        _isGrounded = true;
    }

    public void Move()
    {   
        if (!IsActive)
            return;

        MoveForward();
        MoveSides();
        CheckGrounded();
    }

    private void MoveForward()
    {
        _transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);
    }

    private void MoveSides()
    {
        if (!_sideDirection.AreEqual(Vector3.zero))
        {
            Vector3 target = _transform.position;
            target.x = _sideDirection.x;
            _transform.position = Vector3.MoveTowards(_transform.position, target, Time.deltaTime * MovementSpeed);

            if (_transform.position.x.AreEqual(target.x))
            {
                _sideDirection = Vector3.zero;
            }
        }
    }

    private void CheckGrounded()
    {
        if (_isGrounded)
            return;

        _isGrounded = Physics.Raycast(_transform.position, _transform.TransformDirection(Vector3.down), .1f, ~LayerMask.NameToLayer("Road"));
    }

    private void OnSwiped(SwipeDirection direction)
    {
        if (!IsActive)
            return;

        switch (direction)
        {
            case SwipeDirection.Up: OnSwipeUp(); break;
            case SwipeDirection.Left: OnSwipeLeft(); break;
            case SwipeDirection.Right: OnSwipeRight(); break;
        }
    }

    private void OnSwipeUp()
    {
        if (!_isGrounded)
            return;

        _rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        _isGrounded = false;
    }

    private void OnSwipeRight()
    {
        if (_side != CharacterSide.Right)
        {
            _side++;
            _sideDirection = Vector3.right;
        }
    }

    private void OnSwipeLeft()
    {
        if (_side != CharacterSide.Left)
        {
            _side--;
            _sideDirection = Vector3.left;
        }
    }

    public void Dispose()
    {
        InputController.Swipe -= OnSwiped;
    }
}
