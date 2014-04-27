﻿using UnityEngine;
using System.Collections;

public class SubmarineState : MonoBehaviour {

	public float emergeHeight = 5.21f;

	public float Hull { get; private set; }
	public float Oxygen { get; private set; }
	public int Gold { get; private set; }

	Hook hook;

	// Use this for initialization
	void Start () {
		hook = this.transform.Find("Hook").GetComponent<Hook>();
		Hull = 100;
		Oxygen = 100;
		Gold = 0;
	}

	int GetTreasureOnTheHook() {
		if(hook.Grapple.HasConnectedBody) {
			GameObject go = hook.Grapple.ConnectedBody;
			Treasure t = go.GetComponent<Treasure>();
			if(t) {
				return t.gold;
			}
		}
		return 0;
	}
	
	// Update is called once per frame
	void Update () {
		// check if treasure on the hook
		if(hook.IsWinchUp && hook.Grapple.HasConnectedBody) {
			// check if treasure!
			GameObject go = hook.Grapple.ConnectedBody;
			Treasure t = go.GetComponent<Treasure>();
			if(t != null) {
				Gold += t.gold;
				hook.Grapple.Unlink();
				Destroy(go);
			}
		}
		// check if go up
		if(this.transform.position.y > emergeHeight) {
			Globals.LastGameHull = (int)Hull;
			Globals.LastGameOxygen = (int)Oxygen;
			Globals.LastGameGold = Gold + GetTreasureOnTheHook();
			Application.LoadLevel("gameover");
		}
		// deplete oxygen
		Oxygen -= Time.deltaTime;
		// update gui
		GUI.Singleton.Hull = Mathf.CeilToInt(Hull);
		GUI.Singleton.Oxygen = Mathf.CeilToInt(Oxygen);
		GUI.Singleton.Gold = Mathf.CeilToInt(Gold);
	}
}