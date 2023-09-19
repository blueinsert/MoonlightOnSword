using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionSetEventExecute : EventRuntimeBase
{

    private float m_value;

    public void Setup(FrictionSetEvent e)
    {
        m_value = e.Value;
        var start = e.StartTime;
        var end = e.EndTime;
        SetStartTime(start);
        SetEndTime(end);
    }

    public override void OnStart()
    {
       
    }

    public override void OnEnd()
    {

    }

    protected override void OnTick(float deltaTime)
    {
        var vel = m_basicAblity.GetVel();
        var speed = vel.magnitude;
        
        var dir = vel.normalized;
        speed -= m_value * deltaTime;
        if (speed < 0.1f)
            speed = 0f;
        var newVel = dir * speed;

        m_basicAblity.SetVel(newVel);
    }
}
