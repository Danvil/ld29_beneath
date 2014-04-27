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

	public void EnableEmerge(float time) {
		this.transform.Find("EmergeNote").guiText.enabled = true;
		this.transform.Find("EmergeNote").guiText.text = string.Format("Emerging in {0}", Mathf.RoundToInt(time));
	}

	public void DisableEmerge() {
		this.transform.Find("EmergeNote").guiText.enabled = false;
	}
	
	void Awake() {
		Singleton = this;
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
