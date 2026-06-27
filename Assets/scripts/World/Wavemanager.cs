using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private GameObject wave1;
    [SerializeField] private GameObject wave2;

    [SerializeField] private GameObject wave1PowerUpPrefab;
[SerializeField] private GameObject wave2PowerUpPrefab;
    [SerializeField] private Transform powerupSpawn;

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
        if (EnemiesRemaining() == 0)
{
    if (currentWave == 1)
    {
        Instantiate(
            wave1PowerUpPrefab,
            powerupSpawn.position,
            Quaternion.identity);
    }
    else
    {
        Instantiate(
            wave2PowerUpPrefab,
            powerupSpawn.position,
            Quaternion.identity);
    }
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