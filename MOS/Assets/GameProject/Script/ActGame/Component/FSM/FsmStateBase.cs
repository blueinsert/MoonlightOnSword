using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
	None,
	Idle,
	Walk,
	Run,
	Action,
	GetHit,
}


public abstract class FsmStateBase {
	[SerializeField]
	public ActorBase m_owner;
	[SerializeField]
	public StateType m_type;

	protected MotionComp m_motionComp;
	protected MoveComp m_moveComp;
	protected AnimComp m_animComp;
	protected CmdComp m_cmdComp;
	protected PropertyComp m_propertyComp;

	public event Action EventOnEnd;

	public FsmStateBase(ActorBase owner,StateType type)
	{
		m_owner = owner;
		m_type = type;
	}

	public virtual void Init()
	{
		m_moveComp = m_owner.GetComponent<MoveComp>();
		m_animComp = m_owner.GetComponent<AnimComp>();
		m_cmdComp = m_owner.GetComponent<CmdComp>();
		m_propertyComp = m_owner.GetComponent<PropertyComp>();
		m_motionComp = m_owner.GetComponent<MotionComp>();
	}

	public virtual void OnEnter()
	{
		Debug.Log(string.Format("State {0} OnEnter", m_type));
	}

	public virtual void Tick()
	{

	}

	public virtual void OnExit()
	{
		Debug.Log(string.Format("State {0} OnExit", m_type));
	}

	protected void OnEnd() {
		if (EventOnEnd != null)
		{
			EventOnEnd();
		}
	}
}
