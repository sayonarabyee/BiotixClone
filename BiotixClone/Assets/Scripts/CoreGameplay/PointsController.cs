using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointsController : Singleton<PointsController>, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Tooltip("Задает цвет команды игрока")]
    [SerializeField] SetTeam playerTeam;
    [HideInInspector] public bool isDrag = false;
    [SerializeField] Path pathPrefab;
    public Cell cell;
    public Transform mainUI;

    public List<Cell> selectedCells = new List<Cell>();
    public Transform pointer;
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
        if (cell == true)
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
        selectedCells.Clear();
    } 
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        pointer.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        CreatePath();
    }
}


