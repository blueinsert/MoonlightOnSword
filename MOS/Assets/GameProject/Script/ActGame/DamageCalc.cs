using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageCalcResult
{
	public float damage;
    public DamageSourceDir damageSourceDir;
    public Vector3 damageSourceDirVec;
}

public enum DamageSourceDir
{
    Front,
    Back,
    Left,
    Right,
}

public class DamageCalc  {

    public DamageSourceDir  CalcDamageSourceDir(Vector2 p,Vector2 dir,Vector2 p2)
    {
        var dir2 = (p2 - p).normalized;
        var angle = Vector2.SignedAngle(dir, dir2);
        if(Mathf.Abs(angle) < 45)
        {
            return DamageSourceDir.Front;
        }else if (Mathf.Abs(angle) > 135)
        {
            return DamageSourceDir.Back;
        }else if(angle > 0)
        {
            return DamageSourceDir.Right;
        }
        else
        {
            return DamageSourceDir.Left;
        }
    }
   

    public DamageSourceDir CalcDamageSourceDir(Transform from, Transform to)
    {
        var p = new Vector2(to.position.x, to.position.z);
        var dir = new Vector2(to.forward.x, to.forward.z);
        var p2 = new Vector2(from.position.x, from.position.z);
        return CalcDamageSourceDir(p, dir, p2);
    }

    public DamageCalcResult CalcDamage(PropertyComp property1, PropertyComp property2, HitEffectConfig hitdef)
    {
        DamageCalcResult result = new DamageCalcResult();
        result.damage = 0;
        result.damageSourceDir = CalcDamageSourceDir(property1.transform, property2.transform);
        var dir = (property1.transform.position - property2.transform.position).normalized;
        result.damageSourceDirVec = dir;
        return result;
    }
}
