using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyComp : ComponentBase
{
	public float m_basicWalkSpeed;
	public float m_basicRunSpeed;
	public float m_basicTurnSpeed;

	public float m_walkSpeed;
	public float m_runSpeed;
	public float m_turnSpeed;

	public void SetParams(float basicWalkSpeed, float basicRunSpeed, float turnSpeed)
	{
		m_basicWalkSpeed = basicWalkSpeed;
		m_basicRunSpeed = basicRunSpeed;
		m_basicTurnSpeed = turnSpeed;
	}

	public override void Tick()
	{
		m_walkSpeed = m_basicWalkSpeed;
		m_runSpeed = m_basicRunSpeed;
		m_turnSpeed = m_basicTurnSpeed;
		base.Tick();
	}
}
