using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 4f;    // Tugma bosilganda yuqoriga sakrash impulsi
    [SerializeField] private float normalGravity = 2.0f; // Tugma bosilmaganda pastga tez sho'ng'ish uchun gravitatsiya
    private bool gravityActivated = false;

    private bool jumpRequested;

    private bool isCollided;
    private new Rigidbody2D rigidbody2D;

    private Animator animator;
    private float boundaryY = 4.5f; // Yuqori chegarani belgilash

    private GameObject gameManager;
    private GameManager gameManagerScript;


    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        gameManager = GameObject.Find("Game Manager");
        if(gameManager != null)
        {
            gameManagerScript = gameManager.GetComponent<GameManager>();
        }
    }

    void Start()
    {
        isCollided = false;

        if (rigidbody2D != null)
        {
            rigidbody2D.gravityScale = 0f;
        }
    }

    private void Update()
    {
        if (gameManagerScript == null || !gameManagerScript.isGameStarted || gameManagerScript.isGameOver)
        {
            return;
        }

        bool spacePressed = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
        bool mousePressed = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        if (spacePressed || mousePressed)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (gameManagerScript == null || !gameManagerScript.isGameStarted)
        {
            return;
        }

        if (!gravityActivated)
        {
            rigidbody2D.gravityScale = normalGravity;
            gravityActivated = true;
        }

        if (isCollided)
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            rigidbody2D.gravityScale = 0f; // Urilgandan keyin pastga tushib ketmasligi uchun
            return;
        }

        float currentYVelocity = rigidbody2D.linearVelocity.y;

        if (jumpRequested)
        {
            currentYVelocity = jumpForce;
            jumpRequested = false;
        }

        rigidbody2D.linearVelocity = new Vector2(0f, currentYVelocity);

        if (rigidbody2D.position.y >= boundaryY)
        {
            Vector2 position = rigidbody2D.position;
            position.y = boundaryY;
            rigidbody2D.position = position;

            if (rigidbody2D.linearVelocity.y > 0f)
            {
                rigidbody2D.linearVelocity = Vector2.zero;
            }
        }

        float birdRotation = Mathf.Clamp(rigidbody2D.linearVelocity.y * 5f, -30f, 30f);
        transform.rotation = Quaternion.Euler(0, 0, birdRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Sky_Ground"))
        {
            isCollided = true;

            gameManagerScript.EndGame();
            gameManagerScript.GameOverPanel();

            if (animator != null)
            {
                animator.enabled = false;
            }
        }
    }
}
