using UnityEngine;
using System.Collections;

public class Gameover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.guiText.text = "Game over!\n\nPress 'F' to return to menu";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)) {
			Application.LoadLevel("menu");
		}
	}
}
