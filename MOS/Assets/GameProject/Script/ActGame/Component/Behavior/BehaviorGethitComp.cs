﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeHitStatus
{
    None,
    PreHitPausing,
    HitPausing,
    HitRecover,
    HitRecoverDead,
    HitFlyUp,
    HitFlyFalling,
    HitFlyLanding,
    HitFlyGetup,
}

/// <summary>
/// 受创状态 内部是一个状态机
/// </summary>
public class BehaviorGethitComp : BehaviorCompBase {

    public BehaviorGethitComp() : base(BehaviorType.GetHit) { }

    //public const string ResourceName_GetHitFront = "BeHit_Front";
    //public const string ResourceName_GetHitFrontHeavy = "BeHit_Front_Heavy";
    //public const string ResourceName_GetHitUp = "BeHit_Up";
    //public const string ResourceName_GetHitUpHeavy = "BeHit_Up_Heavy";

    public const float LandingDuration = 1f;
    public const float GetupDuration = 1f;
    public const float DeadingDuration = 0.9f;

    public bool IsPlaying { get { return m_status != BeHitStatus.None; } }

    public HitPauseComp m_hitPauseComp = null;
    public AnimComp m_animComp = null;
    public MoveComp m_moveComp = null;

    public BasicAblitityIml m_basicAblitity = null;

    public bool m_isDead = false;
    public BeHitStatus m_status;
    public HitDef m_hitDef = null;
    public bool m_isBeHitFly = false;

    public Vector2 m_backVel;
    public int m_hitPuaseStartTime = -1;
    public int m_hitPauseEndTime = -1;
    public int m_hitRecoverEndTime = -1;
    public float m_landingStartTime = 0;
    public float m_getupStartTime = 0;
    public float m_deadingEndTime = 0;

    public void Start()
    {
        m_hitPauseComp = GetComp<HitPauseComp>();
        m_animComp = GetComp<AnimComp>();
        m_moveComp = GetComp<MoveComp>();

        m_basicAblitity = new BasicAblitityIml();
        var entityComp = GetComp<EntityComp>();
        m_basicAblitity.Initialize(entityComp);
    }

    #region obsolete

    /*
    [Obsolete]
    private BehaviorConfig GetBehaviorCfg2(EntityComp attack, HitDef hitDef)
    {
        //计算相对角度
        var p1 = attack.gameObject.transform.position;
        var p2 = this.gameObject.transform.position;
        var move = GetComp<MoveComp>();
        var facing = move.Facing;
        var dir = (p1 - p2);
        dir.y = 0;
        dir.Normalize();
        Vector2 dir2d = new Vector2(dir.x, dir.z);
        var angle = Vector2.SignedAngle(facing, dir2d);
        string rname = ResourceName_GetHitFront;
        if (angle >= -45 && angle < 45)
        {
            rname = ResourceName_GetHitFront;
        }else if(angle >= -135 && angle < -45)
        {
            rname = ResourceName_GetHitLeft;
        }
        else if(angle >=45 && angle < 135)
        {
            rname =  ResourceName_GetHitRight;
        }
        else if(angle >=-180 && angle <-135 || angle >=135 && angle <= 180)
        {
            rname = ResourceName_GetHitBack;
        }
        return m_desc.GetBehaviorCfg(rname);
    }
    */

    /*
    private BehaviorConfig GetBehaviorCfg(EntityComp attack, HitDef hitDef)
    {
        string rname = ResourceName_GetHitFront;
        var level = hitDef.Level;
        var posType = hitDef.PosType;
        if(level == HitForceLevel.Light)
        {
            if(posType == HitPosType.Low)
            {
                rname = ResourceName_GetHitFront;
            }else if(posType == HitPosType.High)
            {
                rname = ResourceName_GetHitUp;
            }
        }else if(level == HitForceLevel.Heavy)
        {
            if (posType == HitPosType.Low)
            {
                rname = ResourceName_GetHitFrontHeavy;
            }
            else if (posType == HitPosType.High)
            {
                rname = ResourceName_GetHitUpHeavy;
            }
        }
        return m_desc.GetBehaviorCfg(rname);
    }
    */

    /*
    private List<EventBase> GetEvents(EntityComp attack, HitDef hitDef)
    {
        var cfg = GetBehaviorCfg(attack,hitDef);
        if (cfg == null)
            return null;
        var evts = new List<EventBase>(cfg.GetAllEvents());
        //处理速度
        foreach (var evt in evts)
        {
            if (evt is VelSetEvent)
            {
                var velSetEvt = evt as VelSetEvent;
                if (velSetEvt.Relative == VelRelativeType.FromHit)
                {
                    var dir = (this.gameObject.transform.position - attack.transform.position).normalized;
                    var hdir = new Vector2(dir.x, dir.z);
                    hdir.Normalize();
                    var velH = hdir * hitDef.HitBackHSpeed;
                    velSetEvt.VX = velH.x;
                    velSetEvt.VZ = velH.y;
                    velSetEvt.VY = hitDef.HitBackVSpeed;
                }
            }
        }

        return evts;
    }
    */

