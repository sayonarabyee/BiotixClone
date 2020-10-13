using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isWin;
    private void Start()
    {
        // Создать лист со всеми элементами и проверять тэг
        // Если тэг совпадает у всех элементов - заканчивать уровень
        InvokeRepeating("CheckWin", 0f, 1.5f);
    }
    public void OnCellChanged(Cell[] cells)
	{

	}
    private void Lose()
    {
	
    }
    private void Win()
	{

	}
}
