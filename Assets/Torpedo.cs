using UnityEngine;
using System.Collections;

public class Torpedo : MonoBehaviour {
	
	Vector3 start;
	Vector3 direction;
	float velocity;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += Time.deltaTime*velocity*direction;
	}
	
	public void Fire(Vector3 start, Vector3 direction, float velocity) {
		this.start = start;
		this.direction = direction;
		this.velocity = velocity;
		this.transform.position = start;
		this.transform.rotation = Quaternion.FromToRotation(new Vector3(1,0,0), direction);
	}
}
