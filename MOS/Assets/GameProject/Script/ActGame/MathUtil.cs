using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtil  {

	/// <summary>
	/// todo 2维圆线插值
	/// </summary>
	/// <param name="v1"></param>
	/// <param name="v2"></param>
	/// <param name="a"></param>
	/// <returns></returns>
	public static Vector2 Vector2Lerp(Vector2 v1,Vector2 v2,float a)
	{
		var q1 = Quaternion.identity;
		var q2 = Quaternion.FromToRotation(new Vector2(v1.x,v1.y), new Vector2(v2.x,v2.y));
		var q = Quaternion.Lerp(q1, q2, a);
		//bug
		var v = q * new Vector2(v1.x, v1.y);
		var res = new Vector2(v.x, v.y);
		res.Normalize();
		return res;
    }

	public static float Vector2Angle(Vector2 v1, Vector2 v2)
	{
		return Vector2.SignedAngle(v1, v2);
	}
}
