using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAblitity : IAIAblitity
{
    private EntityComp m_entityComp = null;
    private BehaviorSkillComp m_skillComp = null;
    private AIBehaviorMoveToPositionComp m_moveToComp = null;

    public void Initialize(EntityComp entity)
    {
        m_entityComp = entity;
        m_skillComp = entity.GetComp<BehaviorSkillComp>();
        m_moveToComp = entity.GetComp<AIBehaviorMoveToPositionComp>();
    }

    public bool Attack()
    {
        var res = m_skillComp.TryAttack();
        return res;
    }

    public bool IsNearTo(Vector3 position, float range)
    {
        var dist = (m_entityComp.transform.position - position).magnitude;
        return dist < range;
    }

    public void MoveTo(Vector3 targetPos)
    {
        m_moveToComp.MoveTo(targetPos);
    }
}
