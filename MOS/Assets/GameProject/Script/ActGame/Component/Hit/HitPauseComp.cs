using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于被攻击者的打击停顿
/// </summary>
public class HitPauseComp : ComponentBase {
    public int m_startFrame;
    public int m_endFrame;

    public bool IsInPause()
    {
        var cur = TimeManger.Instance.Frame;
        return cur >= m_startFrame && cur <= m_endFrame;
    }

    public void StartPause(int pauseTime)
    {
        m_startFrame = TimeManger.Instance.Frame;
        m_endFrame = m_startFrame + pauseTime;

    }
}
