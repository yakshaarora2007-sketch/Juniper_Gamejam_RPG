using UnityEngine;
using UnityEngine.InputSystem;
public class movement : MonoBehaviour
{
   

    [SerializeField] private float movespeed = 5f;

    private Rigidbody2D rb;

    private Vector2 move_input;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity= movespeed*move_input;
    }
    public void Move(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }
}
