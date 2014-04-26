using UnityEngine;
using System.Collections;

public class TorpedoLauncher : MonoBehaviour {
	
	public GameObject pfMissile;
	public Vector3 shootStart;
	public Vector3 shootDirection;
	public float shootVelocity;
	public float deadTime;

	float currentDeadTime;
	
	// Use this for initialization
	void Start () {
		currentDeadTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		currentDeadTime -= Time.deltaTime;
		if(currentDeadTime < 0 && Input.GetButton("Fire1")) {
			currentDeadTime = deadTime;
			GameObject go = (GameObject)Instantiate(pfMissile);
			Torpedo h = go.GetComponent<Torpedo>();
			h.Fire(
				this.transform.TransformPoint(shootStart),
				this.transform.TransformDirection(shootDirection),
				shootVelocity);
		}
	}
	
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Vector3 a = this.transform.TransformPoint(shootStart);
		Vector3 b = a + 0.4f*this.transform.TransformDirection(shootDirection);
		Gizmos.DrawWireSphere(a, 0.1f);
		Gizmos.DrawLine(a, b);
	}
}
