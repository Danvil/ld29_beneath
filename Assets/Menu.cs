using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.guiText.text = "Yello Submarine\nTreasure Hunt\n\nPress 'F' to start a game";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)) {
			Application.LoadLevel("ocean-test");
		}
	}
}
