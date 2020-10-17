using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameObject))]
public class EndGameCheck : Singleton<EndGameCheck>
{
	private int nextSceneLoad;
	private List<Cell> cells = new List<Cell>();
	[SerializeField] SetTeam playerTeam;
	[SerializeField] SetTeam botTeam;

	private void Start()
	{
		if (Advertisement.isSupported)
			Advertisement.Initialize("3864625", false);

		nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;

		cells = FindObjectsOfType<Cell>().ToList();
	}
	public void Check()
	{
		var botCount = cells.Where(n => n.Team == botTeam).Count();
		var playerCount = cells.Where(n => n.Team == playerTeam).Count();

		if (botCount <= 0)
		{
			ShowADSWin();
		}
		else if (playerCount <= 0)
		{
			ShowADSLose();
		}
	}

	private void Win()
	{
		/*		loader.currentOpenedLevel++;
				PlayerPrefs.SetInt("currentOpenedLevel", loader.currentOpenedLevel);*/
		LevelLoader.Instance.LoadNextLevel();

		/*if (SceneManager.GetActiveScene().buildIndex == 4)
			SceneManager.LoadScene("MainMenu");
		else
		{
			SceneManager.LoadScene(nextSceneLoad);

			if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
				PlayerPrefs.SetInt("levelAt", nextSceneLoad);
		}*/
	}
	private void Lose()
	{
		if (SceneManager.GetActiveScene().buildIndex == 4)
			SceneManager.LoadScene("MainMenu");
		else
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	private void ShowADSWin()
	{
		if (Advertisement.IsReady())
		{
			var options = new ShowOptions { resultCallback = ShowResultWin };
			Advertisement.Show(options);
			//Advertisement.Show();
		}
	}
	private void ShowADSLose()
	{
		if (Advertisement.IsReady())
		{
			var options = new ShowOptions { resultCallback = ShowResultLose };
			Advertisement.Show(options);
			//Advertisement.Show();
		}
	}
	private void ShowResultWin(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				Debug.Log("The ad was successfully shown.");
				Win();
				break;
			case ShowResult.Skipped:
				Debug.Log("The ad was skipped before reaching the end.");
				Win();
				break;
		}
	}
	private void ShowResultLose(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				Debug.Log("The ad was successfully shown.");
				Lose();
				break;
			case ShowResult.Skipped:
				Debug.Log("The ad was skipped before reaching the end.");
				Lose();
				break;
		}
	}
}
