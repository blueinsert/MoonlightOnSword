using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNearToPosition : BNodeCondition
{
    public Vector3 m_targetPos;
    public float m_range;

	public ConditionNearToPosition() : base()
    {
        this.m_strName = "ConditionNearToPosition";
    }

    public override void OnEnter(BInput input)
    {
        base.OnEnter(input);
    }

    public override ActionResult Excute(BInput input)
    {
        var aiInput = input as AIInput;
        if (aiInput.IsNearTo(m_targetPos, m_range))
        {
            return ActionResult.SUCCESS;
        }
        return ActionResult.FAILURE;
    }
}
