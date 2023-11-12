using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlaySkill : BNodeAction
{
    [SerializeField]
    public int m_skillId;
    [SerializeField]
    public float m_duration;

    public ActionPlaySkill()
        : base()
    {
        this.m_strName = "PlaySkill";
    }

    public override string GetDesc()
    {
        return string.Format("skill:{0}", m_skillId);
    }

    public override void OnEnter(BInput input)
    {
        base.OnEnter(input);
        AIInput tinput = input as AIInput;
        tinput.PlaySkill(m_skillId);
    }

    protected override float GetDuration()
    {
        return m_duration;
    }

    //excute
    public override ActionResult Excute(BInput input)
    {
        if (IsFinish())
            return ActionResult.SUCCESS;
        return ActionResult.RUNNING;
    }
}
