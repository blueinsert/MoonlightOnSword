using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateAction : FsmStateBase
{
    public ActionConfig m_actionCfg = null;
    public float m_actionStartTime;
    public float m_actionEndTime;
    public float m_actionTime;
    public List<ActionEventBase> m_actionEventList = new List<ActionEventBase>();
    public bool m_isEnd = false;
    public float m_hitPauseStartTime;
    public float m_hitPauseEndTime;
    public bool m_isHitPauseEnd = false;
    public StateAction(ActorBase owner, StateType type) : base(owner, type)
    {

    }

    public void SetActionConfig(ActionConfig actionCfg)
    {
        m_actionCfg = actionCfg;
    }

    public ActionConfig GetActionConfig()
    {
        return m_actionCfg;
    }

    #region ActionEvents
    private void PrepareActionEvents()
    {
        m_actionEventList.Clear();
        PrepareMotionActionEvents();
        PrepareHitdefActionEvents();
    }


    private void PrepareDisplacementActionEvents()
    {
        foreach (var ds in m_actionCfg.m_actionEventDisplacements)
        {
            ActionEventDisplacement displacement = new ActionEventDisplacement();
            displacement.SetOwner(this.m_owner);
            displacement.SetStartTime(ds.m_startTime / 1000f);
            Vector2 offset = new Vector2(ds.m_offsetX / 100f, ds.m_offsetY / 100f);
            displacement.SetParams(offset);
            m_actionEventList.Add(displacement);
        }
    }

    private void PrepareMotionActionEvents()
    {
        foreach (var ae in m_actionCfg.m_actionEventMotions)
        {
            ActionEventSetMotion setMotion = new ActionEventSetMotion();
            setMotion.SetOwner(this.m_owner);
            var startTime = m_actionStartTime + ae.m_startTime / 1000f;
            var endTime = m_actionStartTime + ae.m_endTime / 1000f;
            setMotion.SetStartTime(startTime);
            setMotion.SetEndTime(endTime);
            setMotion.SetParams(ae.m_cfgID);
            m_actionEventList.Add(setMotion);
        }
    }

    private void PrepareHitdefActionEvents()
    {
        foreach (var ae in m_actionCfg.m_actionEventHitDefs)
        {
            ActionEventSetHitdef setHitdef = new ActionEventSetHitdef();
            setHitdef.SetOwner(this.m_owner);
            var startTime = m_actionStartTime + ae.m_startTime / 1000f;
            var endTime = m_actionStartTime + ae.m_endTime / 1000f;
            setHitdef.SetStartTime(startTime);
            setHitdef.SetEndTime(endTime);
            setHitdef.SetParams(ae.m_cfgID);
            m_actionEventList.Add(setHitdef);
        }
    }
    #endregion

    public void ReEnterState(int id)
    {
        var config = ConfigDataManager.Instance.GetConfigDataActionConfig(id);
        SetActionConfig(config);
        OnExit();
        OnEnter();
    }

    public override void OnEnter()
    {
        base.OnEnter(); 
        m_actionStartTime = TimeManger.Instance.CurTime;
        m_actionEndTime = m_actionStartTime + m_actionCfg.Duration / 1000f;
        m_actionTime = m_actionStartTime;
        m_isEnd = false;
        m_animComp.PlayAnim(m_actionCfg.Anim);
        PrepareActionEvents();
    }

    public override void OnExit()
    {
        base.OnExit();
        m_motionComp.m_isEnable = false;
        foreach(var ae in m_actionEventList)
        {
            ae.ForceEnd();
        }
        m_actionEventList.Clear();
    }

    private void TickActionEvents()
    {
        foreach(var ae in m_actionEventList)
        {
            ae.Tick(m_actionTime);
        }
    }

    private bool IsInHitPauseing()
    {
        var curTime = TimeManger.Instance.CurTime;
        return curTime >= m_hitPauseStartTime && curTime <= m_hitPauseEndTime;
    }

    private void ProcessStateTransit()
    {
        foreach (var pair in GetActionConfig().m_nextActionDic)
        {
            bool isTrigger = true;
            foreach (var condition in pair.Value)
            {
                if (!condition.CheckCondition(m_owner))
                {
                    isTrigger = false;
                    break;
                }
            }
            if (isTrigger)
            {
                ReEnterState(pair.Key);
            }
        }
    }

    public override void Tick()
    {
        base.Tick();
        if (m_isHitPauseEnd)
        {
            m_actionTime += TimeManger.Instance.DeltaTime;
            if (!m_isEnd)
            {
                TickActionEvents();
                ProcessStateTransit();
                if (m_actionTime > m_actionEndTime)
                {
                    m_isEnd = true;
                    OnEnd();
                }  
            }
        }
        else
        {
            if(TimeManger.Instance.CurTime > m_hitPauseEndTime)
            {
                m_isHitPauseEnd = true;
                OnHitPauseEnd();
            }
        }
    }

    private void OnHitPauseStart()
    {
        m_animComp.Freeze();
        m_moveComp.enabled = false;
    }

    private void OnHitPauseEnd()
    {
        m_animComp.UnFreeze();
        m_moveComp.enabled = true;
    }

    public void OnHitTarget(HitEffectConfig hitDef)
    {
        m_hitPauseStartTime = TimeManger.Instance.CurTime;
        m_hitPauseEndTime = m_hitPauseStartTime + hitDef.P1HitPauseTime / 1000f;
        m_isHitPauseEnd = false;
        OnHitPauseStart();
    }

    

}
