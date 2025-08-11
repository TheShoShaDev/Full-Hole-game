using System;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class HoleMover : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _moveSpeed = 20f;
    [SerializeField] private float _rotationSpeed = 20f;

    private Vector3 _moveDication;
    private InputAction _moveAction => _playerInput.actions["Move"];

    private void Start()
    {
        _moveAction.performed += OnMovePerformed;
        _moveAction.canceled += OnMoveCanceled;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveDiraction = new Vector3(_moveDication.x * Time.deltaTime * _moveSpeed, 0, _moveDication.y * Time.deltaTime * _moveSpeed);

        _rb.MovePosition(_rb.position + moveDiraction);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveDication = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveDication = Vector2.zero;
    }


    private void OnEnable()
    {
        _moveAction.performed += OnMovePerformed;
        _moveAction.canceled += OnMoveCanceled;

    }

    private void OnDisable()
    {
        _moveAction.performed -= OnMovePerformed;
        _moveAction.canceled -= OnMoveCanceled;

    }

    private void OnDestroy()
    {
        _moveAction.performed -= OnMovePerformed;
        _moveAction.canceled -= OnMoveCanceled;

    }

    private void OnValidate()
    {
        _rb ??= GetComponent<Rigidbody>();
        _playerInput ??= GetComponent<PlayerInput>();
    }

}
