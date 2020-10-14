using UnityEngine;
[RequireComponent(typeof(Animator))]
public class CircleAnimation : MonoBehaviour
{
	[SerializeField] Animator circle;

	private void Start()
	{
		circle = GetComponent<Animator>();
		circle.speed = Random.Range(.4f, .7f);
	}
}
