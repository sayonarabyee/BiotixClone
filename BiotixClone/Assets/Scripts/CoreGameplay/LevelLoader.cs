using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : Singleton<LevelLoader>
{
	[SerializeField] List<GameObject> levelPrefabs;
	private int currentOpenedLevel = 0;
	public GameObject selectLevel;
	private void Start()
	{
		PlayerPrefs.SetInt("currentOpenedLevel", currentOpenedLevel);
		selectLevel = Instantiate(levelPrefabs[currentOpenedLevel]);
	}
	public void LoadNextLevel()
	{
		// Удалить предыдущий префаб, добавить единицу к плеерпрефс, загрузить следующий левел.
		Debug.Log(currentOpenedLevel);
		Destroy(selectLevel);
		currentOpenedLevel++;
		PlayerPrefs.GetInt("currentOpenedLevel");
		Debug.Log(currentOpenedLevel);
		selectLevel = Instantiate(levelPrefabs[currentOpenedLevel]);
	}
	public void LoadSelectedLevel()
	{

	}
}
