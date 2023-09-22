using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseColliderDesc : MonoBehaviour
{

    public Collider m_collider = null;

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

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("DefenseColliderDesc:OnTriggerEnter {0}", other.name));
    }
}
