using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CaredCompType(typeof(PushComp))]
public class PushSystem : SystemBase
{
    private void ProcessCollidePair(PushCollidePair pair)
    {
        var p1 = pair.P1;
        var p2 = pair.P2;
        var dist = (p1.gameObject.transform.position - p2.gameObject.transform.position).magnitude;
        var dir = p1.gameObject.transform.position - p2.gameObject.transform.position;
        dir.y = 0;
        dir.Normalize();
        float intersect = (p1.m_radius + p2.m_radius) * 1.1f - dist;
        var offset1 = -intersect * dir * 0.5f;
        var offset2 = intersect * dir * 0.5f;

        p1.gameObject.transform.position += offset1;
        p2.gameObject.transform.position += offset2;
    }

    protected override void OnTick()
    {
        Debug.Log(string.Format("PushSystem:OnTick comp Count:{0}", m_compList.Count));
        //base.OnTick();
        foreach(var comp in m_compList)
        {
            if (comp.IsEnable)
            {
                var pushComp = comp as PushComp;
                if (pushComp.IsCollideWithOthers())
                {
                    int len = 0;
                    var collidePairs = pushComp.GetCollidePairs(out len);
                    for (int i = 0; i < len; i++)
                    {
                        var pair = collidePairs[i];
                        ProcessCollidePair(pair);
                    }
                }
                pushComp.ClearCollidePairs();
            }
            
        }
    }
}
