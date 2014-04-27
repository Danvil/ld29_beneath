using UnityEngine;
using System.Collections.Generic;

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
	
	static System.Random rnd = new System.Random();

	public static T RandomElement<T>(this T[] array) {
		return array[rnd.Next(array.Length)];
	}

	public static T RandomElement<T>(this List<T> list) {
		return list[rnd.Next(list.Count)];
	}

	public static Vector2 NormalSample2() {
		float q = Mathf.Sqrt(-2.0f*Mathf.Log(Random.value));
		float a = 2.0f*Mathf.PI*Random.value;
		return q * new Vector2(Mathf.Cos(a), Mathf.Sin(a));
	}
	
}

