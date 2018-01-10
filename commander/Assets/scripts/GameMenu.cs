using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public void RestartGame()
    {
        SceneManager.LoadScene("level_1");
    }
    
    public void ToMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }
}
