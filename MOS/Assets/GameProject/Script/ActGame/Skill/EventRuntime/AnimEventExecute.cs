using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventExecute : EventRuntimeBase {

    private string m_animName;
    private float m_speed = 1.0f;

	public void Setup(AnimEvent animEvent)
	{
        m_animName = animEvent.Anim;
        var start = animEvent.StartTime;
		var end = animEvent.EndTime;
		SetStartTime(start);
		SetEndTime(end);
        var oriLen = animEvent.OriginLen;
        var len = end - start;
        m_speed = oriLen / len;
	}

    public override void OnStart()
    {
        m_basicAblity.SetAnimSpeed(m_speed);
        m_basicAblity.PlayAnim(m_animName);
    }

    public override void OnEnd()
    {
        m_basicAblity.SetAnimSpeed(1.0f);
    }

    protected override void OnTick(float deltaTime)
    {
    }
}
