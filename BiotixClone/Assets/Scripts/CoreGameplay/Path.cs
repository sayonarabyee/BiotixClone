using System.Collections;
using UnityEngine;

public class Path : MonoBehaviour
{
	private int points;
	[SerializeField] Transform createBranchFrom;
	[SerializeField] Cell createBranchTo;
	private SetTeam team;
	private float timeToMove;

	[SerializeField] Transform particlePrefab;
	[Header("Задает скорость всех частиц в игре")]
	[SerializeField] float speed = 1f;

	public Transform CreateBranchFrom { get => createBranchFrom; set => createBranchFrom = value; }
	public Cell CreateBranchTo { get => createBranchTo; set => createBranchTo = value; }
	public int Points { get => points; set => points = value; }
	private void Start()
	{
		var from = FromScreenToWorld(createBranchFrom.transform.position);
		var to = FromScreenToWorld(createBranchTo.transform.position);
		timeToMove = Vector2.Distance(from, to) / speed;
		StartCoroutine(Send(timeToMove, from, to));
	}
	private Vector3 FromScreenToWorld(Vector3 position)
	{
		var pos = Camera.main.ScreenToWorldPoint(position);
		pos.z = 0f;
		return pos;
	}
	public void Create(Transform from, SetTeam team, Cell to, int points)
	{
		this.createBranchFrom = from;
		this.team = team;
		this.createBranchTo = to;
		this.points = points;
	}
	private void SetParticle()
	{
		var particle = particlePrefab.GetComponentInChildren<ParticleSystem>();
		particle.maxParticles = points;
		particle.startColor = team.TeamColor;
	}
	IEnumerator Send(float time, Vector2 from, Vector2 to)
	{
		SetParticle();
		for (float i = 0; i < time; i += Time.deltaTime)
		{
			particlePrefab.position = Vector2.Lerp(from, to, i / time);
			yield return null;
		}
		CreateBranchTo.AddToBranch(Points, team);
		Destroy(gameObject);
	}
}
