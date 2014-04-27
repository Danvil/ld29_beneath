using UnityEngine;
using System.Collections;

public class SubmarineMove : MonoBehaviour {

	public float BackwardsPenalty = 0.35f;
	public float MaxVelocityX = 1.65f;
	public float MaxVelocityY = 0.95f;
	public float ForceX = 300.00f;
	public float ForceY = 300.00f;

	public float heightMax = 5.2f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
//		float py = targetY
//				+ 0.07f*Mathf.Sin(Time.time/3.74f*2*Mathf.PI)
//				+ 0.03f*Time.deltaTime*(-1.0f+2.0f*Random.value);
	}

	void FixedUpdate() {
		float inpx = Input.GetAxis("Horizontal");
		float inpy = Input.GetAxis("Vertical");
		// penalty for backwards
		if(inpx < 0) inpx *= BackwardsPenalty;
		// desired velocity
		float vx = inpx * MaxVelocityX;
		float vy = inpy * MaxVelocityY;
		// actual velocity
		float avx = this.rigidbody.velocity.x;
		float avy = this.rigidbody.velocity.y;
		// force
		float fx = ForceX*(vx - avx);
		if(Mathf.Abs(fx) > ForceX) {
			fx = Mathf.Sign(fx)*ForceX;
		}
		float fy = ForceY*(vy - avy);
		if(Mathf.Abs(fy) > ForceY) {
			fy = Mathf.Sign(fy)*ForceY;
		}
		this.rigidbody.AddForce(new Vector3(fx,fy,0));
		// max height
		if(this.transform.position.y > heightMax) {
			this.transform.position = new Vector3(
				this.transform.position.x,
				heightMax,
				this.transform.position.z);
		}
	}
}
