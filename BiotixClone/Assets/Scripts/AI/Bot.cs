using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bot : Singleton<Bot>
{
	[Header("Задает команду бота")]
	[SerializeField] SetTeam botTeam;
	[SerializeField] Path pathPrefab;
	[SerializeField] Transform mainUI;
	[Header("Задает задержку перед первым ходом")]
	[SerializeField] float botStartDelay = 0f;
	[Header("Задает периодичность отправки очков")]
	[SerializeField] float botPointsSendDelay = 0f;

	private List<Cell> cells = new List<Cell>();
	private List<Cell> botCells = new List<Cell>();
	private List<Cell> otherCells = new List<Cell>();

	private void Start()
	{
		InvokeRepeating("CheckAndSend", botStartDelay, botPointsSendDelay);
		cells = FindObjectsOfType<Cell>().ToList();
		botCells = cells.Where(c => c.Team == botTeam).ToList();
		otherCells = cells.Where(c => c.Team != botTeam).ToList();
	}
	private void CheckAndSend()
	{
		Cell fromCell = botCells[Random.Range(0, botCells.Count)];
		Cell targetCell = otherCells[Random.Range(0, otherCells.Count)];

		var x = Instantiate(pathPrefab, mainUI);
		var points = fromCell.Points / 2;
		fromCell.Points -= points;

		x.Create(fromCell.transform, botTeam, targetCell, points);

	}
	public void OnCellChangeTeam()
	{
		botCells = cells.Where(c => c.Team == botTeam).ToList();
		otherCells = cells.Where(c => c.Team != botTeam).ToList();
	}
}
