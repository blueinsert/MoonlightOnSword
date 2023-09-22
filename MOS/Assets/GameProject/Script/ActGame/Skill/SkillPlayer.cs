﻿using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillPlayer : IBasicAblitity {

	public bool IsEnd { get { return m_isEnd; } }
	public bool IsStart { get { return m_isStart; } }
    public int NextSkillId { get { return m_nextSkillId; } }

    public AnimComp m_animComp;
    public MoveComp m_moveComp;
	public InputComp m_inputComp;
	public BehaviorSkillComp m_skillComp;

    public SkillConfig m_skillConfig;

    /// <summary>
    /// eventType - exeEvtType
    /// </summary>
    private Dictionary<Type, Type> m_cacheExeTypeDic = new Dictionary<Type, Type>();

	private List<EventExecuteBase> m_events = new List<EventExecuteBase>();
	[SerializeField]
	public bool m_isStart = false;
    [SerializeField]
    public bool m_isEnd = false;
    [SerializeField]
    public float m_skillDuration = 0f;

    private int m_nextSkillId = 0;

    public void ClearNextSkill()
    {
        m_nextSkillId = 0;
    }

	/// <summary>
	/// basic comp
	/// </summary>
	/// <param name="anim"></param>
	/// <param name="move"></param>
	public void Initialize(AnimComp anim,MoveComp move,InputComp input,BehaviorSkillComp skill)
	{
		m_moveComp = move;
		m_animComp = anim;
		m_inputComp = input;
		m_skillComp = skill;
	}

	public void Setup(SkillConfig skillConfig)
	{
		m_skillConfig = skillConfig;
		InitEventExecuterListFromCfg();
		m_isStart = false;
		m_isEnd = false;
    }

    private void AddExecuteEvent(EventBase e, Type exeType)
    {
        var ee = Activator.CreateInstance(exeType) as EventExecuteBase;
        ee.Initialize(this);
        ee.Setup(e);
        m_events.Add(ee);

        m_skillDuration = Mathf.Max(m_skillDuration, ee.m_endTime);
    }

    private Type GetExeEvtType(Type evtType)
    {
        if (m_cacheExeTypeDic.ContainsKey(evtType))
        {
            return m_cacheExeTypeDic[evtType];
        }
        var attributes = evtType.GetCustomAttributes(typeof(ExecutableEventAttribute), true);
        ExecutableEventAttribute target = null;
        if (attributes.Length != 0)
        {
            target = attributes[0] as ExecutableEventAttribute;
        }
        if (target != null)
        {
            var exeType = target.ExecuteClassType;
            Debug.Log(string.Format("register evtType:{0} exeType:{1}", evtType.Name, exeType.Name));
            m_cacheExeTypeDic.Add(evtType, exeType);
            return exeType;
        }
        return null;
    }

	private void InitEventExecuterListFromCfg()
	{
		m_events.Clear();
        foreach(var evt in m_skillConfig.GetAllEvents())
        {
            var exeType = GetExeEvtType(evt.GetType());
            if (exeType != null)
            {
                AddExecuteEvent(evt, exeType);
            }
        }
	}

	public void Start()
	{
        Debug.Log("SkillPlayer:Start");
        m_isStart = true;
        m_isEnd = false;
	}

    public void Stop()
    {
        foreach (var ae in m_events)
        {
            ae.ForceEnd();
        }
        OnEnd();
        m_isEnd = true;
    }

    private void TickEvents(float deltaTime)
    {
		bool isAllEnd = true;
        foreach (var ae in m_events)
        {
            ae.Tick(deltaTime);
			if (!ae.IsEnd)
				isAllEnd = false;
        }
		if (!m_isEnd && isAllEnd)
		{
			OnEnd();
		}
		m_isEnd = isAllEnd;
    }

    public void Tick()
	{
		if (!m_isStart || m_isEnd)
			return;
		var deltaTime = TimeManger.Instance.DeltaTime;
		TickEvents(deltaTime);

    }

	private void Clear()
	{
		m_isStart = false;
		m_isEnd = false;
		m_skillConfig = null;
		m_skillDuration = 0;
		m_events.Clear();
		m_animComp = null;
		m_moveComp = null;
	}

	private void OnEnd()
	{
		Clear();

    }

    #region IBasicAbility
    public void PlayAnim(string name)
    {
		m_animComp.PlayAnim(name);
    }

    public Vector2 GetVel()
    {
        return m_moveComp.VelPreferHorizon;
    }

    public void SetVelH(Vector2 vel)
    {
        m_moveComp.SetPreferVelHorizon(vel.x, vel.y);
    }

    public void SetAnimSpeed(float speed)
    {
		m_animComp.SetSpeed(speed);
    }

    public bool IsAttackingClick()
    {
		return m_inputComp.IsAttackClick;
    }

    public void PlaySkill(int id)
    {
        m_nextSkillId = id;
    }

    public Vector2 GetFacing()
    {
        return m_moveComp.Facing;
    }

    public void SetVelV(float v)
    {
        m_moveComp.SetPreferVelVertical(v);
    }
    #endregion
}
