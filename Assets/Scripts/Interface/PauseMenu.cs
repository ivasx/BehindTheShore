using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused = false;
    public GameObject pauseGameMenu; 
    public GameObject deathGameMenu; 

    void Update()
    {
        if (deathGameMenu.activeSelf) 
        {
            return; 
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    
    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}