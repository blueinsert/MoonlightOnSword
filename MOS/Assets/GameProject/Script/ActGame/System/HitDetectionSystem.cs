using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CaredCompType(typeof(HitDetectionComp))]
public class HitDetectionSystem : SystemBase {

    private void TickHitDetection(HitDetectionComp comp)
    {
        Debug.Log("TickHitDetection");
        var colliderComp = comp.GetComp<ColliderComp>();
        if (comp.IsValid)
        {
            var triggerInfos = colliderComp.GetTriggerInfos();
            foreach (var triggerInfo in triggerInfos)
            {
                var gethitComp = triggerInfo.Collider.GetComponentInParent<BehaviorGethitComp>();
                if (gethitComp != null)
                {
                    Debug.Log(string.Format("{0} hit {1}", comp.gameObject.name, gethitComp.gameObject.name));
                    gethitComp.StartGetHit(comp.m_hitDef);
                }
            }
        }
    }

    public override void Tick()
    {

        foreach (var comp in m_compList)
        {
            if (comp.IsEnable && comp is HitDetectionComp)
            {
                TickHitDetection(comp as HitDetectionComp);
            }
        }

        foreach (var comp in m_compList)
        {
            var colliderComp = comp.GetComp<ColliderComp>();
            if (colliderComp != null)
            {
                colliderComp.ClearTriggerInfos();
            }
        }
        base.Tick();
    }
}
