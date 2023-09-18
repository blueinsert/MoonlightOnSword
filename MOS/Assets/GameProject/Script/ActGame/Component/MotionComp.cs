using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionComp : ComponentBase
{
	public MotionConfig m_motionConfig;
	public MoveComp m_moveComp;
	public bool m_isEnable;
	public override void Init(ActorBase actor)
	{
		base.Init(actor);
	}

	public override void PostInit()
	{
		base.PostInit();
		m_moveComp = m_owner.GetComponent<MoveComp>();
	}

	public void SetMotion(MotionConfig motion)
	{
		m_motionConfig = motion;
	}

	private void TickMotion()
	{
		var forward = m_owner.transform.forward;

		if (m_motionConfig.DirSpeedH == 0)
		{

		}else if (m_motionConfig.DirSpeedH == 1)
		{

		}
		else if (m_motionConfig.DirSpeedH == 2)
		{

		}
		else if (m_motionConfig.DirSpeedH == 3)
		{

		}
		var velH = forward * m_motionConfig.SpeedH;
		var velV = m_motionConfig.SpeedV;
		m_moveComp.SetPreferVelHorizon(velH.x, velH.z);
		m_moveComp.SetPreferVelVertical(m_motionConfig.SpeedV);
	}

	public override void Tick()
	{
		base.Tick();
		if (m_isEnable)
		{
			TickMotion();
		}
	}

}
