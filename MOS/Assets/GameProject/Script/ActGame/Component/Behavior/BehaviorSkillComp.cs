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
    }

    public override void Tick()
    {
        base.Tick();
        m_skillPlayer.Tick();
    }

    public bool TryAttack()
    {
        Debug.Log("BehaviorSkillComp:TryAttack");
        TryPlaySkill(1);
        return false;
    }

    public bool TryPlaySkill(int id, int from = -1)
    {
        var move = this.GetComp<MoveComp>();
        var anim = this.GetComp<AnimComp>();
        var input = this.GetComp<InputComp>();
        var skillCfg = m_skillsDesc.GetSkillConfig(id);
        m_skillPlayer.Initialize(anim, move, input, this);
        m_skillPlayer.Setup(skillCfg);
        m_skillPlayer.Start();
        return true;
    }

    public void ForcePlaySkill(int id)
    {
        m_skillsDesc.RefreshSkills();
        var move = this.GetComp<MoveComp>();
        var anim = this.GetComp<AnimComp>();
        var input = this.GetComp<InputComp>();
        var skillCfg = m_skillsDesc.GetSkillConfig(id);
        m_skillPlayer.Initialize(anim, move, input, this);
        m_skillPlayer.Setup(skillCfg);
        m_skillPlayer.Start();
    }
}
