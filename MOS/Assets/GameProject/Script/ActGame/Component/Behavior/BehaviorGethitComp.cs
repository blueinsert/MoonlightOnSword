using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeHitStatus
{
    None,
    PreHitPausing,
    HitPausing,
    HitRecover,
    HitFlyUp,
    HitFlyFalling,
    HitFlyLanding,
    HitFlyGetup,
}

/// <summary>
/// 受创状态 内部是一个状态机
/// </summary>
public class BehaviorGethitComp : ComponentBase {

    //public const string ResourceName_GetHitFront = "BeHit_Front";
    //public const string ResourceName_GetHitFrontHeavy = "BeHit_Front_Heavy";
    //public const string ResourceName_GetHitUp = "BeHit_Up";
    //public const string ResourceName_GetHitUpHeavy = "BeHit_Up_Heavy";

    public const float LandingDuration = 1f;

    public bool IsPlaying { get { return m_status != BeHitStatus.None; } }

    public HitPauseComp m_hitPauseComp = null;
    public AnimComp m_animComp = null;
    public MoveComp m_moveComp = null;

    public BasicAblitityIml m_basicAblitity = null;

    public BeHitStatus m_status;
    public HitDef m_hitDef = null;
    public bool m_isBeHitFly = false;

    public Vector2 m_backVel;
    public int m_hitPuaseStartTime = -1;
    public int m_hitPauseEndTime = -1;
    public int m_hitRecoverEndTime = -1;

    public void Start()
    {
        m_hitPauseComp = GetComp<HitPauseComp>();
        m_animComp = GetComp<AnimComp>();
        m_moveComp = GetComp<MoveComp>();

        m_basicAblitity = new BasicAblitityIml();
        var entityComp = GetComp<EntityComp>();
        m_basicAblitity.Initialize(entityComp);
    }

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

    private void PlayHitAnim()
    {
        m_animComp.GetHit((int)m_hitDef.Level, (int)m_hitDef.PosType);
    }



    private void ExitHitAnim()
    {
        m_animComp.ExitHit();
    }

    public void StartGetHit(EntityComp attacker, HitDef hitDef)
    {
        Debug.Log("BehaviorGethitComp:StartGetHit");
        m_hitDef = hitDef;
        m_isBeHitFly = hitDef.HitBackVSpeed > 0;

        m_hitPuaseStartTime = TimeManger.Instance.Frame + 2;//延迟1帧
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
        if(m_status == BeHitStatus.PreHitPausing)
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
                m_status = BeHitStatus.HitRecover;
                OnHitPauseEnd();
                m_basicAblitity.SetVelH(m_backVel, false);
            }
        }else if(m_status == BeHitStatus.HitRecover)
        {
            if(frame >= m_hitRecoverEndTime)
            {
                m_basicAblitity.SetVelH(Vector2.zero, false);
                ExitHitAnim();
                m_status = BeHitStatus.None;
            }
        }
    }
}
