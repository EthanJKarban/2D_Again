using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Movement2D : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _rb;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Movement Variables")]
    [SerializeField] private float _movementAcceleration = 50f;
    [SerializeField] private float _maxMoveSpeed = 12f;
    [SerializeField] private float _GroundlinearDrag = 10f;
    private float _horizontalDirection;
    private bool _changingdirection => (_rb.linearVelocity.x > 0f && _horizontalDirection < 0f) || (_rb.linearVelocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 5f;
    [SerializeField] private float _lowJumpFallMultiplier = 3f;
    [SerializeField] private int _extraJumps = 1;
    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;
    private float _jumpBufferTimer = 0.2f;
    private float _jumpBufferCounter;
    private int _extraJumpsValue;

    private bool _canJump => (Input.GetButtonDown("Jump")) && (_onGround || _extraJumpsValue > 0);

    [Header("Ground Collision Variables")]
    [SerializeField] private float _groundRaycastLength;
    [SerializeField] private Vector3 _rayCastOffset;
    private bool _onGround;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _horizontalDirection = GetInput().x;
        if (_canJump) Jump();
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTimer;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        MoveCharacter();
        ApplyingGroundLinearDrag();
        
        if (_onGround)
        {

            _extraJumpsValue = _extraJumps;
            ApplyingGroundLinearDrag();
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            ApplyingAirLinearDrag();
            fallMultiplier();
            _coyoteTimeCounter -= Time.deltaTime;
            if (_coyoteTimeCounter > 0 && _jumpBufferCounter < 0f)
            {
                CoyoteTime();
            }
            
        }
    }
    private void CoyoteTime()
    {
        _onGround = true;
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
    private void ApplyingGroundLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || _changingdirection)
        {
            _rb.linearDamping = _GroundlinearDrag;
        }
        else
        {
            _rb.linearDamping = 0f;
            
        }
    }
    private void ApplyingAirLinearDrag()
    {
        
            _rb.linearDamping = _airLinearDrag;
        
    }
    private void Jump()
    {
        if (!_onGround)

            _extraJumpsValue--;
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

    }

    private void fallMultiplier() // Fall speed
    {
        if (_rb.linearVelocity.y < 0)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else if (_rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }
    private void CheckCollisions()
    {
        _onGround = Physics2D.BoxCast(transform.position + _rayCastOffset, new Vector2(1, _groundRaycastLength), 0, Vector2.zero, 0, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position + _rayCastOffset, new Vector2(1, _groundRaycastLength));
    }
}
