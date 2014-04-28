using UnityEngine;
using System.Collections;

[RequireComponent (typeof (TubeRenderer))]
public class Hook : MonoBehaviour {

	public float winchSpeed = 10.0f;
	public float ropeLength = 4.0f;
	public float ropeRadius = 0.03f;

	public GameObject end;

	public HookGrap Grapple { get; private set; }

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
		Grapple = end.GetComponent<HookGrap>();
	}

	public bool IsWinchUp {
		get {
			return spring.maxDistance <= 0.0f;
		}
	}

	public void WinchUpDown(float a) {
		float target = Mathf.Min(Mathf.Max(spring.maxDistance + winchSpeed*a, 0.0f), ropeLength);
		this.audio.volume = (target != spring.maxDistance ? 0.7f : 0.0f);
		spring.maxDistance = target;
	}

	public void Grap() {
		Collider[] colliders = Physics.OverlapSphere(this.end.transform.position, 0.1f);
		if(colliders.Length > 0) {
			Debug.Log(colliders);
		}
	}

	// Update is called once per frame
	void Update () {
		float w = 
			- 1.0f*Input.GetAxis("Mouse ScrollWheel")
			+ 0.041f*((Input.GetButton("Fire1") ? 1 : 0) - (Input.GetButton("Fire3") ? 1 : 0));
		WinchUpDown(w);
		rope[SEGMENTS-1] = this.end.transform.localPosition;
		tube.SetPoints(rope, ropeRadius, Color.white);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.end.transform.position, 0.1f);
	}
}
