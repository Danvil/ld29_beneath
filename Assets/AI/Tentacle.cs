using UnityEngine;

namespace AI
{
	
	public abstract class TentacleIntelligence
	{
		public Tentacle Tentacle { get; set; }
		public abstract TentacleEndeffector Pose(float time);
	}

	public struct TentacleEndeffector {
		public Vector3 position; // target position
		public Vector3 normal; // target surface normal
	}

	public class TentacleFollow : TentacleIntelligence
	{
		public Transform Target { get; set; }

		Vector3 EndeffectorNormalSlanted {
			get {
				Vector3 a = Tentacle.Base.transform.position;
				Vector3 b = Target.transform.position;
				Vector3 d = a - b;
				float dist = d.magnitude;
				float rot = Mathf.Min(Mathf.Max(0.0f, 1.0f - dist / Tentacle.LengthMax), 1.0f);
				if(b.x < a.x) rot *= -1.0f;
				Quaternion q = Quaternion.AngleAxis(90.0f*rot, new Vector3(0,0,1));
				if(dist == 0.0f) {
					return new Vector3(0,1,0);
				}
				else {
					return q * d / dist;
				}
			}
		}
		
		Vector3 EndeffectorNormal {
			get {
				Vector3 a = Tentacle.Base.transform.position;
				Vector3 b = Target.transform.position;
				Vector3 d = a - b;
				float dist = d.magnitude;
				if(dist == 0.0f) {
					return new Vector3(0,1,0);
				}
				else {
					return d / dist;
				}
			}
		}
		
		#region Intelligence implementation
		public override TentacleEndeffector Pose (float time)
		{
			return new TentacleEndeffector() {
				position = Target.transform.position,
				normal = EndeffectorNormal
			};
		}
		#endregion
	}
	
	public class TentacleTouch : TentacleIntelligence
	{
		#region Intelligence implementation
		public override TentacleEndeffector Pose (float time)
		{
			return new TentacleEndeffector();
		}
		#endregion
	}
	
}

