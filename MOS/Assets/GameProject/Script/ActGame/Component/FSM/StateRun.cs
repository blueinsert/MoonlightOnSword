using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRun : FsmStateBase
{

    public StateRun(ActorBase owner, StateType type) : base(owner, type)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        m_animComp.PlayAnim("Run");
    }

    public override void Tick()
    {
        base.Tick();
        var moveValue = m_cmdComp.m_moveValue;
        var forward = m_cmdComp.m_cameraForward;
        forward.y = 0;
        var hvel = Vector3.zero;
        if (moveValue.x != 0 || moveValue.y != 0)
        {
            var left = -Vector3.Cross(forward, Vector2.up);
            left = left.normalized;
            var dir = forward * moveValue.y + left * moveValue.x;
            dir = dir.normalized;
            hvel = dir * m_propertyComp.m_runSpeed;
            m_moveComp.SetPreferVelHorizon(hvel.x, hvel.z);
            //m_animComp.SetTurnProgress(m_moveComp.m_turnProgress);
        }

    }
}
