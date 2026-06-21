using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("References")]
    public Transform Aim;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;

    private Vector2 moveInput;
    private Vector2 lastDirection = Vector2.down;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    private void Update()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastDirection = moveInput.normalized;
        }

        float angle =
            Mathf.Atan2(
                lastDirection.y,
                lastDirection.x
            ) * Mathf.Rad2Deg;

        Aim.rotation =
            Quaternion.Euler(0, 0, angle + 90);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}