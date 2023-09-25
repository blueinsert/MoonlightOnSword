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

    public void StartGetHit(HitDef hitDef)
    {
        m_hitDef = hitDef;

        var cfg = m_desc.m_bahaviorCfg;
        m_player.Setup(new List<EventBase>(cfg.GetAllEvents()));

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
