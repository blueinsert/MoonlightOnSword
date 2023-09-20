using Flux;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimEvent
{
    [SerializeField]
    public string Anim;
    /// <summary>
    /// 原始长度
    /// </summary>
    [SerializeField]
    public float OriginLen;
    [SerializeField]
    public float StartTime;
    [SerializeField]
    public float EndTime;
}

[Serializable]
public class VelSetEvent
{
    [SerializeField]
    public int Relative;//0:世界绝对值 1:相对角色朝向
    [SerializeField]
    public float VX;
    [SerializeField]
    public float VY;
    [SerializeField]
    public float VZ;
    [SerializeField]
    public float StartTime;
}

/// <summary>
/// 加速度设置
/// </summary>
[Serializable]
public class AcclerSetEvent
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
    [SerializeField]
    public float StartTime;
    [SerializeField]
    public float EndTime;
}

/// <summary>
/// 摩擦力设置
/// 摩擦力与速度方向相反
/// 大小与加速度含义相同
/// </summary>
[Serializable]
public class FrictionSetEvent
{
    [SerializeField]
    public float Value;
    [SerializeField]
    public float StartTime;
    [SerializeField]
    public float EndTime;
}

[Serializable]
public class SkillConfig {
    [SerializeField]
    public string Name;
    [SerializeField]
    public int ID;
	[SerializeField]
	public AnimEvent[] AnimEvents;
    [SerializeField]
    public FrictionSetEvent[] FrictionSetEvents;
}



