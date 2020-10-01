using System.Collections.Generic;
using UnityEngine;


public class PointsController : Singleton<PointsController>
{
	public List<BaseCell> selectedCells = new List<BaseCell>();
	public void AddPoints(BaseCell cell)
	{
		selectedCells.Add(cell);
		foreach (var item in selectedCells)
		{
			print(item);
			print(selectedCells);
		}

	}

	private void SendPoints()
	{

	}
}
