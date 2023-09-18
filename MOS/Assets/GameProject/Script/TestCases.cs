using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestCases {

	public static void TestVectorLerp()
	{
		Vector2 v1 = new Vector2(-0.8f,0.6f);
		Vector2 v2 = new Vector2(0.8f,-0.6f);
		for(float a = 0; a < 1; a += 0.1f)
		{
			//var v = Vector2.Lerp(v1, v2, a);
			var v = MathUtil.Vector2Lerp(v1, v2, a);
			Debug.Log(string.Format("{0} {1} {2} {3}", v1, v2, a, v));
		}

	}
}
