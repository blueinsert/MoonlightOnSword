using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviorType
{
    Moving,
    Skill,
    GetHit,
    Guard,
}

public abstract class BehaviorCompBase : ComponentBase {

    public BehaviorType BehaviorType { get { return m_behaviorType; } }
    public BehaviorType m_behaviorType;

    public BehaviorCompBase(BehaviorType type)
    {
        this.m_behaviorType = type;
    }

    public abstract bool IsBehaviorActive();

    public abstract void OnOtherBehaviorEnter(BehaviorType other);
}
