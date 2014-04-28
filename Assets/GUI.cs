using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

	public static GUI Singleton;

	FadeScreen fade;

	GUIText txtHull;
	GUIText txtOxygen;
	GUIText txtGold;
	GUIText txtEmerge;

	private int hull;
	public int Hull {
		set {
			hull = value;
			txtHull.guiText.text = string.Format("Hull: {0}", value);
		}
	}

	private int oxygen;
	public int Oxygen {
		set {
			oxygen = value;
			txtOxygen.guiText.text = string.Format("Oxygen: {0}", value);
		}
	}
	
	public int Gold {
		set {
			txtGold.guiText.text = string.Format("Gold: {0}", value);
		}
	}

	public void EnableEmerge(float time) {
		txtEmerge.enabled = true;
		txtEmerge.text = string.Format("Emerging in {0}", Mathf.RoundToInt(time));
	}

	public void DisableEmerge() {
		txtEmerge.enabled = false;
	}
	
	void Awake() {
		Singleton = this;
		txtHull = this.transform.Find("Hull").guiText;
		txtOxygen = this.transform.Find("Oxygen").guiText;
		txtGold = this.transform.Find("Gold").guiText;
		txtEmerge = this.transform.Find("EmergeNote").guiText;
		fade = this.GetComponent<FadeScreen>();
	}
	
	// Use this for initialization
	void Start () {
		fade.SetupFadeOut();
		fade.IsFading = true;
	}

	static Color ToColor(Vector3 v) {
		return new Color(v.x, v.y, v.z);
	}
	
	// Update is called once per frame
	void Update () {
		Hull = Mathf.CeilToInt(SubmarineState.Singleton.Hull);
		Oxygen = Mathf.CeilToInt(SubmarineState.Singleton.Oxygen);
		Gold = Mathf.CeilToInt(SubmarineState.Singleton.Gold);

		float p1 = Mathf.Pow(0.5f + 0.5f*Mathf.Sin(Time.time/1.5f*2.0f*Mathf.PI), 3.0f);	
		float p2 = Mathf.Pow(0.5f + 0.5f*Mathf.Sin(Time.time/0.5f*2.0f*Mathf.PI), 3.0f);
		
		if(SubmarineState.Singleton.HullDangerLevel == 0) {
			txtHull.color = Color.white;
		}
		if(SubmarineState.Singleton.HullDangerLevel == 1) {
			txtHull.color = ToColor((1.0f-p1)*new Vector3(1,1,1) + p1*new Vector3(1,1,0));
		}
		if(SubmarineState.Singleton.HullDangerLevel == 2) {
			txtHull.color = ToColor((1.0f-p2)*new Vector3(1,1,1) + p2*new Vector3(1,0,0));
		}

		if(SubmarineState.Singleton.OxygenDangerLevel == 0) {
			txtOxygen.color = Color.white;
		}
		if(SubmarineState.Singleton.OxygenDangerLevel == 1) {
			txtOxygen.color = ToColor((1.0f-p1)*new Vector3(1,1,1) + p1*new Vector3(1,1,0));
		}
		if(SubmarineState.Singleton.OxygenDangerLevel == 2) {
			txtOxygen.color = ToColor((1.0f-p2)*new Vector3(1,1,1) + p2*new Vector3(1,0,0));
		}
	}
}
