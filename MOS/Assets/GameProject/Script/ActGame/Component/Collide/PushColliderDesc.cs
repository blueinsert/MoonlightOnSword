using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PushColliderTriggerInfo
{
    public int Frame;
    public PushColliderDesc Another;
}

public class PushColliderDesc : MonoBehaviour
{

    public Collider m_collider = null;

    public const int MaxNeighborCount = 3;

    public PushColliderTriggerInfo[] m_triggerInfos = new PushColliderTriggerInfo[MaxNeighborCount];
    public int m_triggersLen = 0;

    public void Enable()
    {
        m_collider.enabled = true;
    }

    public void Disable()
    {
        m_collider.enabled = false;
    }

    public void Init()
    {
        m_collider = this.gameObject.GetComponent<Collider>();
        for (int i = 0; i < m_triggerInfos.Length; i++)
        {
            m_triggerInfos[i] = new PushColliderTriggerInfo();
        }
    }

    //在物理更新之前调用
    public void ClearTriggersInfo()
    {
        m_triggersLen = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("PushColliderDesc:OnTriggerEnter {0}", other.name));
        if (m_triggersLen >= MaxNeighborCount)
            return;
        var another = other.GetComponent<PushColliderDesc>();
        if (another != null)
        {
            var info = m_triggerInfos[m_triggersLen++];
            info.Another = another;
            info.Frame = TimeManger.Instance.Frame;
        }
        
    }


    public PushColliderTriggerInfo GetTriggerInfo(int index)
    {
        return m_triggerInfos[index];
    }
}

