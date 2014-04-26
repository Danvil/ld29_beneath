using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swarm : MonoBehaviour {

	public GameObject pfFish;
	public int count = 20;

	public float swimSpeed = 1.3f;
	public float separationFactor = 0.04f;
	public float randomFactor = 0.26f;
	public float swimSpeedDist = 0.3f;

	public AI.SwarmIntelligence ai;

	List<GameObject> fishy = new List<GameObject>();

	void GenerateSwarm() {
//		ai = new AI.SwarmCircle() {
//			Center = new Vector3(0,3,0),
//			Radius = 2.0f,
//			Velocity = 1.0f
//		};
		ai = new AI.SwarmRandom() {
			AreaCenter = new Vector3(0,3,0),
			AreaRadius = 7.0f,
			AreaHeight = 2.0f
		};
		ai.Swarm = this;
		for(int i=0; i<count; i++) {
			GameObject go = (GameObject)Instantiate(pfFish);
			fishy.Add(go);
			go.transform.position = Random.insideUnitSphere;
			go.transform.parent = this.transform;
		}
	}

	// Use this for initialization
	void Start () {
		GenerateSwarm();
	}

	Vector3 ComputeFishDirection(GameObject go) {
		return go.transform.TransformDirection(Vector3.right);
	}

	public float DistanceToNearest(Vector3 p) {
		float dbest = float.MaxValue;
		foreach(GameObject a in fishy) {
			float d = (a.transform.position - p).magnitude;
			dbest = Mathf.Min(dbest, d);
		}
		return dbest;
	}

	// Update is called once per frame
	void Update () {
		Vector3 target = ai.Target(Time.time);
		foreach(GameObject a in fishy) {
			Vector3 opos = a.transform.position;
			// move to goal
			Vector3 togoal = target - a.transform.position;
			float dist = togoal.magnitude;
			if(dist > 0) {
				float speed = swimSpeed * Mathf.Min(1.0f, dist/swimSpeedDist);
				a.transform.position += Time.deltaTime*speed*togoal/dist;
			}
			// separation
			Vector3 separation = Vector3.zero;
			foreach(GameObject b in fishy) {
				Vector3 d = b.transform.position - a.transform.position;
				float m = d.magnitude;
				float F = m == 0.0f ? 0.0f : 1.0f / (m*m);
				separation += F*d;
			}
			a.transform.position -= Time.deltaTime*separationFactor*separation;
			// randomness
			a.transform.position += Time.deltaTime*randomFactor*Random.insideUnitSphere;
			// align
			Vector3 dpos = a.transform.position - opos;
			if(dpos.magnitude > 0) {
				a.transform.rotation = Quaternion.FromToRotation(Vector3.right, dpos);
			}
		}
	}
}
