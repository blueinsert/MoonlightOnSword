using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSkillComp : ComponentBase
{
    public bool IsPlaying { get { return m_skillPlayer.IsPlaying; } }

    public SkillPlayer m_skillPlayer = new SkillPlayer();

    public SkillsDesc m_skillsDesc = null;

    public HitResponseComp m_hitResponseComp = null;

    public bool m_autoTrack = true;
    private bool m_isInHitPauseing = false;

    public void Start()
    {
        m_hitResponseComp = GetComp<HitResponseComp>();

        m_skillsDesc = GetComponentInChildren<SkillsDesc>();

        m_skillPlayer.Initialize(this.GetComp<EntityComp>());
    }

    public override void Tick()
    {
        base.Tick();
        if (m_isInHitPauseing)
        {
            if (m_hitResponseComp != null)
            {
                if (!m_hitResponseComp.IsInPause())
                {
                    OnHitPauseEnd();
                    m_isInHitPauseing = false;
                }
            }
            return;
        }

        if (IsPlaying)
            m_skillPlayer.Tick();
        if (m_skillPlayer.NextSkillId > 0)
        {
            var res = TryTranslate2NewSkill(m_skillPlayer.NextSkillId);
            m_skillPlayer.ClearNextSkill();
        }
    }

    public bool TryAttack()
    {
        //Debug.Log("BehaviorSkillComp:TryAttack");
        if (!IsPlaying)
        {
            TryPlaySkill(1);
        }
        return false;
    }

    private bool TryTranslate2NewSkill(int id)
    {
        if (IsPlaying)
        {
            m_skillPlayer.Stop();
        }
        var res = TryPlaySkill(id);
        return res;
    }

    public bool TryPlaySkill(int id, int from = -1)
    {
        Debug.Log(string.Format("BehaviorSkillComp:TryPlaySkill {0}", id));
        var skillCfg = m_skillsDesc.GetSkillConfig(id);
        PlayerSkill(skillCfg);
        return true;
    }

    private void PlayerSkill(SkillConfig cfg)
    {
        if (m_autoTrack)
        {
            var me = GetComp<EntityComp>();
            var move = GetComp<MoveComp>();
            var target = SearchSystem.Instance.FindNearestInRange(me, 2);
            if(target != null)
                move.LookAt(target.gameObject.transform.position);
        }
        m_skillPlayer.Setup(cfg);
        m_skillPlayer.Start();
    }

    //debug
    public void ForcePlaySkill(int id)
    {
        m_skillsDesc.RefreshSkills();

        var skillCfg = m_skillsDesc.GetSkillConfig(id);
        PlayerSkill(skillCfg);
    }

    public void OnHitTarget(HitDef hitDef, bool isBeBlocking = false)
    {
        var pauseTime = isBeBlocking ? hitDef.P1GuardPauseTime : hitDef.P2HitPauseTime;
        if (pauseTime > 0)
        {
            m_hitResponseComp.StartPause(pauseTime);
            OnHitPauseStart();
            m_isInHitPauseing = true;
        }
    }

    private void OnHitPauseStart()
    {
        m_skillPlayer.BaiscAblitity.FreezeAnim();
        m_skillPlayer.BaiscAblitity.DisableMove();
    }

    private void OnHitPauseEnd()
    {
        m_skillPlayer.BaiscAblitity.UnFreezeAnim();
        m_skillPlayer.BaiscAblitity.EnableMove();
    }
}
