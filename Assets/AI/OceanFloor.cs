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
		#region OceanFloorGenerator implementation
		public float[,] Generate (int W, int H, float resolution)
		{
			SLPerlinNoise.PerlinNoise3D perlin = new SLPerlinNoise.PerlinNoise3D();
			perlin.Frequency = 0.13f;
			perlin.Octaves = 4;
			perlin.Amplitude = 2.0f;
			perlin.InitNoiseFunctions();
			float[,] a = new float[W,H];
			for(int i=0; i<H; i++) {
				for(int j=0; j<W; j++) {
					a[j,i] = perlin.Compute(j*resolution,i*resolution,0.0f);
				}
			}
			return a;
		}
		#endregion
	}
	
}
