using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSkillComp : ComponentBase
{
    public bool IsPlaying { get { return m_skillPlayer.IsStart && !m_skillPlayer.IsEnd; } }

    public SkillPlayer m_skillPlayer = new SkillPlayer();

    public SkillsDesc m_skillsDesc = null;

    public void Start()
    {
        m_skillsDesc = GetComponentInChildren<SkillsDesc>();

        m_skillPlayer.Initialize(this.GetComp<EntityComp>());
    }

    public override void Tick()
    {
        base.Tick();
        if(IsPlaying)
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
        Debug.Log(string.Format("BehaviorSkillComp:TryPlaySkill {0}",id));
        var skillCfg = m_skillsDesc.GetSkillConfig(id);
        m_skillPlayer.Setup(skillCfg);
        m_skillPlayer.Start();
        return true;
    }

    //debug
    public void ForcePlaySkill(int id)
    {
        m_skillsDesc.RefreshSkills();
       
        var skillCfg = m_skillsDesc.GetSkillConfig(id);
        m_skillPlayer.Setup(skillCfg);
        m_skillPlayer.Start();
    }
}
