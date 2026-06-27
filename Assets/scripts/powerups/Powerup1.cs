using UnityEngine;

public class PowerUp1 : MonoBehaviour
{
    private UpgradeMenuUI upgradeMenu;

    private void Start()
    {
        upgradeMenu =
            FindFirstObjectByType<UpgradeMenuUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        RoundManager.RoundRunning = false;

        upgradeMenu.OpenMenu();

        Destroy(gameObject);
    }
}