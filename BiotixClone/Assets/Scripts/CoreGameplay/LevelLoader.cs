using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : Singleton<LevelLoader>
{
	[SerializeField] List<GameObject> levelPrefabs;
	private int currentOpenedLevel = 0;
	private GameObject selectLevel;
	private void Start()
	{
		currentOpenedLevel = PlayerPrefs.GetInt("currentOpenedLevel",0);
		selectLevel = Instantiate(levelPrefabs[currentOpenedLevel]);
	}
	public void LoadNextLevel()
	{
		currentOpenedLevel++;
		PlayerPrefs.SetInt("currentOpenedLevel", currentOpenedLevel);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
