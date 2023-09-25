using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPlayer : BehaviorPlayer {

    public int NextSkillId { get { return m_nextSkillId; } }

    private int m_nextSkillId = 0;

    public SkillConfig m_skillConfig;

    public void ClearNextSkill()
    {
        m_nextSkillId = 0;
    }

    public void SetNextSkill(int id)
    {
        m_nextSkillId = id;
    }

    public override void Initialize(EntityComp entity)
    {
        //base.Initialize(entity);
        m_basicAblitity = new SkillPlayerBasicAblitityIml();
        m_basicAblitity.Initialize(entity);
        (m_basicAblitity as SkillPlayerBasicAblitityIml).SetSkillPlayer(this);
    }

    public void Setup(SkillConfig skillConfig)
    {
        m_skillConfig = skillConfig;
        InitEventExecuterListFromCfg();
        m_isStart = false;
        m_isEnd = false;
    }

    protected override void Clear()
    {
        m_skillConfig = null;
        base.Clear();
    }

    private void InitEventExecuterListFromCfg()
    {
        var allEvents = m_skillConfig.GetAllEvents();
        base.Setup(new List<EventBase>(allEvents));
    }
}
