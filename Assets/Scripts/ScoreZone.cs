using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private GameManager gameManager;
    private bool hasScored;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager was not found for this ScoreZone!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasScored || !other.CompareTag("Player"))
        {
            return;
        }

        if (gameManager == null)
        {
            Debug.LogError("ScoreZone cannot add score because GameManager is missing!");
            return;
        }

        hasScored = true;
        gameManager.AddScore();
    }
}
