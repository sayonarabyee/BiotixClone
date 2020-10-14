using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndGameCheck : Singleton<EndGameCheck>
{
	private List<Cell> cells = new List<Cell>();
	[SerializeField] SetTeam playerTeam;
	[SerializeField] SetTeam botTeam;

	private void Start()
	{
		cells = FindObjectsOfType<Cell>().ToList();
	}
	public void Check()
	{
		var botCount = cells.Where(n => n.Team == botTeam).Count();
		var playerCount = cells.Where(n => n.Team == playerTeam).Count();

		if (botCount <= 0)
			print("WIN");

		else if (playerCount <= 0)
			print("LOSE");
	}

}
