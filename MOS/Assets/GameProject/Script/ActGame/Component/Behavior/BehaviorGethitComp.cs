using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受创状态 内部是一个状态机
/// </summary>
public class BehaviorGethitComp : ComponentBase {

    public const string ResourceName_GetHitFront = "GetHitFront";
    public const string ResourceName_GetHitBack = "GetHitBack";
    public const string ResourceName_GetHitLeft = "GetHitLeft";
    public const string ResourceName_GetHitRight = "GetHitRight";
    public const string ResourceName_GetHitUp = "GetHitUp";

    public GetHitBehaviorDesc m_desc = null;

    public BehaviorPlayer m_player = new BehaviorPlayer();

    public HitDef m_hitDef = null;

    //List<EventBase> m_events = new List<EventBase>();

    public HitPauseComp m_hitPauseComp = null;

    public AnimEventExecute m_animEventExecute = null;

    private bool m_isInHitPauseing = false;

    public int m_time = 0;
    public int m_hitPuaseStartTime = -1;

    public void Start()
    {
        m_desc = GetComponentInChildren<GetHitBehaviorDesc>();

        m_player.Initialize(this.GetComp<EntityComp>());

        m_hitPauseComp = GetComp<HitPauseComp>();
    }

    private BehaviorConfig GetBehaviorCfg(EntityComp attack, HitDef hitDef)
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

    public void StartGetHit(EntityComp attack, HitDef hitDef)
    {
        Debug.Log("BehaviorGethitComp:StartGetHit");
        m_hitDef = hitDef;

        var evts = GetEvents(attack, hitDef);
        if (evts == null)
        {
            Debug.LogError("BehaviorGethitComp:StartGetHit GetEvents == null");
            return;
        }
        m_player.Setup(evts);

        m_animEventExecute = m_player.GetEvent<AnimEventExecute>();

        m_player.Start();

        m_time = 0;
        m_hitPuaseStartTime = -1;
    }

    private void OnHitPauseStart()
    {
        m_player.BaiscAblitity.FreezeAnim();
        m_player.BaiscAblitity.DisableMove();
    }

    private void OnHitPauseEnd()
    {
        m_player.BaiscAblitity.UnFreezeAnim();
        m_player.BaiscAblitity.EnableMove();
    }

    public override void Tick()
    {
        base.Tick();
        if (!m_player.IsPlaying)
            return;
        m_time++;
        if (m_animEventExecute != null && m_animEventExecute.IsStart && m_hitPuaseStartTime==-1)
        {
            m_hitPuaseStartTime = m_time;
        }
        if(m_time == m_hitPuaseStartTime)
        {
            //延迟开始暂停
            if (m_hitDef.P2HitPauseTime > 0 && m_hitPauseComp != null)
            {
                m_isInHitPauseing = true;
                m_hitPauseComp.StartPause(m_hitDef.P2HitPauseTime);
                OnHitPauseStart();
            }
        }
        
        if (m_isInHitPauseing)
        {
            if (m_hitPauseComp != null)
            {
                if (!m_hitPauseComp.IsInPause())
                {
                    OnHitPauseEnd();
                    m_isInHitPauseing = false;
                }
            }
            return;
        }
        if (m_player.IsPlaying)
        {
            m_player.Tick();
        }
    }
}
