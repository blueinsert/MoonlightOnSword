using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class ActionTransferConfig:ConfigDataBase
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
    ///开始action
    /// </summary>
    [Field]
    public int From;
    /// <summary>
    ///toAction
    /// </summary>
    [Field]
    public int To;
    /// <summary>
    ///条件1
    /// </summary>
    [Field]
    public int Condition1;
    /// <summary>
    ///条件2
    /// </summary>
    [Field]
    public int Condition2;
    /// <summary>
    ///条件3
    /// </summary>
    [Field]
    public int Condition3;
    /// <summary>
    ///条件4
    /// </summary>
    [Field]
    public int Condition4;
    /// <summary>
    ///条件5
    /// </summary>
    [Field]
    public int Condition5;
}
public partial class ConfigDataManager : MonoBehaviour {
    public ActionTransferConfig GetConfigDataActionTransferConfig(int id)
    {
        ActionTransferConfig data = null;
        m_ActionTransferConfigDic.TryGetValue(id, out data);
        return data;
    }

    public List<ActionTransferConfig> GetAllConfigDataActionTransferConfig()
    {
        return new List<ActionTransferConfig>(m_ActionTransferConfigDic.Values);
    }

    private void InitActionTransferConfigDic(List<string[]> strGrid)
    {
        ParseConfigDataDic<ActionTransferConfig>(strGrid, m_ActionTransferConfigDic);
    }

    private Dictionary<int,ActionTransferConfig> m_ActionTransferConfigDic = new Dictionary<int,ActionTransferConfig>();
}
