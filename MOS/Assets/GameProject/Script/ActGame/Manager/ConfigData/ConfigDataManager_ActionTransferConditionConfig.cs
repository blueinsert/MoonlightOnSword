using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class ActionTransferConditionConfig:ConfigDataBase
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
    ///类型
    /// </summary>
    [Field]
    public string Type;
    /// <summary>
    ///参数1
    /// </summary>
    [Field]
    public string Param1;
    /// <summary>
    ///参数2
    /// </summary>
    [Field]
    public string Param2;
    /// <summary>
    ///参数3
    /// </summary>
    [Field]
    public string Param3;
    /// <summary>
    ///参数4
    /// </summary>
    [Field]
    public string Param4;
    /// <summary>
    ///参数5
    /// </summary>
    [Field]
    public string Param5;
}
public partial class ConfigDataManager : MonoBehaviour {
    public ActionTransferConditionConfig GetConfigDataActionTransferConditionConfig(int id)
    {
        ActionTransferConditionConfig data = null;
        m_ActionTransferConditionConfigDic.TryGetValue(id, out data);
        return data;
    }

    public List<ActionTransferConditionConfig> GetAllConfigDataActionTransferConditionConfig()
    {
        return new List<ActionTransferConditionConfig>(m_ActionTransferConditionConfigDic.Values);
    }

    private void InitActionTransferConditionConfigDic(List<string[]> strGrid)
    {
        ParseConfigDataDic<ActionTransferConditionConfig>(strGrid, m_ActionTransferConditionConfigDic);
    }

    private Dictionary<int,ActionTransferConditionConfig> m_ActionTransferConditionConfigDic = new Dictionary<int,ActionTransferConditionConfig>();
}
