using UnityEngine;
using System.Collections;

[RequireComponent (typeof (TubeRenderer))]
public class Hook : MonoBehaviour {

	public float winchSpeed = 0.5f;
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

	public void WinchUp() {
		spring.maxDistance = Mathf.Max(spring.maxDistance - winchSpeed*Time.deltaTime, 0.0f);
	}

	public void WinchDown() {
		spring.maxDistance = Mathf.Min(spring.maxDistance + winchSpeed*Time.deltaTime, ropeLength);
	}

	Vector3 Begin {
		get { return Vector3.zero; }
	}
	
	Vector3 End {
		get { return this.end.transform.localPosition; }
	}

	void MoveEnd(Vector3 d) {
		this.end.transform.localPosition += d;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.R)) {
			WinchUp();
		}
		if(Input.GetKey(KeyCode.F)) {
			WinchDown();
		}
		rope[SEGMENTS-1] = End;
		tube.SetPoints(rope, ropeRadius, Color.white);
	}
}
