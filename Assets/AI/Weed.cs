using System.Collections.Generic;
using UnityEngine;

namespace AI
{
	
	public interface WeedDistribution
	{
		List<Vector2> Generate();
	}

	public class WeedUniformRect : WeedDistribution
	{
		public int count = 100;
		public Vector2 areaMin, areaMax;
		
		#region WeedDistribution implementation
		public List<Vector2> Generate ()
		{
			List<Vector2> weed = new List<Vector2>();
			for(int i=0; i<count; i++) {
				Vector2 p = areaMax - areaMin;
				Vector2 r = new Vector2(Random.value, Random.value);
				p = areaMin + new Vector2(p.x*r.x, p.y*r.y);
				weed.Add(p);
			}
			return weed;
		}
		#endregion
	}

	public class WeedGauss : WeedDistribution
	{
		public int count = 100;
		public Vector2 center;
		public float radius = 0.5f;

		#region WeedDistribution implementation
		public List<Vector2> Generate ()
		{
			List<Vector2> weed = new List<Vector2>();
			for(int i=0; i<count/2; i++) {
				weed.Add(center + radius*Tools.NormalSample2());
			}
			return weed;
		}
		#endregion
	}
}
