using UnityEngine;
using System.Collections;

public class FadeScreen : MonoBehaviour {

	public Color color;
	public Texture foreground;
	public float fadeSpeed = 1.0f;

	float alphaFadeValue = 0.0f;

	public bool IsFading { get; set; }

	float fadeSign = 0.0f;

	public bool IsFadeFinished {
		get {
			if(fadeSign > 0) {
				return alphaFadeValue >= 0.99f;
			}
			else {
				return alphaFadeValue <= 0.01f;
			}
		}
	}

	public void SetupFadeIn() {
		alphaFadeValue = 0.0f;
		fadeSign = 1;
	}

	public void SetupFadeOut() {
		alphaFadeValue = 1.0f;
		fadeSign = -1;
	}

	public FadeScreen() {
		IsFading = false;
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(IsFading) {
			alphaFadeValue += fadeSign * Time.deltaTime / fadeSpeed;
			alphaFadeValue = Mathf.Clamp01(alphaFadeValue);
		}
	}
	
	void OnGUI () {
		UnityEngine.GUI.color = new Color(color.r, color.g, color.b, alphaFadeValue);
		UnityEngine.GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), foreground);
	}
}
