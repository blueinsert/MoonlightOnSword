using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PushCollidePair
{
    public PushComp P1;
    public PushComp P2;
}


public class PushComp : ComponentBase
{
    CollidersDesc m_collidersDesc = null;
    public PushColliderDesc m_pushColliderDesc;
    public float m_radius = 1f;

    public List<PushCollidePair> m_cacheCollidePair = new List<PushCollidePair>(PushColliderDesc.MaxNeighborCount);

    public void Start()
    {
        m_collidersDesc = this.GetComponentInChildren<CollidersDesc>();
        if (m_collidersDesc.m_pushCollider == null)
        {
            m_isEnable = false;
            return;
        }
        var pushCollider = m_collidersDesc.m_pushCollider;
        m_radius = pushCollider.radius;

        var layerMask = LayerMask.NameToLayer("PushCollider");

        pushCollider.gameObject.layer = layerMask;

        m_pushColliderDesc = pushCollider.gameObject.AddComponent<PushColliderDesc>();
        m_pushColliderDesc.Init();

        for(int i=0;i< PushColliderDesc.MaxNeighborCount; i++)
        {
            m_cacheCollidePair.Add(new PushCollidePair());
        }
    }

    public bool IsCollideWithOthers()
    {
        return m_pushColliderDesc.m_triggerInfos.Count != 0;
    }

    public List<PushCollidePair> GetCollidePairs(out int len)
    {
        int i = 0;
        foreach (var triggerInfo in m_pushColliderDesc.m_triggerInfos)
        {
            var p2 = triggerInfo.Another.GetComponentInParent<PushComp>();
            if (p2 != null && p2.IsEnable)
            {
                m_cacheCollidePair[i].P1 = this;
                m_cacheCollidePair[i].P2 = p2;
                i++;
            }
        }
        len = i;
        return m_cacheCollidePair;
    }
}

