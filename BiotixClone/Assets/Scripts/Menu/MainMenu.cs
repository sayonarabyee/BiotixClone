using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField] Button[] lvlButtons;
	
	void Start()
	{
		int levelAt = PlayerPrefs.GetInt("levelAt", 0);

		for (int i = 0; i < lvlButtons.Length; i++)
		{
			if (i + 1 > levelAt)
				lvlButtons[i].interactable = false;
		}
	}
	public void LoadLevel1()
	{
		SceneManager.LoadScene("Level1");
	}
	public void LoadLevel2()
	{
		SceneManager.LoadScene("Level2");
	}
	public void LoadLevel3()
	{
		SceneManager.LoadScene("Level3");
	}
	public void QuitGame() => Application.Quit();
}
