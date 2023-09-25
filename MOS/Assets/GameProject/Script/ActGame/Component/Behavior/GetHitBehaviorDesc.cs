using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flux;

[RequireComponent(typeof(ResourceContainer))]
public class GetHitBehaviorDesc : MonoBehaviour {

	public const string ResourceName = "GetHit";

    public ResourceContainer m_resourceContainer = null;

    public FSequence m_sequence;

    public BehaviorConfig m_bahaviorCfg = null;

    public void Start()
    {
        m_resourceContainer = GetComponent<ResourceContainer>();
        m_resourceContainer.LoadAllResources();
        m_sequence = (m_resourceContainer.GetAsset(ResourceName) as GameObject).GetComponent<FSequence>();
        m_bahaviorCfg = m_sequence.ToBehaviorConfig();
    }
}
