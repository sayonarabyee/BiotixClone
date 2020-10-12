﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Path : MonoBehaviour
{
	private int points;
	private Transform createBranchFrom;
	private Cell createBranchTo;
	private SetTeam team;
	private float timeToMove;

	[SerializeField] Transform cellPrefab;
	[SerializeField] float speed = 1f;

	public Transform CreateBranchFrom { get => createBranchFrom; set => createBranchFrom = value; }
	public Cell CreateBranchTo { get => createBranchTo; set => createBranchTo = value; }
	public int Points { get => points; set => points = value; }

	private void Start()
	{
		var from = FromScreenToWorld(createBranchFrom.transform.position);
		var to = FromScreenToWorld(createBranchTo.transform.position);
		timeToMove = Vector2.Distance(from, to) / speed;
		cellPrefab.position = Vector2.Lerp(createBranchFrom.transform.position, createBranchTo.transform.position, timeToMove / 2);
		StartCoroutine(Send());
	}
	private Vector3 FromScreenToWorld(Vector3 position)
	{
		var pos = Camera.main.ScreenToWorldPoint(position);
		pos.z = 0f;
		return position;
	}
	public void Create(Transform from, SetTeam team, Cell to, int points)
	{
		this.createBranchFrom = from;
		this.team = team;
		this.createBranchTo = to;
		this.points = points;
	}
	IEnumerator Send()
	{
		yield return new WaitForSeconds(timeToMove);
		
		CreateBranchTo.AddToBranch(Points, team);
		Destroy(gameObject, timeToMove / 2);
	}
}
