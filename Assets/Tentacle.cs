using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Tentacle : MonoBehaviour {

	public GameObject[] links;

	TentacleGrapple grapple;
	public TentacleGrapple Grapple { get { return grapple; } }

	public float positionForce = 100.0f;

	public AI.TentacleTouchy ai;

	public GameObject Base {
		get { return links[0]; }
	}
	
	public GameObject Endeffector {
		get { return links[links.Length - 1]; }
	}

	public IEnumerable<Collider> LinkColliders {
		get {
			return links.Select(o => o.collider);
		}
	}

	public Vector3 EndeffectorNormal {
		get { return Endeffector.transform.rotation * - new Vector3(0,1,0); }
	}

	public float LengthMax {
		get { return 2.74f; }
	}

	// Use this for initialization
	void Start () {
//		var followai = new AI.TentacleFollow(this);
//		followai.Target = Endeffector.transform;
//		ai = followai;
		ai = new AI.TentacleTouchy(this);
		grapple = Endeffector.GetComponent<TentacleGrapple>();
	}

	AI.TentacleEndeffector lastPose;

	// Update is called once per frame
	void Update () {
	}

	float CurrentForce {
		get {
			if(Grapple.HasConnectedBody) {
				float m = Mathf.Min(300.0f, Mathf.Max(10.0f, Grapple.ConnectedBody.rigidbody.mass));
				return 0.1f * m * this.positionForce;
			}
			else {
				return this.positionForce;
			}
		}
	}

	void FixedUpdate() {
		lastPose = ai.Pose();
		ApplyPose(lastPose);
	}

	void ApplyPose(AI.TentacleEndeffector pose) {
		Endeffector.rigidbody.AddForce(CurrentForce*(pose.position - Endeffector.transform.position).ZeroNormalize());
		Endeffector.transform.rotation = Quaternion.FromToRotation(new Vector3(0,1,0), -pose.normal);
	}

	void OnDrawGizmos() {
//		Gizmos.color = Color.red;
//		Gizmos.DrawWireSphere(Base.transform.position, LengthMax);
		if(ai == null) return;
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(lastPose.position, 0.3f);
		Gizmos.DrawLine(lastPose.position, lastPose.position + lastPose.normal);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(ai.Dbg1, 0.15f);
		Gizmos.DrawWireSphere(ai.Dbg2, 0.20f);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(Endeffector.transform.position, Endeffector.transform.position + ai.Dbg3);
	}
}
