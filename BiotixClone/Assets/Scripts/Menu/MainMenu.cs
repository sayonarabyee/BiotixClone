using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Singleton<MainMenu>
{
	[SerializeField] Button[] lvlButtons;
	private void Start()
	{
		int openedLevel = PlayerPrefs.GetInt("currentOpenedLevel", 0);

		for (int i = 0; i < lvlButtons.Length; i++)
		{
			if (i  > openedLevel)
				lvlButtons[i].interactable = false;
		}
	}
	public void LoadLevel(int level) 
	{
		LevelLoader.currentOpenedLevel = level;
		SceneManager.LoadSceneAsync("Game");
	}
	public void QuitGame() => Application.Quit();
}
