using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(TextMeshProUGUI))]

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
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
	[SerializeField] LineRenderer lineRenderer;

	public UnityEvent onChangeTeam;

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
		currentPoints = maxPoints;
		playerCell = GetComponent<Image>();
		count = GetComponentInChildren<TextMeshProUGUI>();

		if (team == null)
			playerCell.color = Color.white;
		else
			playerCell.color = team.TeamColor;

		onChangeTeam.AddListener(EndGameCheck.Instance.Check);
		onChangeTeam.AddListener(Bot.Instance.OnCellChangeTeam);
	}

	public void AddToBranch(int Points, SetTeam team)
	{
		if (Team == team)
		{
			currentPoints += Points;
		}
		else
		{
			currentPoints -= Points;
			if (currentPoints < 0)
			{
				currentPoints *= -1;
				Team = team;
				onChangeTeam?.Invoke();
			}
			else if (currentPoints == 0)
			{
				team = null;
			}
		}
	}
	private Vector3 FromScreenToWorld(Vector3 position)
	{
		var pos = Camera.main.ScreenToWorldPoint(position);
		pos.z = 0f;
		return pos;
	}

	#region PointsGaner+Linerenderer
	private void Check()
	{
		if (!isCoroutineStarted && currentPoints != maxPoints && team != null)
		{
			StartCoroutine(PointsGain());
			isCoroutineStarted = true;
		}
	}
	private void LateUpdate()
	{
		if (PointsController.Instance.isDrag)
		{
			if (PointsController.Instance.selectedCells.Contains(this))
			{
				var from = FromScreenToWorld(transform.position);
				var to = FromScreenToWorld(PointsController.Instance.pointer.position);
				lineRenderer.SetPosition(0, from);
				lineRenderer.SetPosition(1, to);
				lineRenderer.enabled = true;
			}
			else
				lineRenderer.enabled = false;
		}
		Check();
		count.SetText($"{currentPoints}");
	}
	IEnumerator PointsGain()
	{
		if (currentPoints < maxPoints)
		{
			isCoroutineStarted = true;
			while (currentPoints < maxPoints)
			{
				currentPoints += 1;
				yield return new WaitForSeconds(gainDelay);
			}
		}
		if (currentPoints > maxPoints)
		{
			isCoroutineStarted = true;
			while (currentPoints > maxPoints)
			{
				currentPoints -= 1;
				yield return new WaitForSeconds(gainDelay);
			}
			if(currentPoints < maxPoints)
			{
				isCoroutineStarted = true;
				StartCoroutine(PointsGain());
			}
		}
		else
		{
			isCoroutineStarted = false;
			StopCoroutine(PointsGain());
		}
	}
	#endregion

	#region InputSystem
	public void OnPointerEnter(PointerEventData eventData)
	{
		PointsController.Instance.cell = this;
		if (PointsController.Instance.isDrag)
		{
			if (PointsController.Instance.PlayerTeam == team)
			{
				if (PointsController.Instance.AddCell(this))
				{

				}
			}
			else if (PointsController.Instance.PlayerTeam != team && PointsController.Instance.selectedCells.Count >= 1)
			{
				PointsController.Instance.cell = this;
			}
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (PointsController.Instance.cell == this)
		{
			PointsController.Instance.cell = null;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		PointsController.Instance.cell = this;
		if (PointsController.Instance.PlayerTeam == team)
		{
			if (PointsController.Instance.AddCell(this))
			{
				return;
			}
			else
			{
				PointsController.Instance.CreatePath();
			}
		}
		else if (PointsController.Instance.PlayerTeam != team && PointsController.Instance.selectedCells.Count >= 1)
		{
			PointsController.Instance.cell = this;
			PointsController.Instance.CreatePath();

		}
	}
	#endregion
}