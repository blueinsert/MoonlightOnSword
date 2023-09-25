using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelSetEventExecute : EventExecuteBase
{

    private VelSetEvent m_e;

    public override void Setup(EventBase e)
    {
        m_e = e as VelSetEvent;
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
        if (m_e.Relative ==  VelRelativeType.Local)
        {
            var facing = m_basicAblity.GetFacing();
            var front = new Vector3(facing.x,0,facing.y).normalized;
            var right = Vector3.Cross(front, Vector3.up).normalized;
            var up = Vector3.up;
            var vel = front * m_e.VZ + right * m_e.VX + up * m_e.VY;
            m_basicAblity.SetVelH(new Vector2(vel.x,vel.z));
            m_basicAblity.SetVelV(vel.y);

        }
    }
}

