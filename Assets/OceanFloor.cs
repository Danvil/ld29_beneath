using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OceanFloor : MonoBehaviour {

	public List<GameObject> Tiles;
	const float FLOOR_X1 = -8;
	const float FLOOR_X2 = 12*4;
	const float FLOOR_Y1 = -6;
	const float FLOOR_Y2 = 12;
	const float RESOLUTION = 0.25f;
	const int OCEAN_FLOOR_MASK = 1<<9;

	// Use this for initialization
	void Start () {
		GeneratePerlin();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetHeight(float x) {
		RaycastHit hinfo;
		Physics.Raycast(
			new Vector3(x,-20,0),
			new Vector3(x,-1,0),
			out hinfo,
			40,
			OCEAN_FLOOR_MASK);
		return hinfo.point.y;
	}

	Mesh GeneratePerlinMesh() {
		int M = Mathf.CeilToInt((FLOOR_X2 - FLOOR_X1) / RESOLUTION);
		int N = Mathf.CeilToInt((FLOOR_Y2 - FLOOR_Y1) / RESOLUTION);
		AI.OceanFloorGenerator gen = new AI.OceanFloorPerlin();
		float[,] height = gen.Generate(M,N,RESOLUTION);
		Vector3[] vertices = new Vector3[M*N];
		for(int j=0; j<M; j++) {
			for(int i=0; i<N; i++) {
				float h = height[j,i];
				vertices[i*M + j] = new Vector3(FLOOR_X1+j,h,FLOOR_Y1+i);
			}
		}
		int[] indices = new int[(M-1)*(N-1)*6];
		for(int j=0; j<M-1; j++) {
			for(int i=0; i<N-1; i++) {
				int a = i*M + j;
				int k = 6*(i*(M-1) + j);
				indices[k] = a;
				indices[k + 1] = a + 1 + M;
				indices[k + 2] = a + 1;
				indices[k + 3] = a;
				indices[k + 4] = a + M;
				indices[k + 5] = a + 1 + M;
			}
		}
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = indices;
		mesh.RecalculateNormals();
		return mesh;
	}

	void GeneratePerlin () {
		Mesh mesh = GeneratePerlinMesh();
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

	void GenerateTileBase () {
		// row near
		for(int i=-2; i<12; i++) {
			GameObject pf = Tiles.RandomElement();
			GameObject go = (GameObject)Instantiate(pf);
			go.transform.position = new Vector3(4*i,0,0);
			go.transform.parent = this.transform;
		}
		// row far
		for(int i=-2; i<12; i++) {
			GameObject pf = Tiles.RandomElement();
			GameObject go = (GameObject)Instantiate(pf);
			go.transform.position = new Vector3(4*i,0,10);
			go.transform.parent = this.transform;
		}
	}
}
