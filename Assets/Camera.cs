using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public SubmarineMove target;
	public float lead = 1.0f;
	public float lerpMult = 1.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dst = this.transform.position;
		dst.x = this.target.transform.position.x + lead;
		this.transform.position = Vector3.Lerp(this.transform.position, dst, lerpMult*Time.deltaTime);
	}
}
