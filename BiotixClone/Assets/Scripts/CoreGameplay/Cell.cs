using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerEnterHandler/*, IPointerClickHandler*/, IPointerUpHandler
{
	private int currentPoints = 0;

	private Image playerCell;
	private TextMeshProUGUI count;
	private bool isCoroutineStarted = false;

	[Tooltip("Задает максимальное количество очков базы")]
	[SerializeField] int maxPoints = 20;
	[Tooltip("Задает задержку между добавлением очков")]
	[SerializeField] float gainDelay = .4f;
	[Tooltip("Задает владельца клетки")]
	[SerializeField] SetTeam team;

	public int Points
	{
		get => currentPoints;
		set
		{
			currentPoints = value;
		}
	}
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
	/*	public void ChangeCount()
		{
			count.SetText($"{currentPoints}");
		}*/

	public void AddToBranch(int Points, SetTeam team)
	{
		var x = currentPoints;
		if (Team == team)
			Points += currentPoints;
		else
		{
			Points -= currentPoints;
			if (Points < 0)
			{
				Team = team;
				Points = currentPoints * -1;
			}
			if (Points == 0)
			{
				team = null;
			}
		}
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
		PointsController.Instance.cell = this;
		if (PointsController.Instance.IsDrag)
		{
			if (PointsController.Instance.PlayerTeam == team)
			{
				if (PointsController.Instance.AddCell(this))
				{
					return;
				}
				else if (PointsController.Instance.selectedCells.Count > 1)
				{
					PointsController.Instance.CreatePath();
					PointsController.Instance.selectedCells.Clear();
				}
			}

			else
			{
				if (PointsController.Instance.PlayerTeam != team && PointsController.Instance.selectedCells.Count < 1)
				{
					return;
				}
				else
				{
					PointsController.Instance.CreatePath();
					PointsController.Instance.selectedCells.Clear();
					PointsController.Instance.cell = this;
				}
			}
		}
	}

	/*public void OnPointerClick(PointerEventData eventData)
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
	}*/
	public void OnPointerUp(PointerEventData eventData)
	{
		if (PointsController.Instance.selectedCells.Count > 0)
		{
			PointsController.Instance.CreatePath();
			PointsController.Instance.selectedCells.Clear();
			PointsController.Instance.cell = this;
		}
	}
	#endregion


#if UNITY_EDITOR
	/*	public void OnMouseUp()
		{
			if (PointsController.Instance.selectedCells.Count > 0)
			{
				if (PointsController.Instance.PlayerTeam == team)
				{
					PointsController.Instance.selectedCells.Clear();
					print("Send to ours");
				}
				else
				{
					PointsController.Instance.selectedCells.Clear();
					print("Send points");
				}
			}
		}*/
#endif
}