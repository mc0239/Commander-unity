using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void StartGame()
	{
		SceneManager.LoadScene("level_1");
	}
	
	public void Controls()
	{
		SceneManager.LoadScene("controls");
	}
	
	public void Credits()
	{
		SceneManager.LoadScene("credits");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
