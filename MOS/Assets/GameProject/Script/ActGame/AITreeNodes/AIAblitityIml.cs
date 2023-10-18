using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAblitity : IAIAblitity
{
    private BehaviorSkillComp m_skillComp = null;

    public void Initialize(EntityComp entity)
    {
        m_skillComp = entity.GetComp<BehaviorSkillComp>();
    }

    public bool Attack()
    {
        var res = m_skillComp.TryAttack();
        return res;
    }
}
