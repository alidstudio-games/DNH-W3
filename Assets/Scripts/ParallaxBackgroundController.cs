using UnityEngine;

public class ParallaxBackgroundController : MonoBehaviour
{
    // Far Mountains A/B
    private GameObject farMountainsA;
    private GameObject farMountainsB;

    // Mid Forest Lake A/B
    private GameObject midForestLakeA;
    private GameObject midForestLakeB;

    // Foreground A/B
    private GameObject foregroundA;
    private GameObject foregroundB;

    // Speeds for each layer

    [SerializeField] private float farMountainsSpeed = 0.3f;
    [SerializeField] private float midForestLakeSpeed = 0.8f;
    [SerializeField] private float foregroundSpeed = 2.0f;

    private SpriteRenderer farMountainsARenderer;
    private SpriteRenderer farMountainsBRenderer;

    private SpriteRenderer midForestLakeARenderer;
    private SpriteRenderer midForestLakeBRenderer;

    private SpriteRenderer foregroundARenderer;
    private SpriteRenderer foregroundBRenderer;

    private float cameraLeftX;

    [SerializeField] private GameObject gameManager; // -> referense inspektorda 💯 ulangan
    private GameManager gameManagerScript;

    void Awake()
    {
        // Find Far Mountains A/B by name
        Transform farMountainsATransform = transform.Find("Far Mountains A");
        farMountainsA = farMountainsATransform != null ? farMountainsATransform.gameObject : null;
        Transform farMountainsBTransform = transform.Find("Far Mountains B");
        farMountainsB = farMountainsBTransform != null ? farMountainsBTransform.gameObject : null;

        // Find Mid Forest Lake A/B by name
        Transform midForestLakeATransform = transform.Find("Mid Forest Lake A");
        midForestLakeA = midForestLakeATransform != null ? midForestLakeATransform.gameObject : null;
        Transform midForestLakeBTransform = transform.Find("Mid Forest Lake B");
        midForestLakeB = midForestLakeBTransform != null ? midForestLakeBTransform.gameObject : null;

        // Find Foreground A/B by name
        Transform foregroundATransform = transform.Find("Foreground A");
        foregroundA = foregroundATransform != null ? foregroundATransform.gameObject : null;
        Transform foregroundBTransform = transform.Find("Foreground B");
        foregroundB = foregroundBTransform != null ? foregroundBTransform.gameObject : null;

        if (farMountainsA == null || farMountainsB == null || midForestLakeA == null || midForestLakeB == null || foregroundA == null || foregroundB == null)
        {
            Debug.LogError("One or more background layers are not assigned!");
            enabled = false;
            return;
        }

        farMountainsARenderer = farMountainsA.GetComponent<SpriteRenderer>();
        farMountainsBRenderer = farMountainsB.GetComponent<SpriteRenderer>();

        foregroundARenderer = foregroundA.GetComponent<SpriteRenderer>();
        foregroundBRenderer = foregroundB.GetComponent<SpriteRenderer>();

        midForestLakeARenderer = midForestLakeA.GetComponent<SpriteRenderer>();
        midForestLakeBRenderer = midForestLakeB.GetComponent<SpriteRenderer>();

        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    void Start()
    {
        cameraLeftX = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript == null || gameManagerScript.isGameOver)
        {
            return;
        }

        FarMountainsA_BMove();
        ConnectToEachOtherFarMountains();

        MidForestLakeA_BMove();
        ConnectToEachOtherMidForestLake();

        ForegroundA_BMove();
        ConnectToEachOtherForeground();
    }

    private void FarMountainsA_BMove()
    {
        Vector3 farMountainsAPosition = farMountainsA.transform.position;
        farMountainsAPosition.x -= farMountainsSpeed * Time.deltaTime;
        farMountainsA.transform.position = farMountainsAPosition;

        Vector3 farMountainsBPosition = farMountainsB.transform.position;
        farMountainsBPosition.x -= farMountainsSpeed * Time.deltaTime;
        farMountainsB.transform.position = farMountainsBPosition;
    }

    private void MidForestLakeA_BMove()
    {
        Vector3 midForestLakeAPosition = midForestLakeA.transform.position;
        midForestLakeAPosition.x -= midForestLakeSpeed * Time.deltaTime;
        midForestLakeA.transform.position = midForestLakeAPosition;

        Vector3 midForestLakeBPosition = midForestLakeB.transform.position;
        midForestLakeBPosition.x -= midForestLakeSpeed * Time.deltaTime;
        midForestLakeB.transform.position = midForestLakeBPosition;
    }

    private void ForegroundA_BMove()
    {
        Vector3 foregroundAPosition = foregroundA.transform.position;
        foregroundAPosition.x -= foregroundSpeed * Time.deltaTime;
        foregroundA.transform.position = foregroundAPosition;

        Vector3 foregroundBPosition = foregroundB.transform.position;
        foregroundBPosition.x -= foregroundSpeed * Time.deltaTime;
        foregroundB.transform.position = foregroundBPosition;

    }

    private void ConnectToEachOtherFarMountains()
    {
        if (farMountainsARenderer.bounds.max.x < cameraLeftX)
        {
            Vector3 position = farMountainsA.transform.position;
            position.x = farMountainsBRenderer.bounds.max.x + farMountainsARenderer.bounds.extents.x;
            farMountainsA.transform.position = position;
        }

        if (farMountainsBRenderer.bounds.max.x < cameraLeftX)
        {
            Vector3 position = farMountainsB.transform.position;
            position.x = farMountainsARenderer.bounds.max.x + farMountainsBRenderer.bounds.extents.x;
            farMountainsB.transform.position = position;
        }
    }

    private void ConnectToEachOtherMidForestLake()
    {
        if (midForestLakeARenderer.bounds.max.x < cameraLeftX)
        {
            Vector3 position = midForestLakeA.transform.position;
            position.x = midForestLakeBRenderer.bounds.max.x + midForestLakeARenderer.bounds.extents.x;
            midForestLakeA.transform.position = position;
        }

        if (midForestLakeBRenderer.bounds.max.x < cameraLeftX)
        {
            Vector3 position = midForestLakeB.transform.position;
            position.x = midForestLakeARenderer.bounds.max.x + midForestLakeBRenderer.bounds.extents.x;
            midForestLakeB.transform.position = position;
        }
    }

    private void ConnectToEachOtherForeground()
    {
        if (foregroundARenderer.bounds.max.x < cameraLeftX)
        {
            Vector3 position = foregroundA.transform.position;
            position.x = foregroundBRenderer.bounds.max.x + foregroundARenderer.bounds.extents.x;
            foregroundA.transform.position = position;
        }

        if (foregroundBRenderer.bounds.max.x < cameraLeftX)
        {
            Vector3 position = foregroundB.transform.position;
            position.x = foregroundARenderer.bounds.max.x + foregroundBRenderer.bounds.extents.x;
            foregroundB.transform.position = position;
        }
    }
}
