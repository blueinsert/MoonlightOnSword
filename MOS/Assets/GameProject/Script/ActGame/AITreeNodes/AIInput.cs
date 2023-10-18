using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.AIBehaviorTree;

public class AIInput : BInput,IAIAblitity {

    private AIAblitity m_aiAblitity = null;

    public bool Attack()
    {
        return m_aiAblitity.Attack();
    }

    public void Initialize(EntityComp entity)
    {
        m_aiAblitity = new AIAblitity();
        m_aiAblitity.Initialize(entity);
    }
}
