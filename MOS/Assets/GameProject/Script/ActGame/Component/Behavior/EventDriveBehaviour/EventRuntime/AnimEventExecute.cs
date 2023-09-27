using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventExecute : EventExecuteBase {

    private string m_animName;
    private float m_speed = 1.0f;
    private AnimEvent m_animEvent;

    private float m_lastSpeed = 1;//todo

    public override void Setup(EventBase e)
	{
        m_animEvent = e as AnimEvent;
        var animEvent = m_animEvent;
        m_animName = animEvent.Anim;
        base.Setup(e);
        var oriLen = animEvent.OriginLen;
        var len = m_endTime - m_startTime;
        m_speed = oriLen / len;
	}

    public override void OnStart()
    {
        m_basicAblity.SetAnimSpeed(m_speed);
        m_basicAblity.PlayAnim(m_animName, m_animEvent.IsUseTrigger);
    }

    public override void OnEnd()
    {
        m_basicAblity.SetAnimSpeed(1.0f);
    }

    protected override void OnTick(float deltaTime)
    {
    }
}
