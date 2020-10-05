using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointsController : Singleton<PointsController>, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler
{
    [Tooltip("Задает цвет команды игрока")]
    [SerializeField] SetTeam playerTeam;

    public List<BaseCell> selectedCells = new List<BaseCell>();
    public Transform Pointer;
    [HideInInspector]
    public bool isDrag;
    public SetTeam PlayerTeam { get => playerTeam; set => playerTeam = value; }
    public bool IsDrag { get => isDrag; set => isDrag = value; }

    public bool AddCell(BaseCell cell)
    {
        if (!selectedCells.Contains(cell))
        {
            selectedCells.Add(cell);
            return true;
        }
        return false;
    }
    public int SendPoints(List<BaseCell> list)
    {
        int sum = default;
        foreach (var point in list)
        {
           // Сложить бактерии
        }
        return sum;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        Pointer.position = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {   
    }
    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {   
    }
}
