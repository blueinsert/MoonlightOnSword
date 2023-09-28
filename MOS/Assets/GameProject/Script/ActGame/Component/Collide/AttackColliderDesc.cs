using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderDesc : MonoBehaviour {

    public Collider m_collider = null;

    public Dictionary<Collider, ColliderTriggerInfo> m_triggerInfoDic = new Dictionary<Collider, ColliderTriggerInfo>();

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
    }

    //在物理更新之前调用
    public void ClearTriggersInfo()
    {
        m_triggerInfoDic.Clear();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("AttackCollider:OnTriggerEnter {0}", other.name));
        m_triggerInfoDic.Add(other, new ColliderTriggerInfo() {
            Collider = other,
            Frame = TimeManger.Instance.Frame
        });
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log(string.Format("AttackCollider:OnTriggerExit {0}", other.name));
        m_triggerInfoDic.Remove(other);
    }
}
