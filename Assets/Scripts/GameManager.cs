using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject winScreen;

    private int totalRunes;
    private int collectedRunes;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;

        totalRunes = FindObjectsOfType<RunesClaim>().Length;
        collectedRunes = 0;

        UpdateScoreText();

        if (winScreen != null)
            winScreen.SetActive(false);
    }

    public void RuneCollected()
    {
        collectedRunes++;
        UpdateScoreText();

        if (collectedRunes >= totalRunes)
        {
            WinGame();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Runes: {collectedRunes} / {totalRunes}";
        }
    }

    private void WinGame()
    {
        if (winScreen != null)
            winScreen.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}