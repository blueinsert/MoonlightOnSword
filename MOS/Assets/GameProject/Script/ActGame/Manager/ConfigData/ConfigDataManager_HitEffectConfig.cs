using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class HitEffectConfig:ConfigDataBase
{
    /// <summary>
    ///ID
    /// </summary>
    [Field]
    public int ID;
    /// <summary>
    ///说明
    /// </summary>
    [Field]
    public string Desc;
    /// <summary>
    ///碰撞体节点名
    /// </summary>
    [Field]
    public string HitCollideName;
    /// <summary>
    ///程度(轻:0 中:1 重:2）
    /// </summary>
    [Field]
    public int Level;
    /// <summary>
    ///垂直方向力度分量(forward:0 up:1 down:2)
    /// </summary>
    [Field]
    public int ForceVDir;
    /// <summary>
    ///p1打击停顿时间
    /// </summary>
    [Field]
    public int P1HitPauseTime;
    /// <summary>
    ///p2打击停顿时间
    /// </summary>
    [Field]
    public int P2HitPauseTime;
    /// <summary>
    ///打击后退速度(水平)
    /// </summary>
    [Field]
    public int HitBackHSpeed;
    /// <summary>
    ///打击后退速度(垂直)
    /// </summary>
    [Field]
    public int HitBackVSpeed;
    /// <summary>
    ///打击恢复时间
    /// </summary>
    [Field]
    public int HitRecoverTime;
}
public partial class ConfigDataManager : MonoBehaviour {
    public HitEffectConfig GetConfigDataHitEffectConfig(int id)
    {
        HitEffectConfig data = null;
        m_HitEffectConfigDic.TryGetValue(id, out data);
        return data;
    }

    public List<HitEffectConfig> GetAllConfigDataHitEffectConfig()
    {
        return new List<HitEffectConfig>(m_HitEffectConfigDic.Values);
    }

    private void InitHitEffectConfigDic(List<string[]> strGrid)
    {
        ParseConfigDataDic<HitEffectConfig>(strGrid, m_HitEffectConfigDic);
    }

    private Dictionary<int,HitEffectConfig> m_HitEffectConfigDic = new Dictionary<int,HitEffectConfig>();
}
