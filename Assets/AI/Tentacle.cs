using UnityEngine;

namespace AI
{
	
	public abstract class TentacleIntelligence
	{
		public Tentacle Tentacle { get; set; }
		public TentacleIntelligence(Tentacle t) {
			Tentacle = t;
		}
		public abstract TentacleEndeffector Pose();
	}

	public struct TentacleEndeffector {
		public Vector3 position; // target position
		public Vector3 normal; // target surface normal
	}

	/// <summary>
	/// Aligns the normal to point to base
	/// </summary>
	public class TentacleFollow : TentacleIntelligence
	{
		public Transform Target { get; set; }

		public TentacleFollow(Tentacle t)
			: base(t)
		{ }

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
		public override TentacleEndeffector Pose()
		{
			return new TentacleEndeffector() {
				position = Target.transform.position,
				normal = EndeffectorNormal
			};
		}
		#endregion
	}

	/// <summary>
	/// Toches random objects in range
	/// </summary>
	public class TentacleTouchy : TentacleIntelligence
	{
		const float RANGE_MULT = 0.95f;
		const float LERP_RATE = 1.13f;
		const int COLLIDER_LAYER = 1<<8;

		GameObject current;
		float touchtime;

		TentacleEndeffector endeffector;
		
		public TentacleTouchy(Tentacle t)
			: base(t)
		{
			endeffector.position = Tentacle.Endeffector.transform.position;
			endeffector.normal = Tentacle.Base.transform.position - Tentacle.Endeffector.transform.position;
		}

		TentacleEndeffector Default {
			get {
				return new TentacleEndeffector() {
					position = new Vector3(0,8,0),
					normal = new Vector3(0,-1,0)
				};
			}
		}

		void Retarget() {
			// find all objects in range
			Collider[] colliders = Physics.OverlapSphere(Tentacle.Base.transform.position, RANGE_MULT*Tentacle.LengthMax, COLLIDER_LAYER);
			Debug.Log(colliders);
			if(colliders.Length == 0) {
				current = null;
			}
			else {
				current = colliders.RandomElement().gameObject;
			}
			touchtime = 2.0f + Random.value*2.0f;
		}

		void SmoothIn(TentacleEndeffector target) {
			float lerpfact = Time.deltaTime * LERP_RATE;
			this.endeffector.position = Vector3.Lerp(this.endeffector.position, target.position, lerpfact);
			Vector3 delta = this.endeffector.position - Tentacle.Base.transform.position;
			float m = delta.magnitude;
			if(m >= RANGE_MULT*Tentacle.LengthMax) {
				this.endeffector.position = Tentacle.Base.transform.position + RANGE_MULT*Tentacle.LengthMax/m*delta;
			}
			this.endeffector.normal = Vector3.Lerp(this.endeffector.normal, target.normal, lerpfact).ZeroNormalize(); // FIXME
		}

		#region Intelligence implementation
		public override TentacleEndeffector Pose ()
		{
			if(current == null || touchtime <= 0) {
				Retarget();
			}
			if(current == null) {
				// goto default
				SmoothIn(Default);
			}
			else {
				// touch object
				SmoothIn(new TentacleEndeffector() {
					position = current.transform.position,
					normal = Tentacle.Base.transform.position - current.transform.position
				});
				// check if touching
				bool touching = (Tentacle.Endeffector.transform.position - current.transform.position).magnitude < 0.3f;
				if(touching) {
					touchtime -= Time.deltaTime;
				}
			}

			return endeffector;
		}
		#endregion
	}
	
}

