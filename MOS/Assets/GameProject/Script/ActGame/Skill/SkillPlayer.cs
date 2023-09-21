using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillPlayer : IBasicAblitity {

	public bool IsEnd { get { return m_isEnd; } }
	public bool IsStart { get { return m_isStart; } }

    public AnimComp m_animComp;
    public MoveComp m_moveComp;
	public InputComp m_inputComp;
	public BehaviorSkillComp m_skillComp;

    public SkillConfig m_skillConfig;

	private List<EventRuntimeBase> m_events = new List<EventRuntimeBase>();
	[SerializeField]
	public bool m_isStart = false;
    [SerializeField]
    public bool m_isEnd = false;
    [SerializeField]
    public float m_skillDuration = 0f;

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

	private void InitEventExecuterListFromCfg()
	{
		m_events.Clear();
		float maxTime = -1;
        if (m_skillConfig.AnimEvents!=null && m_skillConfig.AnimEvents.Length != 0)
		{
			foreach(var e in m_skillConfig.AnimEvents)
			{
				AnimEventExecute ae = new AnimEventExecute();
				ae.Initialize(this);
				ae.Setup(e);
				m_events.Add(ae);
				if(ae.EndTime > maxTime)
				{
					maxTime = ae.EndTime;
				}
            }
		}
        if (m_skillConfig.FrictionSetEvents != null && m_skillConfig.FrictionSetEvents.Length != 0)
        {
            foreach (var e in m_skillConfig.FrictionSetEvents)
            {
                FrictionSetEventExecute ae = new FrictionSetEventExecute();
                ae.Initialize(this);
                ae.Setup(e);
                m_events.Add(ae);
                if (ae.EndTime > maxTime)
                {
                    maxTime = ae.EndTime;
                }
            }
        }
        if (m_skillConfig.TranslationEvents != null && m_skillConfig.TranslationEvents.Length != 0)
        {
            foreach (var e in m_skillConfig.TranslationEvents)
            {
                TranslationEventExecute ae = new TranslationEventExecute();
                ae.Initialize(this);
                ae.Setup(e);
                m_events.Add(ae);
                if (ae.EndTime > maxTime)
                {
                    maxTime = ae.EndTime;
                }
            }
        }
        m_skillDuration = maxTime;
	}

	public void Start()
	{
        Debug.Log("SkillPlayer:Start");
        m_isStart = true;
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

    public void SetVel(Vector2 vel)
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
		var from = m_skillConfig != null ? m_skillConfig.ID : -1;
		m_skillComp.TryPlaySkill(id, from);
    }
    #endregion
}
