﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandlerComp : ComponentBase
{
	public HitboxsDesc m_hitboxDesc = null;
	private Dictionary<string, ActorAttackCollider> m_attackColliderDic = new Dictionary<string, ActorAttackCollider>();

	public override void Init(ActorBase actor)
	{
		base.Init(actor);
		
	}

	public override void PostInit()
	{
		base.PostInit();
		m_hitboxDesc = GetComponentInChildren<HitboxsDesc>();
		var layerMask1 = LayerMask.NameToLayer("PlayerAttackCollider");
		var layerMask2 = LayerMask.NameToLayer("EnemyAttackCollider");

		foreach (var collider in m_hitboxDesc.m_attackBoxList)
		{
			collider.enabled = false;
			collider.gameObject.layer = m_owner.GetCampType() == CampType.Player ? layerMask1 : layerMask2;
			var attackCollider = collider.gameObject.AddComponent<ActorAttackCollider>();
			attackCollider.Init();
			attackCollider.SetOwner(this.m_owner);
			m_attackColliderDic.Add(collider.gameObject.name, attackCollider);
		}
	}

	public override void Tick()
	{
		base.Tick();
	}

	public void EnableAttackCollider(int id)
	{
		var hitDefCfg = ConfigDataManager.Instance.GetConfigDataHitEffectConfig(id);
		ActorAttackCollider attackCollider;
		m_attackColliderDic.TryGetValue(hitDefCfg.HitCollideName, out attackCollider);
		if (attackCollider != null)
		{
			attackCollider.SetHitDef(hitDefCfg);
			attackCollider.Enable();
		}
	}

	public void DisableAttackCollider(int id)
	{
		var hitDefCfg = ConfigDataManager.Instance.GetConfigDataHitEffectConfig(id);
		ActorAttackCollider attackCollider;
		m_attackColliderDic.TryGetValue(hitDefCfg.HitCollideName, out attackCollider);
		if (attackCollider != null)
		{
			attackCollider.Disable();
		}
	}

}
