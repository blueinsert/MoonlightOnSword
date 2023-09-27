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
    public CharacterController m_cc;
    public PushColliderDesc m_pushColliderDesc;
    public float m_radius = 1f;

    public List<PushCollidePair> m_cacheCollidePair = new List<PushCollidePair>(PushColliderDesc.MaxNeighborCount);

    public void Start()
    {
        m_cc = this.GetComponentInChildren<CharacterController>();
        m_radius = m_cc.radius;

        var layerMask5 = LayerMask.NameToLayer("OccupyCollider");

        m_cc.gameObject.layer = layerMask5;

        m_pushColliderDesc = m_cc.gameObject.AddComponent<PushColliderDesc>();
        m_pushColliderDesc.Init();

        for(int i=0;i< PushColliderDesc.MaxNeighborCount; i++)
        {
            m_cacheCollidePair.Add(new PushCollidePair());
        }
    }

    public bool IsCollideWithOthers()
    {
        return m_pushColliderDesc.m_triggersLen != 0;
    }

    public List<PushCollidePair> GetCollidePairs(out int len)
    {
        int count = 0;
        for(int i = 0; i < m_pushColliderDesc.m_triggersLen; i++)
        {
            m_cacheCollidePair[i].P1 = this;
            var p2 = m_pushColliderDesc.GetTriggerInfo(i).Another.GetComponent<PushComp>();
            if(p2 != null && p2.IsEnable)
            {
                m_cacheCollidePair[i].P2 = p2;
                count++;
            }
            
        }
        len = count;
        return m_cacheCollidePair;
    }
}

