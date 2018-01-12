using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool Paused = false;

    public GameObject pauseMenu;
        
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!Paused)
            {
                Pause();
                Paused = true;
            }
            else
            {
                Unpause();
                Paused = false;
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level_1");
    }
    
    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("main_menu");
    }
    
}
