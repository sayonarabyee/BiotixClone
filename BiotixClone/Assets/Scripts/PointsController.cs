using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointsController : Singleton<PointsController>, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler
{
    public List<BaseCell> selectedCells = new List<BaseCell>();
    public void AddCell(BaseCell cell)
    {
        if (!selectedCells.Contains(cell))
        {
            selectedCells.Add(cell);
            foreach (var item in selectedCells)
            {
                print("Add to list");
                print(item.ToString());
                print(selectedCells);
            }
        }
    }

    public int SendPoints(List<BaseCell> list, params int [] points)
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
        Debug.Log(eventData.position);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
    }
}
