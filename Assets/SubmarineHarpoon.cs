using UnityEngine;
using System.Collections;

public class SubmarineHarpoon : MonoBehaviour {

	public GameObject pfHarpoonSpear;
	public Vector3 harpoonStart;
	public Vector3 harpoonDirection;
	public float harpoonVelocity;

	GameObject harpoonGo;

	Rope rope;

	bool isLoaded;

	// Use this for initialization
	void Start () {
		BuildRope();
		isLoaded = true;
	}

	void BuildRope() {
		harpoonGo = (GameObject)Instantiate(pfHarpoonSpear);
		rope = GetComponent<Rope>();
		harpoonGo.transform.position = this.transform.position + new Vector3(5,0,0);
		rope.target = harpoonGo.transform;
		rope.BuildRope();
		//harpoonGo.transform.position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(isLoaded && Input.GetButton("Fire1")) {
			// fire harpoon
			isLoaded = false;
		}
		if(!isLoaded && Input.GetButton("Fire2")) {
			// get harpoon back
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Vector3 a = this.transform.TransformPoint(harpoonStart);
		Vector3 b = a + 0.4f*this.transform.TransformDirection(harpoonDirection);
		Gizmos.DrawWireSphere(a, 0.1f);
		Gizmos.DrawLine(a, b);
	}
}
