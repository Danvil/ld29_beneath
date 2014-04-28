using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	FadeScreen fade;

	void Awake() {
		fade = this.GetComponent<FadeScreen>();
	}

	// Use this for initialization
	void Start () {
		this.guiText.text = "Yellow Submarine Treasure Hunt\n\nPress 'F' to start a game";
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
