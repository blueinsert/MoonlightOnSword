using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionEventBase  {
	public float m_startTime;
	public float m_endTime;
	public bool m_isStart = false;
	public bool m_isEnd = false;
	public ActorBase m_owner = null;

	public float EndTime { 
		get {
			return m_endTime;
		} 
	}

	public void SetOwner(ActorBase actor)
	{
		m_owner = actor;
	}

	public void SetStartTime(float startTime)
	{
		m_startTime = startTime;
	}

	public void SetEndTime(float endTime)
	{
		m_endTime = endTime;
	}

	public void ForceEnd()
	{
		if (!m_isEnd)
		{
			m_isEnd = true;
			OnEnd();
		}
	}

	public virtual void Tick(float actionTime)
	{
		if (actionTime > m_startTime)
		{
			if (!m_isStart)
			{
				OnStart();
				m_isStart = true;
			}
		}
		if(actionTime > EndTime)
		{
			if (!m_isEnd)
			{
				OnEnd();
				m_isEnd = true;
			}
		}
	}

	public virtual void OnStart() { }

	public virtual void OnEnd() { }

	
}
