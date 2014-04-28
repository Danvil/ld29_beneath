using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionDust : MonoBehaviour {

	public GameObject pfDustFx;
	public float cooldown = 1.0f;

	float currentCooldown = 0.0f;

	List<GameObject> fx = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		currentCooldown -= Time.deltaTime;
		List<GameObject> fxnew = new List<GameObject>();
		foreach(GameObject x in fx) {
			if(!x.particleSystem.IsAlive() || !IsVisible) {
				Destroy(x, 1.0f);
			}
			else {
				fxnew.Add(x);
			}
		}
		fx = fxnew;
	}

	bool IsVisible {
		get {
			Vector2 cp = Camera.Singleton.camera.WorldToViewportPoint(this.transform.position);
			return (0 <= cp.x && cp.x <= 1 && 0 <= cp.y && cp.y <= 1);
		}
	}

	void OnCollisionStay(Collision collisionInfo) {
		if(!pfDustFx) {
			return;
		}
		if(Time.time < 5.0f) {
			return;
		}
		if(currentCooldown > 0 || collisionInfo.contacts.Length == 0) {
			return;
		}
		// check if visible
		if(!IsVisible) {
			return;
		}
//		foreach (ContactPoint contact in collisionInfo.contacts) {
//			Debug.DrawRay(contact.point, contact.normal, Color.white);
//		}
		this.currentCooldown = this.cooldown;
		GameObject go = (GameObject)Instantiate(pfDustFx);
		go.transform.position = collisionInfo.contacts[0].point;
		go.transform.parent = this.transform;
		fx.Add(go);
	}
}
