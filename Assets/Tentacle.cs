using UnityEngine;
using System.Collections;

public class Tentacle : MonoBehaviour {

	public GameObject[] links;

	public float positionForce = 100.0f;

	public AI.TentacleIntelligence ai;

	public GameObject Base {
		get { return links[0]; }
	}
	
	public GameObject Endeffector {
		get { return links[links.Length - 1]; }
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
	}

	AI.TentacleEndeffector lastPose;

	// Update is called once per frame
	void Update () {
		lastPose = ai.Pose();
	}

	void FixedUpdate() {
		ApplyPose(lastPose);
	}

	void ApplyPose(AI.TentacleEndeffector pose) {
		Endeffector.rigidbody.AddForce(positionForce*(pose.position - Endeffector.transform.position));
		Endeffector.transform.rotation = Quaternion.FromToRotation(new Vector3(0,1,0), -pose.normal);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(Base.transform.position, LengthMax);
		if(ai == null) return;
		Gizmos.color = Color.red;
		Gizmos.DrawLine(lastPose.position, lastPose.position + lastPose.normal);
	}
}
