using UnityEngine;
using System.Linq;
using System.Collections.Generic;

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
		float notinrangetime;

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

		IEnumerable<GameObject> FilterColliders(Collider[] colliders) {
			return colliders.Where(c => !Tentacle.LinkColliders.Any(a => a == c)).Select(c => c.gameObject);
		}

		GameObject SelectTouchy(List<GameObject> choice) {
			if(choice.Count == 0) {
				return null;
			}
			if(Random.value >= 0.6) {
				return null;
			}
			return choice.RandomElement();
		}

		void Retarget() {
			// find all objects in range
			Collider[] colliders = Physics.OverlapSphere(Tentacle.Base.transform.position, RANGE_MULT*Tentacle.LengthMax, COLLIDER_LAYER);
			current = SelectTouchy(FilterColliders(colliders).ToList());
			touchtime = 1.0f + Random.value*2.0f;
			notinrangetime = 6.7f;
			Tentacle.Grapple.TargetBody = current;
			Tentacle.Grapple.MakeConnection = true;
		}

		public Vector3 Dbg1 { get; set; }
		public Vector3 Dbg2 { get; set; }
		public Vector3 Dbg3 { get; set; }

		void SmoothIn(TentacleEndeffector target) {
			// position
			Vector3 src = Tentacle.Endeffector.transform.position;
			Vector3 dst = target.position;
			Vector3 root = Tentacle.Base.transform.position;
			Vector3 dsrc = (src - root);
			Vector3 ddst = (dst - root);
			Vector3 dsrc0 = dsrc.ZeroNormalize();
			Vector3 ddst0 = ddst.ZeroNormalize();
			Vector3 axis = (src.x < dst.x ? 1.0f : -1.0f) * new Vector3(0,0,-1);
			Dbg3 = axis;
			float radprocent = Mathf.Max(0.0f, Vector3.Dot(dsrc0, ddst0));
			Quaternion q = Quaternion.AngleAxis(Time.deltaTime*10.0f*(1.0f - radprocent), axis);
			float rdst = Mathf.Max(0.6f*Tentacle.LengthMax, ddst.magnitude);
			float rtarget = (1.0f-radprocent)*0.8f*Tentacle.LengthMax + radprocent*rdst;
			float rnow = dsrc.magnitude;
			float r = 0.8f*rnow + 0.2f*rtarget;
			Dbg1 = root + r*dsrc0;
			Dbg2 = root + q*(r*dsrc0);
			this.endeffector.position = root + r*(q*dsrc0);

			// normal
			float theta = (1.0f - Mathf.Min(Mathf.Max(0.0f, src.y/Tentacle.LengthMax), 1.0f));
			Vector3 n0 = root - src;
			if(n0.x < 0) theta *= -1.0f;
			Vector3 n = Quaternion.AngleAxis(theta * 90.0f, new Vector3(0,0,1)) * n0;
			this.endeffector.normal = n.ZeroNormalize();

//			float lerpfact = Time.deltaTime * LERP_RATE;
//			this.endeffector.position = Vector3.Lerp(this.endeffector.position, target.position, lerpfact);
//			Vector3 delta = this.endeffector.position - Tentacle.Base.transform.position;
//			float m = delta.magnitude;
//			if(m >= RANGE_MULT*Tentacle.LengthMax) {
//				this.endeffector.position = Tentacle.Base.transform.position + RANGE_MULT*Tentacle.LengthMax/m*delta;
//			}
//			// normal
//			this.endeffector.normal = Vector3.Lerp(Tentacle.EndeffectorNormal, target.normal, lerpfact).ZeroNormalize(); // FIXME
		}

		#region Intelligence implementation
		public override TentacleEndeffector Pose ()
		{
			if(current == null || touchtime <= 0 || notinrangetime <= 0.0f) {
				Retarget();
			}
			if(current == null) {
				// goto default
				SmoothIn(Default);
			}
			else {
				// check if touching
				bool touching = Tentacle.Grapple.HasConnectedBody;
				if(touching) {
					touchtime -= Time.deltaTime;
					// move somewhere
					SmoothIn(Default);
				}
				else {
					notinrangetime -= Time.deltaTime;
					// move to thingy
					SmoothIn(new TentacleEndeffector() {
						position = current.transform.position,
						normal = Tentacle.Base.transform.position - current.transform.position
					});
				}
			}

			return endeffector;
		}
		#endregion
	}
	
}

