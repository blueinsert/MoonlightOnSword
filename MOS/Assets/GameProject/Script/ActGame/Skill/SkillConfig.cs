using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimEvent
{
    [SerializeField]
    public string Anim;
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

[Serializable]
public class SkillsDesc
{
    [SerializeField]
    public SkillConfig[] Skills;

    private Dictionary<int, SkillConfig> m_skillDic = null;

    public SkillConfig GetSkillConfig(int id)
    {
        if (m_skillDic == null)
        {
            m_skillDic = new Dictionary<int, SkillConfig>();
            if (Skills != null)
            {
                foreach(var skill in Skills)
                {
                    m_skillDic.Add(skill.ID, skill);
                }
            }
        }
        if (m_skillDic.ContainsKey(id))
        {
            return m_skillDic[id];
        }
        return null;
    }
}
