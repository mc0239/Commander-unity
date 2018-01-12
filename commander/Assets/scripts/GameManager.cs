using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool _paused = false;
    private bool _playerDied = false;

    public GameObject pauseMenu;
    public GameObject deathMsg;
    public GameObject msgText;
    public GameObject player;
        
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!_paused)
            {
                Pause();
                _paused = true;
            }
            else
            {
                Unpause();
                _paused = false;
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    
    public void DeathMsg(string message)
    {
        if (!_playerDied)
        {
            _playerDied = true;
            msgText.GetComponent<Text>().text = message;
            deathMsg.SetActive(true);
            Invoke("ResetPlayer", 2f);
        }
       
    }

    public void ResetPlayer()
    {
        deathMsg.SetActive(false);
        _playerDied = false;
        player.transform.position = Vector3.zero;
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
