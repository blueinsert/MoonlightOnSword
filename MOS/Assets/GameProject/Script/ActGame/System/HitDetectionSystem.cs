using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CaredCompType(typeof(HitDetectionComp))]
public class HitDetectionSystem : SystemBase {

    private void ProcessOnHit(EntityComp attacker, EntityComp p2, HitDef hitdef)
    {
        var leftHitCount = hitdef.GetLeftHitCount(p2);
        if (leftHitCount <= 0)
        {
            return; //hitdef已经对p2生效过
        }
        Debug.Log(string.Format("{0} hit {1}", attacker.gameObject.name, p2.gameObject.name));
        hitdef.RecordHit(p2);
        //todo damage
        var gethitComp = p2.GetComp<BehaviorGethitComp>();
        //被攻击者有BehaviorGethitComp才会有受创状态
        if (gethitComp != null)
        { 
            gethitComp.StartGetHit(attacker, hitdef);
            var skill = attacker.GetComp<BehaviorSkillComp>();
            skill.OnHitTarget(hitdef);
        }
    }

    private void TickHitDetection(HitDetectionComp comp)
    {
        //Debug.Log("TickHitDetection");
        var attacker = comp.GetComp<EntityComp>();
        var colliderComp = comp.GetComp<ColliderComp>();
        //1.hitdef有定义
        if (comp.IsValid)
        {
            var triggerInfos = colliderComp.GetTriggerInfos();
            foreach (var triggerInfo in triggerInfos)
            {
                //2.碰撞判定相交
                var p2 = triggerInfo.Collider.GetComponentInParent<EntityComp>();
                ProcessOnHit(attacker, p2, comp.m_hitDef);
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
                //colliderComp.ClearTriggerInfos();
            }
        }
        base.Tick();
    }
}
