using UnityEngine;
using System.Collections;

public class Gameover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int score = Globals.Score;
		int gold = Globals.LastGameGold;
		if(score == 0) {
			this.guiText.text = "Game over!\n\nPress 'F' to return to menu";
		}
		else if(gold > 0) {
			this.guiText.text = string.Format("You found TREASURE!\nScore: {0}\n\nPress 'F' to return to menu", score);
		}
		else {
			this.guiText.text = string.Format("Bad luck, at least you did not die...\nScore: {0}\n\nPress 'F' to return to menu", score);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)) {
			Application.LoadLevel("menu");
		}
	}
}
