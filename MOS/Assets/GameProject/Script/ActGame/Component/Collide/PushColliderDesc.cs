using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PushColliderTriggerInfo
{
    public int Frame;
    public Collider Collider;
    public PushColliderDesc Another;
}

public class PushColliderDesc : MonoBehaviour
{

    public Collider m_collider = null;

    public const int MaxNeighborCount = 3;

    public HashSet<PushColliderTriggerInfo> m_triggerInfos = new HashSet<PushColliderTriggerInfo>();


    public void Init()
    {
        m_collider = this.gameObject.GetComponent<Collider>();
    }

    //在物理更新之前调用
    //public void ClearTriggersInfo()
    //{
    //    m_triggersLen = 0;
    //}

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("PushColliderDesc:OnTriggerEnter {0}", other.name));
        if (m_triggerInfos.Count >= MaxNeighborCount)
            return;
        var another = other.GetComponent<PushColliderDesc>();
        if (another != null)
        {
            m_triggerInfos.Add(new PushColliderTriggerInfo() {
                Another = another,
                Frame = TimeManger.Instance.Frame,
                Collider = other,
            });
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        m_triggerInfos.RemoveWhere((item) => { return item.Collider == other; });
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(string.Format("PushColliderDesc:OnCollisionEnter {0}", collision.collider.name));
    }

}

