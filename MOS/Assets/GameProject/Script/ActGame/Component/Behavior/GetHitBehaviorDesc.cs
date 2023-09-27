using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flux;

[RequireComponent(typeof(ResourceContainer))]
public class GetHitBehaviorDesc : MonoBehaviour {

    public ResourceContainer m_resourceContainer = null;

    public Dictionary<string, BehaviorConfig> m_behaviorConfigDic = new Dictionary<string, BehaviorConfig>();

    public void Start()
    {
        m_resourceContainer = GetComponent<ResourceContainer>();
        InitializeBehaviorsFromResource();
    }

    private void InitializeBehaviorsFromResource()
    {
        m_resourceContainer.LoadAllResources();
        foreach (var item in m_resourceContainer.AssetList)
        {
            var go = item.RuntimeAssetCache as GameObject;
            if (go != null)
            {
                var sequence = go.GetComponent<FSequence>();
                var bahaviorCfg = sequence.ToBehaviorConfig();
                m_behaviorConfigDic.Add(item.Name, bahaviorCfg);
            }
        }
    }

    public BehaviorConfig GetBehaviorCfg(string name)
    {
        if (m_behaviorConfigDic.ContainsKey(name))
        {
            return m_behaviorConfigDic[name];
        }
        return null;
    }
}
