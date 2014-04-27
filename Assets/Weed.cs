using UnityEngine;
using System.Collections.Generic;

public class Weed : MonoBehaviour {

	public int segmentsHeight = 4;
	public int segmentsTheta = 3;
	public float radius = 0.1f;
	public float height = 0.5f;
	public float sliceCenterRnd = 0.1f;
	public float sliceCenterChange = 0.6f;

	public AI.WeedDistribution ai;

	// Use this for initialization
	void Start () {
		ai = new AI.WeedGauss();
		UpdateMesh();
	}

	List<Vector3> vertices;
	List<int> indices;

	void GrowWeed(Vector3 center) {
		int index0 = vertices.Count;
		float offset = Random.value * 2.0f * Mathf.PI;
		for(int k=0; k<=segmentsHeight; k++) {
			float kp = (float)(k)/(segmentsHeight);
			float h = height*kp;
			float r = radius*(1.0f - 0.9f*kp);
			Vector3 sliceCenter = sliceCenterRnd * new Vector3(-1.0f+2.0f*Random.value, 0, -1.0f+2.0f*Random.value);
			sliceCenter += center;
			center = (1-sliceCenterChange)*center + sliceCenterChange*sliceCenter;
			for(int s=0; s<segmentsTheta; s++) {
				float angle = offset + ((float)(s) / segmentsTheta) * 2.0f * Mathf.PI;
				Vector3 p = sliceCenter + r * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) + new Vector3(0,h,0);
				vertices.Add(p);
				if(k < segmentsHeight) {
					int a = index0 + segmentsTheta*k + s;
					int ds = (s + 1) % segmentsTheta - s;
					indices.Add(a);
					indices.Add(a + ds + segmentsTheta);
					indices.Add(a + ds);
					indices.Add(a);
					indices.Add(a + segmentsTheta);
					indices.Add(a + ds + segmentsTheta);
				}
			}
		}
	}

	void GrowWeed() {
		var locs = ai.Generate();
		foreach(Vector2 p in locs) {
			float h = OceanFloor.Singleton.GetHeight(p.x,p.y);
			GrowWeed(new Vector3(p.x, h, p.y));
		}
	}

	Mesh CreateWeedMesh() {
		vertices = new List<Vector3>();
		indices = new List<int>();
		GrowWeed();
		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = indices.ToArray();
		mesh.RecalculateNormals();
		return mesh;
	}

	void UpdateMesh() {
		Mesh mesh = CreateWeedMesh();
		Debug.Log(mesh);
		var meshFilter = GetComponent<MeshFilter>();
		Debug.Log(meshFilter);
		if(meshFilter) {
			meshFilter.mesh = mesh;
		}
		var meshCollider = GetComponent<MeshCollider>();
		Debug.Log(meshCollider);
		if(meshCollider) {
			meshCollider.sharedMesh = mesh;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
