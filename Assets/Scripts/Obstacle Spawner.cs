using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePair;

    [SerializeField] private float spawnPosX = 8f;
    [SerializeField] private float minSpawnPosY = 0.4f;
    [SerializeField] private float maxSpawnPosY = 4.4f;

    private float spawnDelay = 2f;
    private float spawnInterval = 3f;

    private GameManager gameManagerScript;
    private bool hasStartedSpawning;

    private void Awake()
    {
        GameObject gameManagerObject = GameObject.Find("Game Manager");

        if (gameManagerObject != null)
        {
            gameManagerScript = gameManagerObject.GetComponent<GameManager>();
        }
    }

    private void Start()
    {
        if (obstaclePair == null)
        {
            Debug.LogError("Obstacle pair prefab is not assigned!");
            enabled = false;
            return;
        }

        if (gameManagerScript == null)
        {
            Debug.LogError("GameManager was not found or has no GameManager component!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (!hasStartedSpawning && gameManagerScript.isGameStarted)
        {
            InvokeRepeating(nameof(SpawnObstaclePair), spawnDelay, spawnInterval);
            hasStartedSpawning = true;
        }
    }

    private void SpawnObstaclePair()
    {
        if (!gameManagerScript.isGameStarted)
        {
            return;
        }

        if (gameManagerScript.isGameOver)
        {
            CancelInvoke(nameof(SpawnObstaclePair));
            return;
        }

        Vector3 spawnPosition = new Vector3(spawnPosX, Random.Range(minSpawnPosY, maxSpawnPosY), 0f);

        Instantiate(obstaclePair, spawnPosition, Quaternion.identity);
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(SpawnObstaclePair));
    }
}
