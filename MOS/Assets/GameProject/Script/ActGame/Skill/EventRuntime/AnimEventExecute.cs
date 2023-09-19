using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventExecute : EventRuntimeBase {

    private string m_animName;

	public void Setup(AnimEvent animEvent)
	{
        m_animName = animEvent.Anim;
        var start = animEvent.StartTime;
		var end = animEvent.EndTime;
		SetStartTime(start);
		SetEndTime(end);
	}

    public override void OnStart()
    {
        m_basicAblity.PlayAnim(m_animName);
    }

    public override void OnEnd()
    {

    }

    protected override void OnTick(float deltaTime)
    {
    }
}
