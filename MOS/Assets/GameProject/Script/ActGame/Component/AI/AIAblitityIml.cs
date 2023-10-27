using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AIAblitity : IAIAblitity
{
    private EntityComp m_entityComp = null;
    private BehaviorSkillComp m_skillComp = null;
    private AIBehaviorMoveToPositionComp m_moveToComp = null;
    private AICacheComp m_cacheComp = null;

    public void Initialize(EntityComp entity)
    {
        m_entityComp = entity;
        m_skillComp = entity.GetComp<BehaviorSkillComp>();
        m_moveToComp = entity.GetComp<AIBehaviorMoveToPositionComp>();
        m_cacheComp = entity.GetComp<AICacheComp>();
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

    public void MoveTo(Vector3 targetPos, float speed)
    {
        m_moveToComp.MoveTo(targetPos, speed);
    }

    public bool IsMoving()
    {
        return m_moveToComp.IsMoving;
    }

    public bool FindAndCacheEnemy(float range)
    {
        var target = SearchSystem.Instance.FindNearestEnemyInRange(m_entityComp, range);

        if(target != null)
        {
            m_cacheComp.SetAttackTarget(target);
            return true;
        }
        return false;
    }

    public bool IsEnemyValid()
    {
        if (m_cacheComp.m_attackTarget == null)
            return false;
        return true;
    }

    public void AbandonEnemy()
    {
        m_cacheComp.m_attackTarget = null;
    }

    public float DistToEnemy()
    {
        if (m_cacheComp.AttackTarget == null)
            return 0f;
        return (m_cacheComp.AttackTarget.transform.position - this.m_entityComp.transform.position).magnitude;
    }

    public bool MoveToEnemy(float speed)
    {
        if (m_cacheComp.AttackTarget == null)
            return false;
        var targetPos = m_cacheComp.AttackTarget.transform.position;
        MoveTo(targetPos, speed);
        return true;
    }

    public bool CanMove()
    {
        return true;
    }
}
