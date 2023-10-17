using Flux;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CaredCompType(typeof(HitDetectionComp))]
public class HitDetectionSystem : SystemBase {

    //根据角度判断能否防御
    private bool CanBlock(EntityComp attacker, EntityComp p2, HitDef hitdef)
    {
        //计算相对角度
        var pos1 = attacker.gameObject.transform.position;
        var pos2 = p2.gameObject.transform.position;
        var move = p2.GetComp<MoveComp>();
        var facing = move.Facing;
        var dir = (pos1 - pos2);
        dir.y = 0;
        dir.Normalize();
        Vector2 dir2d = new Vector2(dir.x, dir.z);
        var angle = Vector2.SignedAngle(facing, dir2d);
        Debug.Log(string.Format("angle:{0}", angle));
        if(angle >= -80 && angle <= 80)
        {
            return true;
        }
        return false;
    }

    private void ProcessOnHit(EntityComp attacker, EntityComp p2, HitDef hitdef)
    {
        var leftHitCount = hitdef.GetLeftHitCount(p2);
        if (leftHitCount <= 0)
        {
            return; //hitdef已经对p2生效过
        }
        Debug.Log(string.Format("{0} hit {1}", attacker.gameObject.name, p2.gameObject.name));
        hitdef.RecordHit(p2);
        
        bool beblocking = false;
        var block = p2.GetComp<BehaviorBlockComp>();
        if (block != null)
        {
            //正在防御
            if (block.IsInBlocking)
            {
                //根据角度判断能否被防御
                var canBlock = CanBlock(attacker, p2, hitdef);
                if (canBlock) {
                    block.OnBlockHit(attacker, hitdef);
                    beblocking = true;
                }
                else
                {
                    block.Clear();
                }
            }
        }
        //todo damage
        var gethitComp = p2.GetComp<BehaviorGethitComp>();
        //被攻击者有BehaviorGethitComp才会有受创状态
        if (!beblocking && gethitComp != null)
        { 
            gethitComp.StartGetHit(attacker, hitdef, true);
        }
        var skill = attacker.GetComp<BehaviorSkillComp>();
        skill.OnHitTarget(hitdef, beblocking);
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
