using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public int currentPoints { get; set; } = 0 ;

	private TextMeshProUGUI count;
	private bool isCoroutineStarted = false;

	[Tooltip("Задает максимальное количество очков базы")]
	[SerializeField] int maxPoints = 20;
	[Tooltip("Задает задержку между добавлением очков")]
	[SerializeField] float gainDelay = .4f;

	// Добавить спрайты для захваченной другим игроком/свободной ячейки (SetColor())
	private void Start()
	{
		count = GetComponentInChildren<TextMeshProUGUI>();
	}

	// Curoutine + LateUpdate
	#region PointsGaner
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
    #endregion 

    public void OnPointerEnter(PointerEventData eventData)
	{
		// Select counter++
		// Если владелец отличается от предыдущего выбранного объекта - Send();
		// Создать сущность Owner, инициализировать её при старте игровой сцены
		// При смене владельца менять Owner
		Debug.Log(eventData.position);
		PointsController.Instance.AddCell(this);
		currentPoints -= 10;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
		// Select counter++
		// Если владелец отличается от предыдущего выбранного объекта - Send();
		// Создать сущность Owner, инициализировать её при старте игровой сцены
		// При смене владельца менять Owner
	}

	public void OnDrag(PointerEventData eventData)
	{

	}
    public void OnBeginDrag(PointerEventData eventData)
    {
		
	}
    public void OnEndDrag(PointerEventData eventData)
    {

	}
}