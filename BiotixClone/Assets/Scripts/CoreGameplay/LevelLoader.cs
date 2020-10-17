using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : Singleton<LevelLoader>
{
	[SerializeField] List<GameObject> levelPrefabs;
	private int currentOpenedLevel = 0;

	private void Start()
	{
		PlayerPrefs.SetInt("currentOpenedLevel", currentOpenedLevel);
		var clone = Instantiate(levelPrefabs[currentOpenedLevel]);
	}
	public void LoadNextLevel()
	{
		// Удалить предыдущий префаб, добавить единицу к плеерпрефс, загрузить следующий левел.
		//PlayerPrefs.GetInt("currentOpenedLevel");

		
		PlayerPrefs.SetInt("currentOpenedLevel", currentOpenedLevel++);
		PlayerPrefs.GetInt("currentOpenedLevel");
		Instantiate(levelPrefabs[currentOpenedLevel]);
	}
	public void LoadSelectedLevel()
	{

	}
}
