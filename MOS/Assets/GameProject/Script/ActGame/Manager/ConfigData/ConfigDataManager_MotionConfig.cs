using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class MotionConfig:ConfigDataBase
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
    ///水平速度方向(前:0后:1左:2右:3)
    /// </summary>
    [Field]
    public int DirSpeedH;
    /// <summary>
    ///水平速度
    /// </summary>
    [Field]
    public float SpeedH;
    /// <summary>
    ///垂直速度
    /// </summary>
    [Field]
    public float SpeedV;
}
public partial class ConfigDataManager : MonoBehaviour {
    public MotionConfig GetConfigDataMotionConfig(int id)
    {
        MotionConfig data = null;
        m_MotionConfigDic.TryGetValue(id, out data);
        return data;
    }

    public List<MotionConfig> GetAllConfigDataMotionConfig()
    {
        return new List<MotionConfig>(m_MotionConfigDic.Values);
    }

    private void InitMotionConfigDic(List<string[]> strGrid)
    {
        ParseConfigDataDic<MotionConfig>(strGrid, m_MotionConfigDic);
    }

    private Dictionary<int,MotionConfig> m_MotionConfigDic = new Dictionary<int,MotionConfig>();
}
