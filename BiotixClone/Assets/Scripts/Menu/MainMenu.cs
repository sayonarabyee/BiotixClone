using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField] Button[] lvlButtons;
	
	void Start()
	{
		int openedLevel = PlayerPrefs.GetInt("currentOpenedLevel", 0);

		for (int i = 0; i < lvlButtons.Length; i++)
		{
			if (i  > openedLevel)
				lvlButtons[i].interactable = false;
		}
	}
	public void LoadLevel() => SceneManager.LoadScene("Game");
	public void QuitGame() => Application.Quit();
}
