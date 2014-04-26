using UnityEngine;

public static class Tools
{
	public static Vector3 ZeroNormalize(this Vector3 a) {
		float q = a.magnitude;
		if(q == 0.0f) {
			return Vector3.zero;
		}
		else {
			return a/q;
		}
	}	

}

