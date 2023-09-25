using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEventSetMotion : EventExecuteBase
{
	public MotionComp m_motionComp;
	public int m_motionId;

	public override void OnStart()
	{
		base.OnStart();
		var motionCfg = ConfigDataManager.Instance.GetConfigDataMotionConfig(m_motionId);
		m_motionComp.SetMotion(motionCfg);
		m_motionComp.m_isEnable = true;
	}

	public override void OnEnd()
	{
		base.OnEnd();
		m_motionComp.m_isEnable = false;
	}

	public void SetParams(int motionId)
	{
		m_motionId = motionId;
		m_motionComp = m_owner.GetComponent<MotionComp>();
	}
}
