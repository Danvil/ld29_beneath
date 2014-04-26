using UnityEngine;
using System.Collections;

public class SubmarineMove : MonoBehaviour {

	const float BackwardsPenalty = 0.35f;
	const float AccelerationXMax = 1.85f;
	const float VelocityXMaxBackwards = 0.65f;
	const float VelocityXMax = 0.95f;
	const float VelocityYMax = 0.45f;

	float targetY;

	public float VelocityX { get; private set; }

	// Use this for initialization
	void Start () {
		targetY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		float inpx = Input.GetAxis("Horizontal");
		float inpy = Input.GetAxis("Vertical");
		// penalty for backwards
		if(inpx < 0) inpx *= BackwardsPenalty;
		// left/right with velocity
		float dvx = AccelerationXMax*inpx*Time.deltaTime;
		VelocityX = Mathf.Min(Mathf.Max(-VelocityXMaxBackwards, VelocityX + dvx),  VelocityXMax);
		float px = this.transform.position.x + Time.deltaTime*VelocityX;
		// up/down without velocity
		float dy = VelocityYMax*inpy*Time.deltaTime;
		targetY += dy;
		float py = targetY
				+ 0.07f*Mathf.Sin(Time.time/3.74f*2*Mathf.PI)
				+ 0.03f*Time.deltaTime*(-1.0f+2.0f*Random.value);
		this.transform.position = new Vector3(px,py,0);
	}
}
