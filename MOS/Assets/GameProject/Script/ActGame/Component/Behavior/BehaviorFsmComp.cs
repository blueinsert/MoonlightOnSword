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
    public BehaviorMoveComp m_moveComp;
    public BehaviorSkillComp m_skillComp;
    public BehaviorBlockComp m_blockComp;
    public BehaviorGethitComp m_gethitComp;

    public void Start()
    {
        m_moveComp = GetComp<BehaviorMoveComp>();
        m_skillComp = GetComp<BehaviorSkillComp>();
        m_blockComp = GetComp<BehaviorBlockComp>();
        m_gethitComp = GetComp<BehaviorGethitComp>();
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
        return m_blockComp.IsInBlocking;
    }
}
