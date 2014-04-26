using UnityEngine;

namespace AI
{

	public interface SwarmIntelligence
	{
		Vector3 Target(float time);
	}

	public class SwarmCircle : SwarmIntelligence
	{
		public Vector3 Center = Vector3.zero;
		public float Velocity = 1.0f;
		public float Radius = 1.0f;
		
		#region Intelligence implementation
		public Vector3 Target (float time)
		{
			float phi = time*Velocity/Radius;
			float x = Radius*Mathf.Cos(phi);
			float y = Radius*Mathf.Sin(phi);
			return Center + new Vector3(x,0,y);
		}
		#endregion
	}

}

