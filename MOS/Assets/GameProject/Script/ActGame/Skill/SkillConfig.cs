﻿using Flux;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

//todo
public class ColliderSetEvent : EventBase
{

}

//todo
public class HitDefSetEvent : EventBase
{

}

[Serializable]
public class SkillConfig {
    [SerializeField]
    public string Name;
    [SerializeField]
    public int ID;

    //[ExecutableEventList(typeof(AnimEventExecute))]
	[SerializeField]
	public List<AnimEvent> AnimEvents = new List<AnimEvent>();

    //[ExecutableEventList(typeof(AnimEventExecute))]
    [SerializeField]
    public List<FrictionSetEvent> FrictionSetEvents = new List<FrictionSetEvent>();

    //[ExecutableEventList(typeof(AnimEventExecute))]
    [SerializeField]
    public List<TranslationEvent> TranslationEvents = new List<TranslationEvent>();

    [SerializeField]
    public List<VelSetEvent> VelSetEvents = new List<VelSetEvent>();

    public IEnumerable<EventBase> GetAllEvents()
    {
        List<EventBase> events = new List<EventBase>();
        foreach (var ae in AnimEvents) events.Add(ae);
        foreach (var ae in FrictionSetEvents) events.Add(ae);
        foreach (var ae in TranslationEvents) events.Add(ae);
        foreach (var ae in VelSetEvents) events.Add(ae);
        for (int i = 0; i < events.Count; i++)
        {
            yield return events[i];
        }
    }

    public void AddEvent(EventBase e)
    {
        if(e is AnimEvent)
        {
            AnimEvents.Add(e as AnimEvent);
        }
        if(e is FrictionSetEvent)
        {
            FrictionSetEvents.Add(e as FrictionSetEvent);
        }
        if((e is TranslationEvent))
        {
            TranslationEvents.Add(e as TranslationEvent);
        }
        if(e is VelSetEvent)
        {
            VelSetEvents.Add(e as VelSetEvent);
        }
    }
}



