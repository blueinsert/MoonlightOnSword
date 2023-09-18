using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class CameraConfig:ConfigDataBase
{
    /// <summary>
    ///ID
    /// </summary>
    [Field]
    public int ID;
    /// <summary>
    ///旋转灵敏度X
    /// </summary>
    [Field]
    public float RotateXRate;
    /// <summary>
    ///旋转灵敏度Y
    /// </summary>
    [Field]
    public float RotateYRate;
    /// <summary>
    ///缩放灵敏度
    /// </summary>
    [Field]
    public float ZoomRate;
    /// <summary>
    ///初始距离
    /// </summary>
    [Field]
    public float InitDist;
    /// <summary>
    ///最小距离
    /// </summary>
    [Field]
    public float MinDist;
    /// <summary>
    ///最大距离
    /// </summary>
    [Field]
    public float MaxDist;
    /// <summary>
    ///X轴最小旋转角
    /// </summary>
    [Field]
    public float MinXRotate;
    /// <summary>
    ///X轴最大旋转角
    /// </summary>
    [Field]
    public float MaxXRotate;
    /// <summary>
    ///角色头部高度
    /// </summary>
    [Field]
    public float HeadOffsetY;
}
public partial class ConfigDataManager : MonoBehaviour {
    public CameraConfig GetConfigDataCameraConfig(int id)
    {
        CameraConfig data = null;
        m_CameraConfigDic.TryGetValue(id, out data);
        return data;
    }

    public List<CameraConfig> GetAllConfigDataCameraConfig()
    {
        return new List<CameraConfig>(m_CameraConfigDic.Values);
    }

    private void InitCameraConfigDic(List<string[]> strGrid)
    {
        ParseConfigDataDic<CameraConfig>(strGrid, m_CameraConfigDic);
    }

    private Dictionary<int,CameraConfig> m_CameraConfigDic = new Dictionary<int,CameraConfig>();
}
