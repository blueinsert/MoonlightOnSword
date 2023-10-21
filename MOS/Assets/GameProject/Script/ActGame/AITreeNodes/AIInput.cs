using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.AIBehaviorTree;

public class AIInput : BInput, IAIAblitity {

    private AIAblitity m_aiAblitity = null;

    public void Initialize(EntityComp entity)
    {
        m_aiAblitity = new AIAblitity();
        m_aiAblitity.Initialize(entity);
    }

    public bool Attack()
    {
        return m_aiAblitity.Attack();
    }

    public bool IsNearTo(Vector3 position, float range)
    {
        return m_aiAblitity.IsNearTo(position, range);
    }

    public void MoveTo(Vector3 targetPos, float speed)
    {
        m_aiAblitity.MoveTo(targetPos, speed);
    }

    public bool IsMoving()
    {
        return m_aiAblitity.IsMoving();
    }

    public bool CanMove()
    {
        return m_aiAblitity.CanMove();
    }
}
