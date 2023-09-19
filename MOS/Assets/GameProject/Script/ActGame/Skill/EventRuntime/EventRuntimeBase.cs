using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventRuntimeBase  {

	public bool IsEnd { get { return m_isEnd; } }

	public float m_time = 0;
	public float m_startTime;
	public float m_endTime;
	public bool m_isStart = false;
	public bool m_isEnd = false;
	public ActorBase m_owner = null;

	protected IBasicAblitity m_basicAblity = null;

	public float EndTime { 
		get {
			return m_endTime;
		} 
	}

    public void Initialize(IBasicAblitity basicAblity)
    {
		m_basicAblity = basicAblity;
    }

    [Obsolete]
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

    protected virtual void OnTick(float deltaTime)
    {

    }

	public void Tick(float deltaTime)
	{
		m_time += deltaTime;
        if (m_time > m_startTime)
		{
			if (!m_isStart)
			{
				OnStart();
				m_isStart = true;
			}
		}
		if(m_time > EndTime)
		{
			if (!m_isEnd)
			{
				OnEnd();
				m_isEnd = true;
			}
		}
        if(m_isStart && !m_isEnd)
        {
            OnTick(deltaTime);
        }
	}

	public virtual void OnStart() { }

	public virtual void OnEnd() { }

	
}
