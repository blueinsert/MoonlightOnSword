using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仅仅是数据存储和查询组件
/// 控制各个Behavior之间行为的互斥
/// 避免产生Behavior之间产生网状耦合，最多只是星型耦合
/// </summary>
public class BehaviorFsmComp : ComponentBase
{
    public AIBehaviorMoveToPositionComp m_aiMoveToComp;
    public BehaviorMoveComp m_moveComp;
    public BehaviorSkillComp m_skillComp;
    public BehaviorBlockComp m_blockComp;
    public BehaviorGethitComp m_gethitComp;
    public EntityComp m_entity;

    private List<BehaviorCompBase> m_behaviorList = new List<BehaviorCompBase>();

    public void Start()
    {
        m_entity = GetComp<EntityComp>();
        m_moveComp = GetComp<BehaviorMoveComp>();
        m_aiMoveToComp = GetComp<AIBehaviorMoveToPositionComp>();
        m_skillComp = GetComp<BehaviorSkillComp>();
        m_blockComp = GetComp<BehaviorBlockComp>();
        m_gethitComp = GetComp<BehaviorGethitComp>();
        m_behaviorList.Add(m_aiMoveToComp);
    }
    
    private void OnBehaviorEnter(BehaviorCompBase behavior)
    {
        foreach(var b in m_behaviorList)
        {
            if (b != null && b != behavior)
            {
                b.OnOtherBehaviorEnter(behavior.BehaviorType);
            }
        }
    }

    public bool CanMove()
    {
        if (m_skillComp.IsPlaying)
            return false;
        if (m_blockComp.IsInBlocking)
            return false;
        if (m_gethitComp.IsPlaying)
            return false;
        return true;
    }

    public bool CanBlock()
    {
        if (m_skillComp.IsPlaying)
            return false;
        if (m_gethitComp.IsPlaying)
            return false;
        return true;
    }

    public bool IsInBlocking()
    {
        return m_blockComp!=null && m_blockComp.IsInBlocking;
    }

    public bool CanAttack()
    {
        if (m_skillComp.IsPlaying)
            return false;
        if (m_blockComp.IsInBlocking)
            return false;
        if (m_gethitComp.IsPlaying)
            return false;
        return true;
    }

    public void StartGetHit(EntityComp attacker, HitDef hitDef, bool isDead = false)
    {
        if (m_gethitComp == null)
            return;
        OnBehaviorEnter(m_gethitComp);
        m_gethitComp.StartGetHit(attacker, hitDef, false);
    }


    public void OnBlockHit(EntityComp attacker, HitDef hitdef)
    {
        if (m_blockComp == null)
            return;
        m_blockComp.OnBlockHit(attacker, hitdef);
    }
}
