using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceContainer))]
public class SkillSequenceResourceContainerEditor : Editor {

    void OnEnable()
    {
        m_prefabResourceContainer = target as ResourceContainer;
        RecordAssetList();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed)
        {
            // 如果列表发生变化，则重新生成资源路径，并记录资源
            if (IsAssetListChanged())
            {
                GenerateAssetPath();

                RecordAssetList();
            }
        }
    }

    /// <summary>
    /// 生成资源的路径
    /// </summary>
    protected void GenerateAssetPath()
    {
        foreach (var assetCache in m_prefabResourceContainer.AssetList)
        {
            if (assetCache.Asset != null)
            {
                assetCache.AssetPath = AssetDatabase.GetAssetPath(assetCache.Asset);
            }
        }
    }

    /// <summary>
    /// 记录asset对象列表中的资源对象
    /// </summary>
    protected void RecordAssetList()
    {
        m_assetList = new List<UnityEngine.Object>();
        foreach (var assetCache in m_prefabResourceContainer.AssetList)
        {
            m_assetList.Add(assetCache.Asset);
        }
    }

    /// <summary>
    /// 资源列表内的对象是否有变化
    /// </summary>
    /// <returns></returns>
    protected bool IsAssetListChanged()
    {
        if (m_assetList.Count != m_prefabResourceContainer.AssetList.Count)
        {
            return true;
        }

        for (int i = 0; i < m_assetList.Count; ++i)
        {
            if (m_assetList[i] != m_prefabResourceContainer.AssetList[i].Asset)
            {
                return true;
            }
        }

        return false;
    }

    private ResourceContainer m_prefabResourceContainer;
    private List<UnityEngine.Object> m_assetList;
}
