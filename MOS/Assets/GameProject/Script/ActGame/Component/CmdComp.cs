using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdComp : ComponentBase
{
	public bool m_isAttackCmdActive;
	public bool m_isWalkCmdActive;
	public bool m_isRunCmdActive;
	public Vector2 m_moveValue;
	public Vector2 m_rotateValue;
	public Vector3 m_cameraForward;
	
	public override void PostInit()
	{
		base.PostInit();
	}

	public void Clear()
	{
		m_moveValue = Vector2.zero;
		m_rotateValue = Vector2.zero;
		m_cameraForward = Vector3.forward;
		m_isWalkCmdActive = false;
		m_isRunCmdActive = false;
		m_isAttackCmdActive = false;
	}

	public bool IsCmdActive(string cmdName)
	{
		if (m_isAttackCmdActive)
			return true;
		return false;
	}

}
