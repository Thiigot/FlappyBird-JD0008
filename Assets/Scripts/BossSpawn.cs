using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform stopPoint;
    [SerializeField] private Transform player;


    [Header("Spawn Condition")]
    [SerializeField] private int bossSpawnScore = 5;

    public bool bossSpawned = false;


    public void CheckBossSpawn(int currentScore)
    {
        if (bossSpawned || currentScore < bossSpawnScore)
            return;

        SpawnBoss();
    }
    private void SpawnBoss()
    {
        if (bossPrefab == null || spawnPoint == null ||
            stopPoint == null)
        {
            Debug.LogError("BossSpawner: Referências não configuradas!");
            return;
        }

        GameObject boss = Instantiate(
            bossPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        BossMovement movement = boss.GetComponent<BossMovement>();

        if (movement != null)
        {
            movement.Initialize(stopPoint.position, player);
        }

        bossSpawned = true;
    }
}
