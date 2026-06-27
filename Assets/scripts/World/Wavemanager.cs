using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private GameObject wave1;
    [SerializeField] private GameObject wave2;

  
    private WeaponWheelManager wheelManager;
private UpgradeMenuUI upgradeMenu;

    private int currentWave = 1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        wave1.SetActive(true);
        wave2.SetActive(false);
        
    }

   public void EnemyKilled()
{
    int remaining = EnemiesRemaining() - 1;

    Debug.Log("Enemies Remaining = " + remaining);

    if (remaining > 0)
        return;

    if (currentWave == 1)
    {
        Debug.Log("Wave 1 Cleared");

        currentWave = 2;

        wave2.SetActive(true);

        RoundManager.RoundRunning = false;

        Object.FindFirstObjectByType<WeaponWheelManager>()
    .StartWeaponSelection();
    }
    else
    {
        Debug.Log("Level Complete");

        RoundManager.Instance.EndRound();

        Object.FindFirstObjectByType<UpgradeMenuUI>()
    .OpenMenu();
    }
}
   int EnemiesRemaining()
{
    GameObject wave =
        currentWave == 1 ? wave1 : wave2;

    return wave.GetComponentsInChildren<navmesh_enemy>().Length;
}
    public void StartWave2()
    {
        currentWave = 2;

        wave2.SetActive(true);
    }

    public bool IsFinalWave()
    {
        return currentWave == 2;
    }
}