using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEventSetHitdef : EventExecuteBase
{
	public ColliderHandlerComp m_hitCom;
	public int m_hitId;

	public override void OnStart()
	{
		base.OnStart();
		m_hitCom.EnableAttackCollider(m_hitId);
	}

	public override void OnEnd()
	{
		base.OnEnd();
		m_hitCom.DisableAttackCollider(m_hitId);
	}

	public void SetParams(int hitdefId)
	{
		m_hitId = hitdefId;
		m_hitCom = m_owner.GetComponent<ColliderHandlerComp>();
	}
}
