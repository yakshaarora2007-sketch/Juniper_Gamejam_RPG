using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerCombat playerCombat;

    private Camera cam;
    private Quaternion defaultRotation;

    private void Start()
    {
        cam = Camera.main;

        defaultRotation = transform.rotation;

        if (playerCombat == null)
            playerCombat = GetComponentInParent<PlayerCombat>();
    }

    private void Update()
    {
        // Freeze during countdown
        if (!RoundManager.RoundRunning)
        {
            transform.rotation = defaultRotation;
            return;
        }

        // Face down unless an attack animation is currently playing
        if (playerCombat != null &&
            !playerCombat.IsAttackingAnimation)
        {
            transform.rotation = defaultRotation;
            return;
        }

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
            Quaternion.Euler(
                0f,
                0f,
                angle + 90f);
    }
}