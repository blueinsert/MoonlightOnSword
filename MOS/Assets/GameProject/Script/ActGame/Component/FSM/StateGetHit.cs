using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GetHitState
{
    None,
    Shake,
    Back,
}

public class StateGetHit : FsmStateBase
{
    public HitEffectConfig m_hitDef;
    public DamageCalcResult m_damageRes;
    public float m_stateTimer;
    public GetHitState m_gethitState = GetHitState.None;
    public float m_shakeEndTime;
    public float m_backEndTime;

    public bool m_isEnd = false;

    public StateGetHit(ActorBase owner, StateType type) : base(owner, type)
    {

    }

    public override void Init()
    {
        base.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private void Tick_HitSlide()
    {
        if(m_stateTimer > m_backEndTime)
        {
            m_isEnd = true;
            m_gethitState = GetHitState.None;
            OnEnd();
        }
    }

    private void Tick_HitShake()
    {
        if (m_stateTimer > m_shakeEndTime)
        {
            StartHitSlide();
        }
    }

    public override void Tick()
    {
        base.Tick();
        if(m_gethitState == GetHitState.Shake)
        {
            Tick_HitShake();
        }else if(m_gethitState == GetHitState.Back)
        {
            Tick_HitSlide();
        }
        m_stateTimer += TimeManger.Instance.DeltaTime;
    }

    private void StartHitShake()
    {
        m_gethitState = GetHitState.Shake;
        m_animComp.PlayAnimFrom("GetHit_Front1", 0.16f);
        m_animComp.Freeze();
        m_moveComp.enabled = false;
    }

    private void StartHitSlide()
    {
        m_gethitState = GetHitState.Back;
        m_animComp.UnFreeze();
        m_moveComp.enabled = true;
        var dir = m_damageRes.damageSourceDirVec;
        dir.y = 0;
        var sx = -dir.x * m_hitDef.HitBackHSpeed/100f;
        var sz = -dir.z * m_hitDef.HitBackHSpeed/100f;
        m_moveComp.SetPreferVelHorizon(sx,sz);
    }

    public void SetParams(HitEffectConfig hitDef,DamageCalcResult damageRes)
    {
        m_hitDef = hitDef;
        m_damageRes = damageRes;
        m_shakeEndTime = m_hitDef.P2HitPauseTime / 1000f;
        m_backEndTime = m_shakeEndTime + m_hitDef.HitRecoverTime / 1000f;
        m_isEnd = false;
        m_stateTimer = 0;
        StartHitShake();
    }

}
