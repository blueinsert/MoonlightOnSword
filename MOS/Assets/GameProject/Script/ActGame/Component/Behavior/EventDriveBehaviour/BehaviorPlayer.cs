using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorPlayer
{
    public IBasicAblitity BaiscAblitity { get { return m_basicAblitity; } }
    public bool IsPlaying { get { return m_isStart && !m_isEnd; } }
    public bool IsEnd { get { return m_isEnd; } }
    public bool IsStart { get { return m_isStart; } }

    protected IBasicAblitity m_basicAblitity;


    /// <summary>
    /// eventType - exeEvtType
    /// </summary>
    protected Dictionary<Type, Type> m_cacheExeTypeDic = new Dictionary<Type, Type>();

    protected List<EventExecuteBase> m_events = new List<EventExecuteBase>();
    [SerializeField]
    public bool m_isStart = false;
    [SerializeField]
    public bool m_isEnd = false;
    [SerializeField]
    public float m_duration = 0f;


    public virtual void Initialize(EntityComp entity)
    {
        m_basicAblitity = new BasicAblitityIml();
        m_basicAblitity.Initialize(entity);
    }

    protected void AddExecuteEvent(EventBase e, Type exeType)
    {
        var ee = Activator.CreateInstance(exeType) as EventExecuteBase;
        ee.Initialize(m_basicAblitity);
        ee.Setup(e);
        m_events.Add(ee);

        m_duration = Mathf.Max(m_duration, ee.m_endTime);
    }

    protected Type GetExeEvtType(Type evtType)
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

    public void Setup(List<EventBase> events)
    {
        m_events.Clear();
        foreach (var evt in events)
        {
            var exeType = GetExeEvtType(evt.GetType());
            if (exeType != null)
            {
                AddExecuteEvent(evt, exeType);
            }
        }
        m_isStart = false;
        m_isEnd = false;
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

    protected virtual void Clear()
    {
        m_isStart = false;
        m_isEnd = false;
        m_duration = 0;
        m_events.Clear();
    }

    private void OnEnd()
    {
        Clear();
    }


}
