using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private PlayerAnimationController animationController;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animationController =
            GetComponent<PlayerAnimationController>();

        animationController.UpdateMovement(
            rb.linearVelocity.magnitude);
    }

    private void FixedUpdate()
    {
        

    rb.linearVelocity = moveInput * moveSpeed;

    Debug.Log("Velocity = " + rb.linearVelocity);
    Debug.Log("Magnitude = " + rb.linearVelocity.magnitude);

    animationController.UpdateMovement(rb.linearVelocity.magnitude);

    }

    public void Move(InputAction.CallbackContext context)
    {
  

    moveInput = context.ReadValue<Vector2>();

    Debug.Log("Input = " + moveInput);

    }
}