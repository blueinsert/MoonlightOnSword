using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAttackCollider : MonoBehaviour {

	public ActorBase m_owner = null;
    public Collider m_collider = null;
    public HitEffectConfig m_hitDef = null;

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
        m_collider = this.gameObject.GetComponent<BoxCollider>();
    }

    public void SetOwner(ActorBase actor)
    {
        m_owner = actor;
    }

    public HitEffectConfig GetHitDef()
    {
        return m_hitDef;
    }

    public void SetHitDef(HitEffectConfig hitDef)
    {
        m_hitDef = hitDef;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("ActorAttackCollider:OnTriggerEnter {0}", other.name));
        var hiter = other.GetComponent<ActorBase>();
        var attacker = this.m_owner;
        var hitDef = GetHitDef();
        if (attacker != null && hiter != null && hitDef != null)
        {
            ActGame.Instance.OnHitTarget(attacker, hiter, hitDef);
        }
    }
}
