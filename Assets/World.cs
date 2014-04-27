using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public GameObject pfFish;
	public GameObject pfTreasure;
	public GameObject pfBoulder;
	public GameObject pfOceanFloor;
	public GameObject pfWeed;
	public GameObject pfTentacle;

	public int weedCount = 50;
	public int fishCount = 10;
	public int boulderScatterCount = 100;
	public int treasureCount = 50;
	public int boulderGameCount = 50;
	public int tentacleCount = 5;

	Vector3 RandomWeedPos() {
		return OceanFloor.Singleton.RandomInArea();
	}
	
	Vector3 RandomScatterBoulderPos() {
		do {
			Vector3 p = OceanFloor.Singleton.RandomInArea();
			if(Mathf.Abs(p.z) > 3) return p;
		} while(true);
	}
	
	Vector3 RandomTreasurePos() {
		do {
			Vector3 p = OceanFloor.Singleton.RandomInArea();
			if(p.x > 6) {
				p.z = 0.0f;
				p.y = OceanFloor.Singleton.GetHeight(p.x);
				return p;
			}
		} while(true);
	}

	Vector3 RandomGameBoulderPos() {
		Vector3 p = OceanFloor.Singleton.RandomInArea();
		p.z = 0.0f;
		p.y = OceanFloor.Singleton.GetHeight(p.x);
		return p;
	}
	
	Vector3 RandomTentaclePos() {
		do {
			Vector3 p = OceanFloor.Singleton.RandomInArea();
			if(p.x > 6) {
				p.z = 0.0f;
				p.y = OceanFloor.Singleton.GetHeight(p.x);
				return p;
			}
		} while(true);
	}
	
	void Generate() {
		// floor
		Instantiate(pfOceanFloor);
		// weed patches
		for(int i=0; i<fishCount; i++) {
			Vector3 p = RandomWeedPos() + new Vector3(0,2.5f,0);
			GameObject go = (GameObject)Instantiate(pfFish);
			Swarm w = go.GetComponent<Swarm>();
			w.ai.AreaCenter = p;
		}
		// fish
		for(int i=0; i<weedCount; i++) {
			Vector3 p = RandomWeedPos();
			GameObject go = (GameObject)Instantiate(pfWeed);
			Weed w = go.GetComponent<Weed>();
			go.transform.position = new Vector3(p.x, 0, p.z); // need 0 height!
		}
		// environment boulders
		for(int i=0; i<boulderScatterCount; i++) {
			Vector3 p = RandomScatterBoulderPos();
			GameObject go = (GameObject)Instantiate(pfBoulder);
			float vscl = (0.5f + 1.5f*Random.value);
			go.transform.localScale *= vscl;
			go.transform.position = p + new Vector3(0,1.0f,0);
			go.rigidbody.mass *= vscl;
		}
		// treasure
		for(int i=0; i<treasureCount; i++) {
			Vector3 p = RandomTreasurePos();
			GameObject go = (GameObject)Instantiate(pfTreasure);
			int level = 1 + Tools.RandomInt(5);
			float vscl = Mathf.Pow(level,0.333f);
			go.GetComponent<Treasure>().gold = level;
			go.transform.localScale *= vscl;
			go.transform.position = p + new Vector3(0,1.0f,0);
			go.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
			go.rigidbody.mass *= vscl;
		}
		// game boulders
		for(int i=0; i<boulderGameCount; i++) {
			Vector3 p = RandomGameBoulderPos();
			GameObject go = (GameObject)Instantiate(pfBoulder);
			float vscl = (0.5f + 1.75f*Random.value);
			go.transform.localScale *= vscl;
			go.transform.position = p + new Vector3(0,1.5f,0);
			go.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
			go.rigidbody.mass *= vscl;
		}
		// tentacles
		for(int i=0; i<tentacleCount; i++) {
			Vector3 p = RandomTentaclePos();
			GameObject go = (GameObject)Instantiate(pfTentacle);
			go.transform.position = p ;
		}
	}

	void Awake() {
		Generate();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
