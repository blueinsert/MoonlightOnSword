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


    public bool FindAndCacheEnemy(float range)
    {
        return m_aiAblitity.FindAndCacheEnemy(range);
    }

    public bool IsEnemyValid()
    {
        return m_aiAblitity.IsEnemyValid();
    }

    public void AbandonEnemy()
    {
        m_aiAblitity.AbandonEnemy();
    }

    public float DistToEnemy()
    {
        return m_aiAblitity.DistToEnemy();
    }

    public bool MoveToEnemy(float speed)
    {
        return m_aiAblitity.MoveToEnemy(speed);
    }
    public bool CanMove()
    {
        return m_aiAblitity.CanMove();
    }

    public void StopMove()
    {
        m_aiAblitity.StopMove();
    }
}
