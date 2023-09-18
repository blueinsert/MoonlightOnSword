using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmComp : ComponentBase
{
    protected CmdComp m_cmdComp = null;

    protected StateWalk m_stateNormal;
    protected StateIdle m_stateIdle;
    protected StateRun m_stateRun;
    protected StateAction m_stateAction;
    protected StateGetHit m_stateGetHit;

    public StateType m_prevStateType;
    [SerializeField]
    public FsmStateBase m_curState;

    private Dictionary<StateType, FsmStateBase> m_stateDic = new Dictionary<StateType, FsmStateBase>();

    public override void Init(ActorBase actor)
    {
        base.Init(actor);
    }

    public override void PostInit()
    {
        base.PostInit();
        m_cmdComp = m_owner.GetComponent<CmdComp>();

        m_stateIdle = new StateIdle(m_owner, StateType.Idle);
        m_stateIdle.Init();
        m_stateNormal = new StateWalk(m_owner, StateType.Walk);
        m_stateNormal.Init();
        m_stateRun = new StateRun(m_owner, StateType.Run);
        m_stateRun.Init();
        m_stateAction = new StateAction(m_owner, StateType.Action);
        m_stateAction.Init();
        m_stateAction.EventOnEnd += OnActionEnd;
        m_stateGetHit = new StateGetHit(m_owner, StateType.GetHit);
        m_stateGetHit.Init();
        m_stateGetHit.EventOnEnd += OnGetHitEnd;

        m_stateDic.Add(m_stateIdle.m_type, m_stateIdle);
        m_stateDic.Add(m_stateNormal.m_type, m_stateNormal);
        m_stateDic.Add(m_stateRun.m_type, m_stateRun);
        m_stateDic.Add(m_stateAction.m_type, m_stateAction);
        m_stateDic.Add(m_stateGetHit.m_type, m_stateGetHit);

        ChangeState(StateType.Idle);
    }

    private void OnGetHitEnd()
    {
        ChangeState(StateType.Idle);
    }

    private void OnActionEnd()
    {
        ChangeState(StateType.Idle);
    }

    private void CheckStateTranslateCondition()
    {
        if (m_curState.m_type == StateType.Idle)
        {
            if (m_cmdComp.m_isRunCmdActive)
            {
                ChangeState(StateType.Run);
            }
            else if (m_cmdComp.m_isWalkCmdActive)
            {
                ChangeState(StateType.Walk);
            }
        }
        else if (m_curState.m_type == StateType.Walk)
        {
            if (m_cmdComp.m_isRunCmdActive)
            {
                ChangeState(StateType.Run);
            }
            if (m_cmdComp.m_moveValue == Vector2.zero)
            {
                ChangeState(StateType.Idle);
            }
        }
        else if (m_curState.m_type == StateType.Run)
        {
            if (m_cmdComp.m_moveValue == Vector2.zero)
            {
                ChangeState(StateType.Idle);
            }
        }
        if (m_cmdComp.m_isAttackCmdActive)
        {
            if (m_curState != m_stateAction)
                PlayAction(1);
        } 
    }

    public override void Tick()
    {
        base.Tick();
        if (m_curState != null)
        {
            m_curState.Tick();
            CheckStateTranslateCondition();
        }
    }

    private FsmStateBase GetState(StateType type)
    {
        return m_stateDic[type];
    }

    public void ChangeState(StateType stateType)
    {
        var newState = GetState(stateType);
        var oldState = m_curState;
        if (oldState != null)
        {
            oldState.OnExit();
            m_prevStateType = oldState.m_type;
        }
        newState.OnEnter();
        m_curState = newState;
    }


    public void PlayAction(int id)
    {
        var config = ConfigDataManager.Instance.GetConfigDataActionConfig(id);
        m_stateAction.SetActionConfig(config);
        ChangeState(StateType.Action);
    }

    private void EnterGetHit(HitEffectConfig hitDef, DamageCalcResult damageRes)
    {
        m_stateGetHit.SetParams(hitDef, damageRes);
        ChangeState(StateType.GetHit);
    }

    public bool IsActionPlaying()
    {
        return m_curState == m_stateAction;
    }

    public float GetActionTime()
    {
        var curTime = TimeManger.Instance.CurTime;
        var res = curTime - m_stateAction.m_actionStartTime;
        return res;
    }

    public bool IsActionFinish()
    {
        if (m_curState == m_stateAction)
        {
            return m_stateAction.m_isEnd;
        }
        else if (m_curState == m_stateGetHit)
        {
            return m_stateGetHit.m_isEnd;
        }
        return true;
    }

    public void OnHitTarget(HitEffectConfig hitDef)
    {
        if (m_curState == m_stateAction)
        {
            m_stateAction.OnHitTarget(hitDef);
        }
    }

    public void OnBeHit(HitEffectConfig hitDef, DamageCalcResult damageRes)
    {
        EnterGetHit(hitDef, damageRes);
    }
}
