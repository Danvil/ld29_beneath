﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (TubeRenderer))]
public class Hook : MonoBehaviour {

	public float winchSpeed = 5.0f;
	public float ropeLength = 3.0f;
	public float ropeRadius = 0.03f;

	public GameObject end;

	const int SEGMENTS = 2;
	private Vector3[] rope = new Vector3[SEGMENTS];
	TubeRenderer tube;

	SpringJoint spring;

	void Awake() {
		tube = GetComponent<TubeRenderer>();
		spring = GetComponent<SpringJoint>();
	}

	// Use this for initialization
	void Start () {
	}

	public void WinchUpDown(float a) {
		spring.maxDistance = Mathf.Min(Mathf.Max(spring.maxDistance + winchSpeed*a, 0.0f), ropeLength);
	}

	public void Grap() {
		Collider[] colliders = Physics.OverlapSphere(this.end.transform.position, 0.1f);
		if(colliders.Length > 0) {
			Debug.Log(colliders);
		}
	}
	
	// Update is called once per frame
	void Update () {
		WinchUpDown(Input.GetAxis("Mouse ScrollWheel"));
		rope[SEGMENTS-1] = this.end.transform.localPosition;
		tube.SetPoints(rope, ropeRadius, Color.white);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.end.transform.position, 0.1f);
	}
}