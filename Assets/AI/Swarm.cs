using UnityEngine;

namespace AI
{

	public abstract class SwarmIntelligence
	{
		public Swarm Swarm { get; set; }
		public abstract Vector3 Target(float time);
	}

	public class SwarmCircle : SwarmIntelligence
	{
		public Vector3 Center = Vector3.zero;
		public float Velocity = 1.0f;
		public float Radius = 1.0f;
		
		#region Intelligence implementation
		public override Vector3 Target (float time)
		{
			float phi = time*Velocity/Radius;
			float x = Radius*Mathf.Cos(phi);
			float y = Radius*Mathf.Sin(phi);
			return Center + new Vector3(x,0,y);
		}
		#endregion
	}

	public class SwarmRandom : SwarmIntelligence
	{
		public Vector3 AreaCenter = Vector3.zero;
		public float AreaRadius = 10.0f;
		public float AreaHeight = 2.0f;

		bool currentValid = false;
		Vector3 current;

		void NewTarget() {
			Vector2 r = AreaRadius * Random.insideUnitCircle;
			float h = AreaHeight * (-1.0f + 2.0f*Random.value);
			current = AreaCenter + new Vector3(r.x,h,r.y);
		}

		#region Intelligence implementation
		public override Vector3 Target (float time)
		{
			while(true) {
				float dbest = Swarm.DistanceToNearest(current);
				if(dbest <= 0.5f) {
					currentValid = false;
				}
				if(currentValid) {
					break;
				}
				NewTarget();
				currentValid = true;
			}
			return current;
		}
		#endregion
	}
	
}

