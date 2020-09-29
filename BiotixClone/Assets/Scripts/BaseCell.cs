using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BaseCell : MonoBehaviour
{
	public static int currentPoints = 0;

	private TextMeshProUGUI count;
	private bool isCoroutineStarted = false;

	[Tooltip("Задает максимальное количество очков базы")] 
	[SerializeField] int maxPoints = 20;
	[Tooltip("Задает задержку между добавлением очков")]
	[SerializeField] float gainDelay = .4f;

	// Добавить спрайты для захваченной другим игроком/свободной ячейки (SetColor())

	private void Start()
	{
		currentPoints = maxPoints;
		count = GetComponentInChildren<TextMeshProUGUI>();
	}
	private void Check()
	{
		if (currentPoints < 0)
			currentPoints = 0;

		if (!isCoroutineStarted && (currentPoints < maxPoints))
		{
			isCoroutineStarted = false;
			StartCoroutine(PointsGain());
			isCoroutineStarted = true;
		}
	}
	private void LateUpdate()
	{
		Check();
		count.SetText($"{currentPoints}");
	}
	IEnumerator PointsGain()
	{
		isCoroutineStarted = true;
		while (currentPoints < maxPoints)
		{
			currentPoints += 1;
			yield return new WaitForSeconds(gainDelay);
			if (currentPoints == maxPoints)
			{
				isCoroutineStarted = false;
				StopCoroutine(PointsGain());
			}
		}
	}
}