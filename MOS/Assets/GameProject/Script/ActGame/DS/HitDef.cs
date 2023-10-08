using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HitDef {

    /// <summary>
    ///程度(轻:0 中:1 重:2）
    /// </summary>
    [SerializeField]
    public int Level;
    /// <summary>
    ///垂直方向力度分量(forward:0 up:1 down:2)
    /// </summary>
    [SerializeField]
    public int ForceVDir;
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
