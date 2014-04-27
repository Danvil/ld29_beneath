using UnityEngine;
using System.Collections;

public class SubmarineMove : MonoBehaviour {

	public float BackwardsPenalty = 0.35f;
	public float AccelerationXMax = 1.85f;
	public float AccelerationYMax = 1.85f;
	public float VelocityXMaxBackwards = 0.65f;
	public float VelocityXMax = 0.95f;
	public float VelocityYMax = 0.45f;

	public float Force = 1000.0f;
	public float VelocityMax = 2.0f;

	public float heightMax = 5.2f;

	float targetY;

	public float VelocityX { get; private set; }

	// Use this for initialization
	void Start () {
		targetY = this.transform.position.y;
	}

	Vector3 force;
	
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
		//this.transform.position = new Vector3(px,py,0);
		force = new Vector3(
			AccelerationXMax*inpx,
			AccelerationYMax*inpy + (py-targetY),
			0.0f
		);
	}

	void FixedUpdate() {
		this.rigidbody.AddForce(Force*force);
		if(rigidbody.velocity.magnitude > VelocityMax)
		{
			rigidbody.velocity = rigidbody.velocity.normalized * VelocityMax;
		}
		if(rigidbody.position.y > heightMax) {
			rigidbody.transform.position = new Vector3(rigidbody.transform.position.x, heightMax, rigidbody.transform.position.z);
		}
	}
}
