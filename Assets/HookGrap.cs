using UnityEngine;
using System.Collections;

public class HookGrap : MonoBehaviour {

	public float deadTime = 1.0f;

	float currentDeadTime = 0.0f;
	
	FixedJoint joint;

	public GameObject ConnectedBody { get; private set; }

	public bool HasConnectedBody { get { return this.ConnectedBody; } }

	Light connectLight;

	public void Unlink() {
		Destroy(this.joint);
		this.ConnectedBody = null;
	}

	// Use this for initialization
	void Start () {
		connectLight = this.transform.Find("ConnectLight").GetComponent<Light>();
	}
	
	void SetColor() {
		Color color = HasConnectedBody ? Color.red : Color.green;
		this.connectLight.color = color;
//		this.renderer.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		this.currentDeadTime -= Time.deltaTime;
		if(this.currentDeadTime < 0 && this.HasConnectedBody && Input.GetButton("Fire2")) {
			this.currentDeadTime = deadTime;
			Unlink();
		}
		SetColor();
	}

	void OnTriggerStay(Collider other) {
//		Debug.Log(other);
//		Debug.Log(other.gameObject.layer);
//		Debug.Log(this.gameObject.layer);
		if(other.gameObject.layer == this.gameObject.layer) {
			if(this.currentDeadTime < 0 &&!this.HasConnectedBody && Input.GetButton("Fire2")) {
				this.currentDeadTime = deadTime;
				this.ConnectedBody = other.gameObject;
				this.joint = this.ConnectedBody.AddComponent<FixedJoint>();
				this.joint.connectedBody = this.rigidbody;
			}
		}
	}
}
