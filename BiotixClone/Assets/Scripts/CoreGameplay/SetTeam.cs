using UnityEngine;

public class SetTeam : MonoBehaviour
{
    [Header("Задает цвет команды игрока/бота")]
	[SerializeField] Color teamColor;
	public Color TeamColor { get => teamColor; set => teamColor = value; }
}
