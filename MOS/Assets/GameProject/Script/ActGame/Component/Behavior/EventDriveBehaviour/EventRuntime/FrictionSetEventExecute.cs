using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionSetEventExecute : EventExecuteBase
{
    private FrictionSetEvent m_fe;
    private float m_value;

    public override void Setup(EventBase e)
    {
        m_fe = e as FrictionSetEvent;
        m_value = m_fe.Value;
        base.Setup(e);
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

        m_basicAblity.SetVelH(newVel);
    }
}
