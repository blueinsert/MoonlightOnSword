using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受创状态 内部是一个状态机
/// </summary>
public class BehaviorGethitComp : ComponentBase {

	public GetHitBehaviorDesc m_desc = null;

    public BehaviorPlayer m_player = new BehaviorPlayer();

    public HitDef m_hitDef = null;

    //List<EventBase> m_events = new List<EventBase>();

    public HitPauseComp m_hitPauseComp = null;

    private bool m_isInHitPauseing = false;

    public int m_time = 0;

    public void Start()
    {
        m_desc = GetComponentInChildren<GetHitBehaviorDesc>();

        m_player.Initialize(this.GetComp<EntityComp>());

        m_hitPauseComp = GetComp<HitPauseComp>();
    }

    private List<EventBase> GetEvents(EntityComp attack, HitDef hitDef)
    {
        var cfg = m_desc.m_bahaviorCfg;

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
        
        m_player.Setup(evts);

        m_player.Start();

        var anim = GetComp<AnimComp>();
        anim.GetHit();

        m_time = 0;
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
        if (m_time == 2)
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
