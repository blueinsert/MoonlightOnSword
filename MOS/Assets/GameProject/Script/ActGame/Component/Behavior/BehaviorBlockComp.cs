using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorBlockComp : ComponentBase
{

    public const float MinDuration = 0.1f;

    public bool m_isInBlocking = false;
    public float m_blockStartTime = 0;
    public float m_blockEndTime = 0;

    public bool IsPlaying { get { return m_isInBlocking; } }

    public void StartBlock()
    {
        m_isInBlocking = true;
        m_blockStartTime = TimeManger.Instance.CurTime;
        m_blockEndTime = m_blockStartTime + MinDuration;
    }

    public void Refresh()
    {
        if (m_isInBlocking)
        {
            var cur = TimeManger.Instance.CurTime;
            m_blockEndTime = cur + MinDuration;
        }
    }

    public override void Tick()
    {
        base.Tick();
        var cur = TimeManger.Instance.CurTime;
        m_isInBlocking = cur >= m_blockStartTime && cur <= m_blockEndTime;
    }
}
