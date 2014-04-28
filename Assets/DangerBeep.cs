using UnityEngine;
using System.Collections;

public class DangerBeep : MonoBehaviour {

	public AudioClip dangerLow;
	public AudioClip dangerHigh;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int lvl = SubmarineState.Singleton.DangerLevel;
		if(lvl == 0) {
			this.audio.clip = null;
		}
		if(lvl == 1 && this.audio.clip != dangerLow) {
			this.audio.clip = dangerLow;
			this.audio.Play();
		}
		if(lvl == 2 && this.audio.clip != dangerHigh) {
			this.audio.clip = dangerHigh;
			this.audio.Play();
		}
	}
}
