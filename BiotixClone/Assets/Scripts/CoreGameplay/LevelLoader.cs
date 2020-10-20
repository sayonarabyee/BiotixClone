using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : Singleton<LevelLoader>
{
	[SerializeField] List<GameObject> levelPrefabs;
	[HideInInspector] public static int currentOpenedLevel;
	[SerializeField] GameObject selectLevel;
	private void Start()
	{
		Time.timeScale = 1f;
		LoadSelectedLevel(currentOpenedLevel);
	}
	public void LoadNextLevel()
	{
		currentOpenedLevel++;
		PlayerPrefs.SetInt("currentOpenedLevel", currentOpenedLevel);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void LoadSelectedLevel(int level)
	{
		if (level == null)
		{
			currentOpenedLevel = PlayerPrefs.GetInt("currentOpenedLevel", 0);
			selectLevel = Instantiate(levelPrefabs[currentOpenedLevel]);
		}
		else
		{
			selectLevel = Instantiate(levelPrefabs[level]);
		}
	}
}
