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

        animationController.UpdateMovement(0);
    }

    private void FixedUpdate()
    {
        if (!RoundManager.RoundRunning)
        {
            rb.linearVelocity = Vector2.zero;
            animationController.UpdateMovement(0);
            return;
        }

        rb.linearVelocity =
            moveInput * moveSpeed;

        animationController.UpdateMovement(
            rb.linearVelocity.magnitude);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!RoundManager.RoundRunning)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput =
            context.ReadValue<Vector2>();
    }
}