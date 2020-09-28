using System.Collections;
using TMPro;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public static int currentPoints { get; set; } = 0;
    private bool isCoroutineStarted = false;
    private bool isFull = false;
    private TextMeshProUGUI count;

    [SerializeField] int maxPoints = 40;

    private void Start()
    {
        count = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(PointsGain());
        isCoroutineStarted = true;
    }
    private void Update()
    {
        if (isFull)
        {
            StopCoroutine(PointsGain());
            isCoroutineStarted = false;
            Debug.Log(isCoroutineStarted);
            Debug.Log("perviy if");
        }
        else if (!isCoroutineStarted)
        {
            StartCoroutine(PointsGain());
            isCoroutineStarted = true;
            Debug.Log(isCoroutineStarted);
            Debug.Log("vtoroi if");
        }

        count.SetText($"{currentPoints}");
    }
    IEnumerator PointsGain()
    {
        isCoroutineStarted = true;
        for (int i = 0; i < maxPoints; i++)
        {
            currentPoints += 1;
            yield return new WaitForSeconds(.7f);
            if (currentPoints == maxPoints)
            {
                isFull = true;
            }
        }
    }
}
