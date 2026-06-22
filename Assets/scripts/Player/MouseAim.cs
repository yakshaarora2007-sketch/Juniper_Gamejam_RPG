using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAim : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePos =
            cam.ScreenToWorldPoint(
                Mouse.current.position.ReadValue());

        mousePos.z = 0f;

        Vector2 direction =
            mousePos - transform.position;

        float angle =
            Mathf.Atan2(direction.y, direction.x) *
            Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0f, 0f, angle + 90f);
    }
}