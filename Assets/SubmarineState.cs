using UnityEngine;
using System.Collections;

public class SubmarineState : MonoBehaviour {

	public float emergeHeight = 5.21f;

	public float Hull { get; private set; }
	public float Oxygen { get; private set; }
	public int Gold { get; private set; }

	// Use this for initialization
	void Start () {
		Hull = 100;
		Oxygen = 100;
		Gold = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// check if go up
		if(this.transform.position.y > emergeHeight) {
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
