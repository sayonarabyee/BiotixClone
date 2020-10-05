using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseCell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
	public int currentPoints { get; set; } = 0;

	private Image playerCell;
	private TextMeshProUGUI count;
	private bool isCoroutineStarted = false;

	[Tooltip("Задает максимальное количество очков базы")]
	[SerializeField] int maxPoints = 20;
	[Tooltip("Задает задержку между добавлением очков")]
	[SerializeField] float gainDelay = .4f;
	[Tooltip("Задает владельца клетки")]
	[SerializeField] SetTeam team;
	public SetTeam Team
	{
		get => team;
		set
		{
			team = value;

			if (team == null)
				playerCell.color = Color.white;
			else
				playerCell.color = team.TeamColor;
		}
	}
	private void Start()
	{
		playerCell = GetComponent<Image>();
		count = GetComponentInChildren<TextMeshProUGUI>();

		if (team == null)
			playerCell.color = Color.white;
		else
			playerCell.color = team.TeamColor;

	}

	#region PointsGaner
	private void Check()
	{
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

	#region InputSystem
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (PointsController.Instance.PlayerTeam == team)
		{
			if (PointsController.Instance.IsDrag)
			{
				PointsController.Instance.AddCell(this);
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (PointsController.Instance.PlayerTeam == team)
		{
			if (PointsController.Instance.AddCell(this))
			{
				print("Added to list");
			}
			else if (PointsController.Instance.selectedCells.Count > 1)
			{
				print("Send to ours");
				PointsController.Instance.selectedCells.Clear();
			}
		}
		else if (PointsController.Instance.PlayerTeam != team && PointsController.Instance.selectedCells.Count < 1)
		{
			print("nothing happens");
			return;
		}
		else
		{
			PointsController.Instance.selectedCells.Clear();
			print("Send points");
		}
	}
	#endregion
}