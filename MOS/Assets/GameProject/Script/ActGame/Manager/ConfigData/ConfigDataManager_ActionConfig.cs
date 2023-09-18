using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class ActionConfig:ConfigDataBase
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
    ///持续时间(ms)
    /// </summary>
    [Field]
    public float Duration;
    /// <summary>
    ///动画
    /// </summary>
    [Field]
    public string Anim;
    /// <summary>
    ///位移帧事件
    /// </summary>
    [Field]
    public string Displacements;
    /// <summary>
    ///运动公式
    /// </summary>
    [Field]
    public string Motions;
    /// <summary>
    ///打击定义
    /// </summary>
    [Field]
    public string HitDefs;
}
public partial class ConfigDataManager : MonoBehaviour {
    public ActionConfig GetConfigDataActionConfig(int id)
    {
        ActionConfig data = null;
        m_ActionConfigDic.TryGetValue(id, out data);
        return data;
    }

    public List<ActionConfig> GetAllConfigDataActionConfig()
    {
        return new List<ActionConfig>(m_ActionConfigDic.Values);
    }

    private void InitActionConfigDic(List<string[]> strGrid)
    {
        ParseConfigDataDic<ActionConfig>(strGrid, m_ActionConfigDic);
    }

    private Dictionary<int,ActionConfig> m_ActionConfigDic = new Dictionary<int,ActionConfig>();
}
