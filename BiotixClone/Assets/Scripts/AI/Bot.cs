using UnityEngine;
using UnityEngine.UI;

public class Bot : MonoBehaviour
{
	[SerializeField] Color botTeam;

	private void Start()
	{
		InvokeRepeating("CheckAndSend", 5f, 7f);
	}
	private void CheckAndSend()
	{
		//ОТправлять очки в пустые и в ячейки игрока
	}
}
