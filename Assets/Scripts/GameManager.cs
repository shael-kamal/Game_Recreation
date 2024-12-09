using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro.Examples;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    [Header("UI Elements")]
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;
    public GameObject hudPanel;
    public Text scoreText;
    public Text coinText;
    //public Text livesText;
    //public Text levelText;
    public Text timerText; 
    private float timeRemaining = 300f;
    private bool timerRunning = true;

    [Header("Audio")]
    public AudioSource backgroundMusic;
    public AudioClip gameOverSound;
    public AudioClip victorySound;
    public AudioClip deathSound;
    public AudioClip powerupSound;
    public AudioClip powerdownSound;
    public AudioClip mushroomSound;

    [Header("Gameplay Settings")]
    public int startingLives = 3;
    public int pointsPerCoin = 10;
    public int pointsPerEnemy = 100;

    private int score = 0;
    private int lives;
    private int currentLevel = 1;
    private int coinsCollected = 0;
    [SerializeField] GameObject mario;


    private bool isGameOver = false;


    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(false);

        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        //mario.SetActive(false);
    }

    private void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                TimerEnded();
            }
        }
    }


    private void UpdateTimerUI()
    {
        
        timerText.text =  Mathf.FloorToInt(timeRemaining).ToString();
    }

    private void TimerEnded()
    {
        Debug.Log("Timer has ended!");
        
    }

    public void StartGame()
    {
        
        score = 0;
        lives = startingLives;
        currentLevel = 1;
        isGameOver = false;

        mainMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(true);
        UpdateUI();

        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        LoadLevel(currentLevel);
    }

    private void LoadLevel(int level)
    {
        //levelText.text = $"Level: {level}";
        // Replace with your actual level-loading logic
        //SceneManager.LoadScene("Level" + level); // Assumes levels are named Level1, Level2, etc.
    }

    private void UpdateUI()
    {
        //scoreText.text = $"score: {score}";

        // Update ScoreText
        if (score == 0)
        {
            scoreText.text = "00000";
        }
        else if (score <= 9 && score > 0)
        {
            scoreText.text = "0000" + score;
        }
        else if (score <= 99 && score >= 10)
        {
            scoreText.text = "000" + score;
        }
        else if (score <= 999 && score >= 100)
        {
            scoreText.text = "00" + score;
        }
        else if (score <= 9999 && score >= 1000)
        {
            scoreText.text = "0" + score;
        }



        // Updates CoinText
        if (coinsCollected == 0)
        {
            coinText.text = "00";
        }
        else if (coinsCollected <= 9 && score > 0)
        {
            coinText.text = "0" + coinsCollected;
        }
        else if (score <= 99 && score >= 10)
        {
            coinText.text = "" + coinsCollected;
        }
        //livesText.text = $"lives: {lives}";
    }

    public void AddScore(int points)
    {
        if (isGameOver) return;

        score += points;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (isGameOver) return;

        lives--;
        UpdateUI();

        if (lives <= 0)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        if (gameOverSound != null)
        {
            AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
        }

        Debug.Log("Game Over!");
    }

    public void Victory()
    {
        if (isGameOver) return;

        isGameOver = true;
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponentInChildren<Text>().text = "You Win!";

        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        if (victorySound != null)
        {
            AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position);
        }

        Debug.Log("Victory!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        //currentLevel++;
        //LoadLevel(currentLevel);
    }

    // Example triggers for coins and enemies
    public void CollectCoin()
    {
        coinsCollected += 2;
        AddScore(pointsPerCoin);
    }

    public void DefeatEnemy()
    {
        AddScore(pointsPerEnemy);
    }

    public void PlayerDeath()
    {
        backgroundMusic.Stop();
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
    }

    public void PowerUp()
    {
        AudioSource.PlayClipAtPoint(powerupSound, Camera.main.transform.position);
    }

    public void PowerDown()
    {
        AudioSource.PlayClipAtPoint(powerdownSound, Camera.main.transform.position);
    }

    public void MushroomAppears()
    {
        AudioSource.PlayClipAtPoint(mushroomSound, Camera.main.transform.position);
    }
}