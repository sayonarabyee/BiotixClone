using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
	public void PauseGame()
	{
		Time.timeScale = 0f;
	}
	public void ResumeGame()
	{
		Time.timeScale = 1f;
	}
	public void BackToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	public void Restart()
	{
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}
}
