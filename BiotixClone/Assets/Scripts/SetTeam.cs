using UnityEngine;

public class SetTeam : MonoBehaviour
{
	[Tooltip("Задает цвет команды игрока")]
	[SerializeField] Color teamColor;
	public Color TeamColor { get => teamColor; set => teamColor = value; }
}
