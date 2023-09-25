using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutableEventAttribute : Attribute
{
    public Type ExecuteClassType;
    public ExecutableEventAttribute(Type executeClassType)
    {
        ExecuteClassType = executeClassType;
    }
}

[Serializable]
public class EventBase
{
    [SerializeField]
    public float StartTime;
    [SerializeField]
    public float EndTime;
}

[ExecutableEvent(typeof(AnimEventExecute))]
[Serializable]
public class AnimEvent : EventBase
{
    [SerializeField]
    public string Anim;
    /// <summary>
    /// 原始长度
    /// </summary>
    [SerializeField]
    public float OriginLen;

}

public enum VelRelativeType
{
    World,
    Local,
}

[ExecutableEvent(typeof(VelSetEventExecute))]
[Serializable]
public class VelSetEvent : EventBase
{
    [SerializeField]
    public VelRelativeType Relative;//0:世界绝对值 1:相对角色朝向
    [SerializeField]
    public float VX;
    [SerializeField]
    public float VY;
    [SerializeField]
    public float VZ;
}

/// <summary>
/// 加速度设置
/// </summary>
[Serializable]
public class AcclerSetEvent : EventBase
{
    [SerializeField]
    public int Relative;//0:世界绝对值 1:相对角色朝向
    [SerializeField]
    public float VXT;//速度目标值/最大值
    [SerializeField]
    public float VYT;
    [SerializeField]
    public float VZT;
    [SerializeField]
    public float AX;
    [SerializeField]
    public float AY;
    [SerializeField]
    public float AZ;
}

[Serializable]
public class TranslateCondition
{
    [SerializeField]
    public string Type;
    [SerializeField]
    public string Value;
}

/// <summary>
/// 状态转移设置
/// </summary>
[Serializable]
[ExecutableEvent(typeof(TranslationEventExecute))]
public class TranslationEvent : EventBase
{
    /// <summary>
    /// target skill id
    /// </summary>
    [SerializeField]
    public int To;
    [SerializeField]
    public List<TranslateCondition> ConditionList;
}

/// <summary>
/// 摩擦力设置
/// 摩擦力与速度方向相反
/// 大小与加速度含义相同
/// </summary>
[Serializable]
[ExecutableEvent(typeof(FrictionSetEventExecute))]
public class FrictionSetEvent : EventBase
{
    [SerializeField]
    public float Value;
}

[Serializable]
[ExecutableEvent(typeof(ColliderSetEventExecute))]
public class ColliderSetEvent : EventBase
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public bool IsEnable;
}

[Serializable]
[ExecutableEvent(typeof(HitDefSetEventExecute))]
public class HitDefSetEvent : EventBase
{
    [SerializeField]
    public HitDef HitDef;
}
