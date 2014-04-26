using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OceanFloor : MonoBehaviour {

	public List<GameObject> Tiles;

	// Use this for initialization
	void Start () {
		GenerateOceanFloor();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GenerateOceanFloor () {
		const int SPACING = 4;
		// row near
		for(int i=-2; i<12; i++) {
			GameObject pf = Tiles.RandomElement();
			GameObject go = (GameObject)Instantiate(pf);
			go.transform.position = new Vector3(SPACING*i,0,0);
			go.transform.parent = this.transform;
		}
		// row far
		for(int i=-2; i<12; i++) {
			GameObject pf = Tiles.RandomElement();
			GameObject go = (GameObject)Instantiate(pf);
			go.transform.position = new Vector3(SPACING*i,0,10);
			go.transform.parent = this.transform;
		}
	}
}
