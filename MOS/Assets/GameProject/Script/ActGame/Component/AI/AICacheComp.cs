using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICacheComp : ComponentBase {

	public EntityComp AttackTarget { get { return m_attackTarget; } }
    public EntityComp m_attackTarget = null;

    public void SetAttackTarget(EntityComp entityComp)
    {
        this.m_attackTarget = entityComp;
    }
}
