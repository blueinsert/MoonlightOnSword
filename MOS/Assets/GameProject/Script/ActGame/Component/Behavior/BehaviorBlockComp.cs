using Flux;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockStatus
{
    None,
    BlockStart,
    BlockEnd,
    Blocking,
    BlockHitPause,
    BlockHitRecover,
}

public class BehaviorBlockComp : ComponentBase
{

    public const float StartDuration = 0.1f;
    public const float EndDuration = 0.1f;
    public const float MinDuration = 0.05f;

    public float m_blockStartTime = 0;
    public float m_blockEndTime = 0;
    public BlockStatus m_status = BlockStatus.None;
    public BasicAblitityIml m_basicAblitity = null;

    public bool IsInBlocking { get { return m_status != BlockStatus.None; } }

    public int m_pauseStartFrame;
    public int m_pauseEndFrame;
    public int m_recoverEndFrame;

    public Vector2 m_backVel;

    public void Start()
    {
        m_basicAblitity = new BasicAblitityIml();
        var entityComp = GetComp<EntityComp>();
        m_basicAblitity.Initialize(entityComp);
    }

    public void StartBlock()
    {
        m_status = BlockStatus.BlockStart;
        m_blockStartTime = TimeManger.Instance.CurTime;
    }

    public void Clear()
    {
        m_status = BlockStatus.None;
    }

    public void Refresh()
    {
        if (m_status != BlockStatus.None)
        {
            var cur = TimeManger.Instance.CurTime;
            m_blockEndTime = cur + MinDuration;
        }
    }

    //格挡成功
    public void OnBlockHit(EntityComp attacker, HitDef hitdef)
    {
        m_status = BlockStatus.None;
        StartBlockPause(hitdef);
        SetBackSpeed(attacker, hitdef);
    }

    private void SetBackSpeed(EntityComp attacker, HitDef hitdef)
    {
        var speed = hitdef.GuardBackHSpeed;
        var dir = (this.gameObject.transform.position - attacker.transform.position).normalized;
        var hdir = new Vector2(dir.x, dir.z);
        hdir.Normalize();
        var velH = hdir * speed;
        m_backVel = velH;
       
    }

    private void StartBlockPause(HitDef hitdef)
    {
        var pauseTime = hitdef.P2GuardPauseTime;
        var recoverTime = hitdef.P2GuardRecoverTime;
        m_pauseStartFrame = TimeManger.Instance.Frame;
        m_pauseEndFrame = m_pauseStartFrame + pauseTime;
        m_recoverEndFrame = m_pauseEndFrame + recoverTime;
        m_status = BlockStatus.BlockHitPause;
        OnPauseStart();
    }

    private void OnPauseStart()
    {
        m_basicAblitity.FreezeAnim();
        m_basicAblitity.DisableMove();
    }

    private void OnPauseEnd()
    {
        m_basicAblitity.UnFreezeAnim();
        m_basicAblitity.EnableMove();
    }

    public override void Tick()
    {
        base.Tick();
        var frame = TimeManger.Instance.Frame;
        var cur = TimeManger.Instance.CurTime;
        if (m_status == BlockStatus.BlockHitPause)
        {
            if (frame > m_pauseEndFrame)
            {
                OnPauseEnd();
                var move = GetComp<MoveComp>();
                move.SetPreferVelHorizon(m_backVel.x, m_backVel.y, false);
                m_status = BlockStatus.BlockHitRecover;
            }
        }else if (m_status == BlockStatus.BlockHitRecover)
        {
            if(frame > m_recoverEndFrame)
            {
                m_status = BlockStatus.Blocking;
                var move = GetComp<MoveComp>();
                move.SetPreferVelHorizon(0, 0, false);
            }
        }
        else if(m_status == BlockStatus.BlockStart)
        {
            if(cur > m_blockStartTime + StartDuration)
            {
                m_status = BlockStatus.Blocking;
            }
        }else if(m_status == BlockStatus.BlockEnd)
        {
            if(cur > m_blockEndTime + EndDuration)
            {
                m_status = BlockStatus.None;
            }
        }else if(m_status == BlockStatus.Blocking)
        {
            if(cur > m_blockEndTime)
            {
                m_status = BlockStatus.BlockEnd;
            }
        }
        if (m_status == BlockStatus.BlockHitPause
            || m_status == BlockStatus.BlockHitRecover)
        {
            Refresh();
        }

    }
}
