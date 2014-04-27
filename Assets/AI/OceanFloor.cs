using UnityEngine;

namespace AI
{

	public interface OceanFloorGenerator
	{
		float[,] Generate(int W, int H, float resolution);
	}

	public class OceanFloorFlat : OceanFloorGenerator
	{
		#region OceanFloorGenerator implementation
		public float[,] Generate (int W, int H, float resolution)
		{
			float[,] a = new float[W,H];
			for(int i=0; i<H; i++) {
				for(int j=0; j<W; j++) {
					a[j,i] = 0.0f;
				}
			}
			return a;
		}
		#endregion
	}

	public class OceanFloorPerlin : OceanFloorGenerator
	{
		public float borderHeight = 7.4f;
		public float borderPadd = 1.0f;

		#region OceanFloorGenerator implementation
		public float[,] Generate (int W, int H, float resolution)
		{
			SLPerlinNoise.PerlinNoise3D perlin = new SLPerlinNoise.PerlinNoise3D();
			perlin.Frequency = 0.055f;
			perlin.Octaves = 4;
			perlin.Amplitude = 2.0f;
			perlin.InitNoiseFunctions();
			float[,] a = new float[W,H];
			for(int j=0; j<W; j++) {
				float x = (float)(j)*resolution;
				float parg = Mathf.Min(x, W-1-j)*resolution/borderPadd;
				float p = Mathf.Exp(-0.5f*parg*parg);
				for(int i=0; i<H; i++) {
					float h = p*borderHeight + (1.0f-p)*perlin.Compute(x,i*resolution,0.0f);
					a[j,i] = h;
				}
			}
			return a;
		}
		#endregion
	}
	
}
