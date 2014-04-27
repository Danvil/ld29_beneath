using UnityEngine;
using System.Collections;

public class Gameover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Globals.Highscore = Mathf.Max(Globals.Highscore, Globals.Score);
		int score = Globals.Score;
		int gold = Globals.LastGameGold;
		if(score == 0) {
			if(Globals.LastGameHull == 0) {
				this.guiText.text = "Ship destroyed!\n\nPress 'F' to try again";
			}
			else if(Globals.LastGameOxygen == 0) {
				this.guiText.text = "You suffocated!\n\nPress 'F' to try again";
			}
			else {
				this.guiText.text = "Game over!\n\nPress 'F' to try again";
			}
		}
		else if(gold > 0) {
			this.guiText.text = string.Format("You found GOLD!\nScore: {0}\nCurrent highscore: {0}\n\nPress 'F' to try again", score, Globals.Highscore);
		}
		else {
			this.guiText.text = string.Format("Bad luck, at least you did not die...\nScore: {0}\n\nPress 'F' to try again", score);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)) {
			Application.LoadLevel("beneath");
		}
	}
}
