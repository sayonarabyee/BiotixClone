using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointsController : Singleton<PointsController>, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler
{
    [Tooltip("Задает цвет команды игрока")]
    [SerializeField] SetTeam playerTeam;
    [HideInInspector] public bool isDrag;
    [SerializeField] Path pathPrefab;
    public Cell cell;
    public Transform mainUI;

    public List<Cell> selectedCells = new List<Cell>();
    public Transform Pointer;
    public SetTeam PlayerTeam { get => playerTeam; set => playerTeam = value; }
    public bool IsDrag { get => isDrag; set => isDrag = value; }

    public bool AddCell(Cell cell)
    {
        if (!selectedCells.Contains(cell))
        {
            selectedCells.Add(cell);
            return true;
        }
        return false;
    }
    public void CreatePath()
	{
        foreach (var item in selectedCells)
        {
            if (item == cell) 
                continue;

            var x = Instantiate(pathPrefab, mainUI);
            var value = item.Points / 2;
            item.Points -= value;

            x.Create(item.transform, PlayerTeam, cell, value);
        }
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
        CompletePath();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {   
        
    }
    public void CompletePath()
    {
        if (cell)
        {
            CreatePath();
        }
        selectedCells.Clear();
    }
}
