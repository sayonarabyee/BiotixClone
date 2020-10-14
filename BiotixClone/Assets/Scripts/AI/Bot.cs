using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bot : Singleton<Bot>
{
	[SerializeField] SetTeam botTeam;

	private List<Cell> cells = new List<Cell>();
	private List<Cell> botCells = new List<Cell>();
	private List<Cell> otherCells = new List<Cell>();

	private void Start()
	{
		InvokeRepeating("CheckAndSend", 5f, 7f);
		cells = FindObjectsOfType<Cell>().ToList();
		botCells = cells.Where(c => c.Team == botTeam).ToList();
		otherCells = cells.Where(c => c.Team != botTeam).ToList();
		CheckAndSend();
	}
	private void CheckAndSend()
	{
		Cell fromCell = botCells[Random.Range(0, botCells.Count)];
		Cell targetCell = otherCells[Random.Range(0, otherCells.Count)];
		

		
	}
	public void OnCellChangeTeam()
	{

	}
}
