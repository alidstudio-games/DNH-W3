using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;

    [SerializeField] private TextMeshProUGUI numbersText;
    private bool isCountDownRunning;


    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    private const string BestScoreKey = "DNH_W3_BestScore";
    [SerializeField] private TextMeshProUGUI bestScoreText;
    private int bestScore;


    public bool isGameStarted;
    private bool isStartButtonClicked;

    [SerializeField] private GameObject guideTextObject;
    [SerializeField] private GameObject commandsPanel;

    [SerializeField] private GameObject bird_1;
    [SerializeField] private GameObject bird_1Circle;

    [SerializeField] private GameObject bird_2;
    [SerializeField] private GameObject bird_2Circle;

    [SerializeField] private GameObject bird_3;
    [SerializeField] private GameObject bird_3Circle;

    [SerializeField] private GameObject selectionControls;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button restartButton;

    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI lastBestScoreText;

    private AudioManager audioManagerScript;

    private void Awake()
    {
        audioManagerScript = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

        score = 0;
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        UpdateBestScoreText();

        isGameOver = false;
        isGameStarted = false;
        isStartButtonClicked = false;

        gameOverPanel.SetActive(false);

        bird_2.SetActive(false);
        bird_2Circle.SetActive(false);

        bird_3.SetActive(false);
        bird_3Circle.SetActive(false);

        bird_1.SetActive(true);
        bird_1Circle.SetActive(true);

        if (numbersText != null)
        {
            numbersText.gameObject.SetActive(false);
        }

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }

        if (commandsPanel != null)
        {
            commandsPanel.SetActive(true);
        }

        if (bestScoreText != null)
        {
            bestScoreText.gameObject.SetActive(true);
        }

        if (guideTextObject != null)
        {
            guideTextObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned on GameManager!");
            return;
        }

        UpdateScoreText();
    }

    private void Update()
    {
        if (!isStartButtonClicked)
        {
            return;
        }

        bool gPressed = Keyboard.current != null &&
                        Keyboard.current.gKey.wasPressedThisFrame;

        if (gPressed && !isGameStarted && !isGameOver && !isCountDownRunning)
        {
            PlayButtonClick();
            isCountDownRunning = true;

            if (guideTextObject != null)
            {
                guideTextObject.SetActive(false);
            }

            StartCoroutine(CountDownBeforeStart());
        }
    }

    private System.Collections.IEnumerator CountDownBeforeStart()
    {
        numbersText.gameObject.SetActive(true);

        for (int number = 3; number > 0; number--)
        {
            numbersText.text = number.ToString();
            yield return new WaitForSeconds(1f);
        }

        numbersText.gameObject.SetActive(false);
        isGameStarted = true;
        isCountDownRunning = false;

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
            UpdateScoreText();
        }
    }

    private void UpdateBestScoreText()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = bestScore.ToString();
        }
    }

    public void AddScore()
    {
        if (isGameOver)
        {
            return;
        }

        if (!isGameStarted)
        {
            return;
        }

        score++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (!isGameStarted)
        {
            return;
        }

        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void EndGame()
    {
        if (isGameOver)
        {
            return;
        }

        scoreText.gameObject.SetActive(false);

        isGameOver = true;

        if (score > bestScore)
        {
            bestScore = score;

            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            PlayerPrefs.Save();

            UpdateBestScoreText();
        }
    }

    private void PlayButtonClick()
    {
        audioManagerScript.PlayButtonClick();
    }

    public void StartButton()
    {
        PlayButtonClick();
        isStartButtonClicked = true;

        selectionControls.SetActive(false);
        bestScoreText.gameObject.SetActive(false);
        commandsPanel.SetActive(false);
        guideTextObject.SetActive(true);
    }

    public void BackButton()
    {
        PlayButtonClick();

        SceneManager.LoadScene("MainMenu");
    }

    public void MakeActiveBird_1()
    {
        PlayButtonClick();

        bird_1.SetActive(true);
        bird_1Circle.SetActive(true);

        bird_2.SetActive(false);
        bird_2Circle.SetActive(false);

        bird_3.SetActive(false);
        bird_3Circle.SetActive(false);
    }

    public void MakeActiveBird_2()
    {
        PlayButtonClick();

        bird_1.SetActive(false);
        bird_1Circle.SetActive(false);

        bird_2.SetActive(true);
        bird_2Circle.SetActive(true);

        bird_3.SetActive(false);
        bird_3Circle.SetActive(false);
    }

    public void MakeActiveBird_3()
    {
        PlayButtonClick();

        bird_1.SetActive(false);
        bird_1Circle.SetActive(false);

        bird_2.SetActive(false);
        bird_2Circle.SetActive(false);

        bird_3.SetActive(true);
        bird_3Circle.SetActive(true);
    }

    public void MainMenuButton()
    {
        PlayButtonClick();

        SceneManager.LoadScene("MainMenu");
    }

    public void RestartButton()
    {
        PlayButtonClick();

        SceneManager.LoadScene("Game");
    }

    public void GameOverPanel()
    {
        currentScoreText.text = score.ToString();
        lastBestScoreText.text = bestScore.ToString();

        gameOverPanel.SetActive(true);
    }
}
