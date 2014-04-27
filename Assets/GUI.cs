using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

	public static GUI Singleton;

	public int Hull {
		set {
			this.transform.Find("Hull").guiText.text = string.Format("Hull: {0}", value);
		}
	}

	public int Oxygen {
		set {
			this.transform.Find("Oxygen").guiText.text = string.Format("Oxygen: {0}", value);
		}
	}
	
	public int Gold {
		set {
			this.transform.Find("Gold").guiText.text = string.Format("Gold: {0}", value);
		}
	}

	void Awake() {
		Singleton = this;
	}
	
	// Use this for initialization
	void Start () {
		Hull = 99;
		Oxygen = 74;
		Gold = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
