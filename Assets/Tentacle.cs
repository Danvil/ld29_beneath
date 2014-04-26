using UnityEngine;
using System.Collections;

public class Tentacle : MonoBehaviour {

	public GameObject[] links;

	public AI.TentacleIntelligence ai;

	public GameObject Base {
		get { return links[0]; }
	}
	
	public GameObject Endeffector {
		get { return links[links.Length - 1]; }
	}

	public float LengthMax {
		get { return 13.75f; }
	}

	// Use this for initialization
	void Start () {
		var followai = new AI.TentacleFollow();
		followai.Tentacle = this;
		followai.Target = Endeffector.transform;
		ai = followai;
	}
	
	// Update is called once per frame
	void Update () {
		ApplyPose(ai.Pose(Time.time));
	}

	void ApplyPose(AI.TentacleEndeffector pose) {
		Endeffector.transform.position = pose.position;
		Endeffector.transform.rotation = Quaternion.FromToRotation(new Vector3(0,1,0), -pose.normal);
	}

	void OnDrawGizmos() {
		if(ai == null) return;
		Gizmos.color = Color.red;
		var pose = ai.Pose(Time.time);
		Gizmos.DrawLine(pose.position, pose.position + pose.normal);
	}
}
