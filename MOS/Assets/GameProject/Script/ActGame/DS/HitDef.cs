using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 打击力度
/// </summary>
public enum HitForceLevel
{
    Light = 0,
    Heavy,
}

//打击部位类型
public enum HitPosType
{
    Low,
    High,
}

public enum HitType
{
    NormalAttack,

}

[Serializable]
public class HitDef {

    /// <summary>
    ///程度(轻:0 中:1 重:2）
    /// </summary>
    [SerializeField]
    public HitForceLevel Level;
    [SerializeField]
    public HitPosType PosType;
    /// <summary>
    ///p1打击停顿时间
    /// </summary>
    [SerializeField]
    public int P1HitPauseTime;
    /// <summary>
    ///p2打击停顿时间
    /// </summary>
    [SerializeField]
    public int P2HitPauseTime;
    [SerializeField]
    public int P2HitRecoverTime;
    /// <summary>
    ///打击后退速度(水平)
    /// </summary>
    [SerializeField]
    public int HitBackHSpeed;
    /// <summary>
    ///打击后退速度(垂直)
    /// </summary>
    [SerializeField]
    public int HitBackVSpeed;
    /// <summary>
    ///p1打击停顿时间
    /// </summary>
    [SerializeField]
    public int P1GuardPauseTime;
    /// <summary>
    ///p2打击停顿时间
    /// </summary>
    [SerializeField]
    public int P2GuardPauseTime;

    [SerializeField]
    public int P2GuardRecoverTime;

    [SerializeField]
    public int GuardBackHSpeed;

    /// <summary>
    /// 打击次数
    /// </summary>
    //[SerializeField]
    //public int HitCount = 1;
    /// <summary>
    /// 打击间隔(帧)
    /// </summary>
    //[SerializeField]
    //public int HitPeriod = 1;

    private int m_maxHitCount = 1;

    private Dictionary<EntityComp, int> m_hitRecorderDic = new Dictionary<EntityComp, int>();

    public void RecordHit(EntityComp c)
    {
        if (!m_hitRecorderDic.ContainsKey(c))
        {
            m_hitRecorderDic.Add(c, 1);
        }
        else
        {
            m_hitRecorderDic[c] = m_hitRecorderDic[c] + 1;
        }
            
    }

    public int GetLeftHitCount(EntityComp c)
    {
        int hitCount = 0;
        if (m_hitRecorderDic.ContainsKey(c))
        {
            hitCount = m_hitRecorderDic[c];
        }
        var left = m_maxHitCount - hitCount;
        left = Mathf.Clamp(left, 0, left);
        return left;
    }

    public void ClearCache()
    {
        m_hitRecorderDic.Clear();
    }
}
