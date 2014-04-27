using UnityEngine;
using System.Collections.Generic;

public class TentacleGrapple : MonoBehaviour {

	FixedJoint joint;
	
	public GameObject ConnectedBody { get; private set; }
	
	public bool HasConnectedBody { get { return this.ConnectedBody; } }

	public GameObject TargetBody { get; set; }

	public bool MakeConnection { get; set; }

	// Use this for initialization
	void Start () {
		MakeConnection = false;
	}
	
	// Update is called once per frame
	void Update () {
		if((!MakeConnection && this.HasConnectedBody) || (TargetBody != null && ConnectedBody != TargetBody)) {
			Destroy(this.joint);
			this.ConnectedBody = null;
		}
	}
	
	void OnTriggerStay(Collider other) {
		if(other.gameObject.layer == this.gameObject.layer) {
			if(MakeConnection && !this.HasConnectedBody) {
				if(TargetBody == null || TargetBody == other.gameObject) {
					this.ConnectedBody = other.gameObject;
					this.joint = this.ConnectedBody.AddComponent<FixedJoint>();
					this.joint.connectedBody = this.rigidbody;
				}
			}
		}
	}

	void OnDrawGizmos() {
		if(TargetBody) {
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(TargetBody.transform.position, 0.3f);
		}
	}
}
