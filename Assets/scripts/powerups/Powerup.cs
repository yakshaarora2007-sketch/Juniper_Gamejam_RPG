using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private WeaponWheelManager wheelManager;

    private void Start()
    {
        wheelManager =
            FindFirstObjectByType<WeaponWheelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        

        WaveManager.Instance.StartWave2();

        wheelManager.StartWeaponSelection();

        Destroy(gameObject);
    }
}