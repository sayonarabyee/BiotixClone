using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Cell))]
public class EndGameCheck : Singleton<EndGameCheck>
{
	private List<Cell> cells = new List<Cell>();
	[SerializeField] SetTeam playerTeam;
	[SerializeField] SetTeam botTeam;
	
	private void Start()
	{
		if (Advertisement.isSupported)
			Advertisement.Initialize("3864625", false);

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

	private void Win() => LevelLoader.Instance.LoadNextLevel();
	private void Lose() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	private void ShowADSWin()
	{
		if (Advertisement.IsReady())
		{
			var options = new ShowOptions { resultCallback = ShowResultWin };
			Advertisement.Show(options);
		}
	}
	private void ShowADSLose()
	{
		if (Advertisement.IsReady())
		{
			var options = new ShowOptions { resultCallback = ShowResultLose };
			Advertisement.Show(options);
		}
	}
	private void ShowResultWin(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				Win();
				break;
			case ShowResult.Skipped:
				Win();
				break;
		}
	}
	private void ShowResultLose(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				Lose();
				break;
			case ShowResult.Skipped:
				Lose();
				break;
		}
	}
}
