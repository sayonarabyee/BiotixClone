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
			ShowADS();
			Invoke("CheckLevelIfWin", 5f);
		}
		else if (playerCount <= 0)
		{
			ShowADS();
			Invoke("CheckLeveIfLose", 5f);
		}	
	}
	private void ShowADS()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
	private void CheckLevelIfWin()
	{
		if (SceneManager.GetActiveScene().buildIndex == 4)
			SceneManager.LoadScene("MainMenu");
		else
		{
			SceneManager.LoadScene(nextSceneLoad);

			if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
				PlayerPrefs.SetInt("levelAt", nextSceneLoad);
		}
	}
	private void CheckLeveIfLose()
	{
		if (SceneManager.GetActiveScene().buildIndex == 4)
			SceneManager.LoadScene("MainMenu");
		else
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
