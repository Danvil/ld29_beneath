using UnityEngine;
using System.Collections;

public class CollisionDust : MonoBehaviour {

	public GameObject pfDustFx;
	public float cooldown = 1.0f;

	float currentCooldown = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		currentCooldown -= Time.deltaTime;
	}

	void OnCollisionStay(Collision collisionInfo) {
		foreach (ContactPoint contact in collisionInfo.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		if(currentCooldown <= 0 && collisionInfo.contacts.Length > 0) {
			this.currentCooldown = this.cooldown;
			GameObject go = (GameObject)Instantiate(pfDustFx);
			go.transform.position = collisionInfo.contacts[0].point;
			go.transform.parent = this.transform;
			Destroy(go, 5.0f);
		}
	}
}
