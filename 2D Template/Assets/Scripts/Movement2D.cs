using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _rb;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Movement Variables")]
    [SerializeField] private float _movementAcceleration = 50f;
    [SerializeField] private float _maxMoveSpeed = 12f;
    [SerializeField] private float _linearDrag = 10f;
    private float _horizontalDirection;
    private bool _changingdirection =>  (_rb.linearVelocity.x >  0f && _horizontalDirection < 0f) || (_rb.linearVelocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float _airLinearDrag = 2.5f;
    private bool _canJump => (Input.GetButtonDown("Jump")) && _onGround;

    [Header("Ground Collision Variables")]
    [SerializeField] private float _groundRaycastLength;
    private bool _onGround;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _horizontalDirection = GetInput().x;
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        MoveCharacter();
        ApplyingLinearDrag();
        if (_canJump) Jump();
        if (_onGround)
        {
            ApplyingLinearDrag();
        }
        else
        {
            ApplyingLinearDrag();
        }
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
    private void ApplyingAirLinearDrag()
    {
        {
            //_rb.drag = _airLinearDrag = 0f;
        }
    }
    private void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckCollisions()
    {
        _onGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRaycastLength, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down *  _groundRaycastLength);
    }
}
