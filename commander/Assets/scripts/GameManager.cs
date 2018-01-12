using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool _paused = false;
    private bool _playerDied = false;

    public GameObject pauseMenu;
    public GameObject winMsg;
    public GameObject deathMsg;
    public Text msgText;
    //public Text scoreText;
    public Text totalText;
    public GameObject player;
        
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!_paused && Time.timeScale>0.5f)
            {
                Pause();
            }
            else if(_paused)
            {
                Unpause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _paused = true;
        pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        _paused = false;
    }

    public void WinMsg()
    {
        Time.timeScale = 0;
        
        //int Score = Int32.Parse(GameObject.Find("Score").GetComponent<Text>().text);
        //int Bullets = Int32.Parse(GameObject.Find("Bullets").GetComponent<Text>().text);
        //scoreText.text = Score + "\n" + Bullets;
        totalText.text = GameObject.Find("Score").GetComponent<Text>().text;
        winMsg.SetActive(true);
    }
    
    public void DeathMsg(string message)
    {
        Time.timeScale = 0.1f;
        if (!_playerDied)
        {
            _playerDied = true;
            msgText.text = message;
            deathMsg.SetActive(true);
            Invoke("ResetPlayer", 0.25f);
        }
       
    }

    public void ResetPlayer()
    {
        Time.timeScale = 1;
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
