using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderDesc : MonoBehaviour {

    public Collider m_collider = null;

    public ColliderTriggerInfo[] m_triggerInfos = new ColliderTriggerInfo[10];
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
        for(int i=0;i< m_triggerInfos.Length; i++)
        {
            m_triggerInfos[i] = new ColliderTriggerInfo();
        }
    }

    //在物理更新之前调用
    public void ClearTriggersInfo()
    {
        m_triggersLen = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("AttackCollider:OnTriggerEnter {0}", other.name));
        if (m_triggersLen >= 10)
            return;
        var info = m_triggerInfos[m_triggersLen++];
        info.Collider = other;
        info.Frame = TimeManger.Instance.Frame;
    }
}
