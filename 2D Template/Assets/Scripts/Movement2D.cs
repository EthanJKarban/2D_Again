using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _rb;


    [Header("Movement Variables")]
    [SerializeField] private float _movementAcceleration = 50f;
    [SerializeField] private float _maxMoveSpeed = 12f;
    [SerializeField] private float _linearDrag = 10f;
    private float _horizontalDirection;
    private bool _changingdirection =>  (_rb.linearVelocity.x >  0f && _horizontalDirection < 0f) || (_rb.linearVelocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float _jumpForce = 12f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _horizontalDirection = GetInput().x;
        if (Input.GetButtonDown("Jump")) Jump();
        {

        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        ApplyingLinearDrag();
    }


    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        _rb.AddForce(new Vector2(_horizontalDirection, 0f) * _movementAcceleration);

        if (Mathf.Abs(_rb.linearVelocity.x) > _maxMoveSpeed)
            _rb.linearVelocity = new Vector2(Mathf.Sign(_rb.linearVelocity.x) * _maxMoveSpeed, _rb.linearVelocity.y);
    }
    private void ApplyingLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || _changingdirection)
        {
            _rb.linearDamping = _linearDrag;
        }
        else
        {
            _rb.linearDamping = 0f;
        }
    }
    private void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
