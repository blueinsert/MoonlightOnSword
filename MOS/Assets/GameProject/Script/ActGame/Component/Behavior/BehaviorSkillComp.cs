using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSkillComp : ComponentBase
{
    public bool IsPlaying { get { return m_skillPlayer.IsStart && !m_skillPlayer.IsEnd; } }
    [SerializeField]
    public SkillsDesc m_skillsDesc;

    public SkillPlayer m_skillPlayer = new SkillPlayer();

    public override void Tick()
    {
        base.Tick();
        m_skillPlayer.Tick();
    }

    public bool TryAttack()
    {
        Debug.Log("BehaviorSkillComp:TryAttack");
        var move = this.GetComp<MoveComp>();
        var anim = this.GetComp<AnimComp>();
        var skillCfg = m_skillsDesc.GetSkillConfig(0);
        m_skillPlayer.Initialize(anim, move);
        m_skillPlayer.Setup(skillCfg);
        m_skillPlayer.Start();
        return false;
    }
}
