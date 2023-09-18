using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : FsmStateBase
{
    public StateIdle(ActorBase owner, StateType type) : base(owner, type)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        m_animComp.PlayAnim("StandIdle");
        m_moveComp.SetPreferVelHorizon(0, 0);
    }
}
