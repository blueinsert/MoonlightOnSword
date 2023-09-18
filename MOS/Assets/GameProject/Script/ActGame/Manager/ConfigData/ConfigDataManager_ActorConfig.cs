using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class ActorConfig:ConfigDataBase
{
    /// <summary>
    ///ID
    /// </summary>
    [Field]
    public int ID;
    /// <summary>
    ///描述
    /// </summary>
    [Field]
    public string Desc;
    /// <summary>
    ///模型
    /// </summary>
    [Field]
    public string Model;
    /// <summary>
    ///属性配置ID
    /// </summary>
    [Field]
    public int PropertyID;
}
public partial class ConfigDataManager : MonoBehaviour {
    public ActorConfig GetConfigDataActorConfig(int id)
    {
        ActorConfig data = null;
        m_ActorConfigDic.TryGetValue(id, out data);
        return data;
    }

    public List<ActorConfig> GetAllConfigDataActorConfig()
    {
        return new List<ActorConfig>(m_ActorConfigDic.Values);
    }

    private void InitActorConfigDic(List<string[]> strGrid)
    {
        ParseConfigDataDic<ActorConfig>(strGrid, m_ActorConfigDic);
    }

    private Dictionary<int,ActorConfig> m_ActorConfigDic = new Dictionary<int,ActorConfig>();
}
