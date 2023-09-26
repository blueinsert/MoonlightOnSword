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

    public void Start()
    {
        m_desc = GetComponentInChildren<GetHitBehaviorDesc>();

        m_player.Initialize(this.GetComp<EntityComp>());
    }

    public void StartGetHit(EntityComp attack, HitDef hitDef)
    {
        m_hitDef = hitDef;

        var cfg = m_desc.m_bahaviorCfg;

        var evts = new List<EventBase>(cfg.GetAllEvents());
        //处理速度
        foreach(var evt in evts)
        {
            if(evt is VelSetEvent)
            {
                var velSetEvt = evt as VelSetEvent;
                if(velSetEvt.Relative == VelRelativeType.FromHit)
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
        m_player.Setup(evts);

        m_player.Start();
    }

    public override void Tick()
    {
        base.Tick();
        if (m_player.IsPlaying)
        {
            m_player.Tick();
        }
    }
}