    #endregion

    private void PlayHitAnim()
    {
        if (!m_isBeHitFly) {
            if (!m_isDead)
                m_animComp.GetHit((int)m_hitDef.Level, (int)m_hitDef.PosType);
            else
                m_animComp.Dead(0);
        }    
        else
        {
            m_animComp.BeHitFly();
        }
    }



    private void ExitHitAnim()
    {
        m_animComp.ExitHit();
    }

    public void StartGetHit(EntityComp attacker, HitDef hitDef,bool isDead = false)
    {
        Debug.Log("BehaviorGethitComp:StartGetHit");
        m_hitDef = hitDef;
        m_isBeHitFly = hitDef.HitBackVSpeed > 0;
        m_isDead = isDead;

        m_hitPuaseStartTime = TimeManger.Instance.Frame + 1;//延迟1帧
        m_hitPauseEndTime = m_hitPuaseStartTime + m_hitDef.P2HitPauseTime;
        m_hitRecoverEndTime = m_hitPauseEndTime + m_hitDef.P2HitRecoverTime;

        m_status = BeHitStatus.PreHitPausing;
        PlayHitAnim();
        SetBackSpeed(attacker, hitDef);
    }

    private void SetBackSpeed(EntityComp attacker, HitDef hitdef)
    {
        var speed = hitdef.HitBackHSpeed;
        var dir = (this.gameObject.transform.position - attacker.transform.position).normalized;
        var hdir = new Vector2(dir.x, dir.z);
        hdir.Normalize();
        var velH = hdir * speed;
        m_backVel = velH;

    }

    private void OnHitPauseStart()
    {
        m_basicAblitity.FreezeAnim();
        m_basicAblitity.DisableMove();
    }

    private void OnHitPauseEnd()
    {
        m_basicAblitity.UnFreezeAnim();
        m_basicAblitity.EnableMove();
    }

    public override void Tick()
    {
        base.Tick();
        var frame = TimeManger.Instance.Frame;
        var cur = TimeManger.Instance.CurTime;
        if (m_status == BeHitStatus.PreHitPausing)
        {
            if(frame >= m_hitPuaseStartTime)
            {
                OnHitPauseStart();
                m_status = BeHitStatus.HitPausing;
            }
        }else if(m_status == BeHitStatus.HitPausing)
        {
            if(frame >= m_hitPauseEndTime)
            {
                if (!m_isBeHitFly)
                {
                    m_status = m_isDead ? BeHitStatus.HitRecoverDead : BeHitStatus.HitRecover;
                    OnHitPauseEnd();
                    m_basicAblitity.SetVelH(m_backVel, false);
                    if (m_isDead)
                    {
                        m_deadingEndTime = cur + DeadingDuration;
                    }
                }
                else
                {
                    m_status = BeHitStatus.HitFlyUp;
                    OnHitPauseEnd();
                    m_basicAblitity.SetVelH(m_backVel, false);
                    m_basicAblitity.SetVelVOnce(m_hitDef.HitBackVSpeed);
                }
            }
        }else if(m_status == BeHitStatus.HitRecover)
        {
            if(frame >= m_hitRecoverEndTime)
            {
                m_basicAblitity.SetVelH(Vector2.zero, false);
                ExitHitAnim();
                m_status = BeHitStatus.None;
            }
        }else if(m_status == BeHitStatus.HitRecoverDead)
        {
            //todo
            if(cur > m_deadingEndTime)
            {
                m_basicAblitity.SetVelH(Vector2.zero, false);
                m_status = BeHitStatus.None;
            }
        }
        else if(m_status == BeHitStatus.HitFlyUp)
        {
            float vy = m_moveComp.m_vel.y;
            m_animComp.SetVY(vy);
            if (vy < 0)
            {
                m_status = BeHitStatus.HitFlyFalling;
            }
        }else if(m_status == BeHitStatus.HitFlyFalling)
        {
            m_animComp.SetLanding(m_moveComp.m_isOnGround);
            if (m_moveComp.m_isOnGround)
            {
                m_status = BeHitStatus.HitFlyLanding;
                m_landingStartTime = TimeManger.Instance.CurTime;
                m_basicAblitity.SetVelH(Vector2.zero, false);
                m_basicAblitity.SetVelV(0);
            }
        }else if(m_status == BeHitStatus.HitFlyLanding)
        {
            if(!m_isDead && cur > m_landingStartTime + LandingDuration)
            {
                m_status = BeHitStatus.HitFlyGetup;
                m_animComp.Getup();
                m_getupStartTime = cur;
            }
        }else if(m_status == BeHitStatus.HitFlyGetup)
        {
            if(cur > m_getupStartTime + GetupDuration)
            {
                m_basicAblitity.SetVelH(Vector2.zero, false);
                ExitHitAnim();
                m_status = BeHitStatus.None;
            }
        }
    }

    public override bool IsBehaviorActive()
    {
        return IsPlaying;
    }

    public override void OnOtherBehaviorEnter(BehaviorType other)
    {
    }
}
