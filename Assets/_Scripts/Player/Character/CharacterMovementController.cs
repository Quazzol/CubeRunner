using System;
using UnityEngine;

public class CharacterMovementController : IDisposable
{
    public bool IsActive { get; set; }
    public const float MovementSpeed = 5f;
    public const float JumpPower = 6f;
    
    private Transform _transform;
    private Rigidbody _rigidbody;
    private float _sideMovement;
    private bool _isGrounded = true;

    public CharacterMovementController(Transform transform, Rigidbody rigidbody)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        InputController.Swerve += OnSwerve;
    }

    public void Reset()
    {
        _transform.position = Vector3.left;
        _sideMovement = 0;
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
        if (!_sideMovement.AreEqual(0))
        {
            Vector3 target = _transform.position;
            target.x = Mathf.Clamp(target.x + _sideMovement, -1, 1);
            
            _transform.position = target;
            _sideMovement = 0;
        }
    }

    private void CheckGrounded()
    {
        if (_isGrounded)
            return;

        _isGrounded = Physics.Raycast(_transform.position, _transform.TransformDirection(Vector3.down), .1f, ~LayerMask.NameToLayer("Road"));
    }

    private void OnSwerve(float movementPosition)
    {
        if (!IsActive)
            return;

        _sideMovement = movementPosition * MovementSpeed;
    }

    public void Dispose()
    {
        InputController.Swerve -= OnSwerve;
    }
}
