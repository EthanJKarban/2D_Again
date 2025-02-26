using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] // This script can only be added to a Gameobject w/ a Rigidbody2D
public class MovementExtra : MonoBehaviour
{
    private Rigidbody2D _rib;
    private Vector2 _moveAmount;

    public float movementSpeed;



    void Awake()
    {   // Set the _rb variable equal to this GameObject's rigidbody.
        _rib = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        _rib.linearVelocityX = _moveAmount.x * movementSpeed;
    }


    // Handles the movement code. If you couldn't comprehend the Handle movement handling the movement you need to grab a book and read it. 
    public void HandleMovement(InputAction.CallbackContext ctx)
    {
       _moveAmount = ctx.ReadValue<Vector2>();
    }

    //public void
    //    _rib.linearVelocityY = 10;
}
