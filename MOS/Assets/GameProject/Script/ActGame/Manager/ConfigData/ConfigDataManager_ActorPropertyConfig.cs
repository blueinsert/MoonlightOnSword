using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class ActorPropertyConfig:ConfigDataBase
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
    ///移动速度
    /// </summary>
    [Field]
    public float WalkSpeed;
    /// <summary>
    ///跑动速度
    /// </summary>
    [Field]
    public float RunSpeed;
    /// <summary>
    ///转身速度
    /// </summary>
    [Field]
    public float TurnSpeed;
}
public partial class ConfigDataManager : MonoBehaviour {
    public ActorPropertyConfig GetConfigDataActorPropertyConfig(int id)
    {
        ActorPropertyConfig data = null;
        m_ActorPropertyConfigDic.TryGetValue(id, out data);
        return data;
    }

    public List<ActorPropertyConfig> GetAllConfigDataActorPropertyConfig()
    {
        return new List<ActorPropertyConfig>(m_ActorPropertyConfigDic.Values);
    }

    private void InitActorPropertyConfigDic(List<string[]> strGrid)
    {
        ParseConfigDataDic<ActorPropertyConfig>(strGrid, m_ActorPropertyConfigDic);
    }

    private Dictionary<int,ActorPropertyConfig> m_ActorPropertyConfigDic = new Dictionary<int,ActorPropertyConfig>();
}
