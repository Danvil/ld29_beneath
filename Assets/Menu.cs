using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	FadeScreen fade;

	void Awake() {
		fade = this.GetComponent<FadeScreen>();
	}

	// Use this for initialization
	void Start () {
		this.guiText.text = "Yellow Submarine Treasure Hunt\n\nPress 'F' to start a game\n\nControls:\nWASD or Arrows to MOVE\nF/G/H or Mouse Buttons to control HOOK";
		fade.SetupFadeIn();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)) {
			fade.IsFading = true;
		}
		if(fade.IsFadeFinished) {
			Application.LoadLevel("beneath");
		}
	}
}
