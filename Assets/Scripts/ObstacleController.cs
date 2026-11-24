using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float destroyXPosition = -20f;

    private new Rigidbody2D rigidbody2D;
    private GameManager gameManagerScript;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        GameObject gameManagerObject = GameObject.Find("Game Manager");

        if (gameManagerObject != null)
        {
            gameManagerScript = gameManagerObject.GetComponent<GameManager>();
        }

        if (rigidbody2D == null)
        {
            Debug.LogError("ObstacleController needs a Rigidbody2D on the same GameObject!");
            enabled = false;
            return;
        }

        if (gameManagerScript == null)
        {
            Debug.LogError("GameManager was not found or has no GameManager component!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (transform.position.x <= destroyXPosition)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (rigidbody2D == null || gameManagerScript == null)
        {
            return;
        }

        if (gameManagerScript.isGameOver)
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            return;
        }

        rigidbody2D.linearVelocity = new Vector2(-moveSpeed, 0f);
    }
}
