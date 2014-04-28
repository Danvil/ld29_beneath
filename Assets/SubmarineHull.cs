using UnityEngine;
using System.Collections;

public class SubmarineHull : MonoBehaviour {

	public float damageScale = 1.7f;

	public GameObject damageFx;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c) {
		// compute damage
		float v = c.relativeVelocity.magnitude;
		float dmg = damageScale*v*v;
		SubmarineState.Singleton.DamageHull(dmg);
		Debug.Log(c.relativeVelocity);
		// create Fx
		if(dmg > 1.0) {
			GameObject go = (GameObject)Instantiate(damageFx);
			go.transform.position = c.contacts[0].point;
			Destroy(go, go.GetComponent<ParticleSystem>().duration);
		}
	}
}
