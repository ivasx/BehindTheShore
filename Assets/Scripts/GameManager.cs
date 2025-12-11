using UnityEngine;
using TMPro; // Для тексту
using UnityEngine.SceneManagement; // Для перезавантаження сцен

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Налаштування UI")]
    [SerializeField] private TextMeshProUGUI scoreText; // Текст лічильника
    [SerializeField] private GameObject winScreen; // Панель перемоги (Win Screen)

    private int totalCoins;
    private int collectedCoins;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Обов'язково запускаємо час, бо після минулої перемоги він міг бути зупинений
        Time.timeScale = 1f;

        // Рахуємо монетки
        totalCoins = FindObjectsOfType<Coin>().Length;
        collectedCoins = 0;
        
        UpdateScoreText();
        
        // Ховаємо екран перемоги на старті
        if (winScreen != null) 
            winScreen.SetActive(false);
    }

    public void CoinCollected()
    {
        collectedCoins++;
        UpdateScoreText();

        if (collectedCoins >= totalCoins)
        {
            WinGame();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Coins: {collectedCoins} / {totalCoins}";
        }
    }

    private void WinGame()
    {
        // Показуємо екран перемоги
        if (winScreen != null)
            winScreen.SetActive(true);
        
        // ЗУПИНЯЄМО ГРУ (фізику, рух персонажа)
        // Оскільки рух персонажа у вас у FixedUpdate, це спрацює ідеально.
        Time.timeScale = 0f; 
    }

    // --- ФУНКЦІЇ ДЛЯ КНОПОК ---

    public void RestartLevel()
    {
        // Перезавантажуємо поточну сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        // Завантажуємо сцену меню (переконайтеся, що вона у вас є і додана в Build Settings)
        // Якщо сцена називається інакше, змініть "MainMenu" на вашу назву
        Time.timeScale = 1f; // Повертаємо час, щоб меню не зависло
        SceneManager.LoadScene("MainMenu"); 
    }
}