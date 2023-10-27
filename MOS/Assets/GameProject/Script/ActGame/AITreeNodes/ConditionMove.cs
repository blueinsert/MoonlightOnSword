using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionMove : BNodeCondition
{

	public ConditionMove() : base()
    {
        this.m_strName = "ConditionMove";
    }

    public override void OnEnter(BInput input)
    {
        base.OnEnter(input);
    }

    public override ActionResult Excute(BInput input)
    {
        var aiInput = input as AIInput;
        if (aiInput.CanMove())
        {
            return ActionResult.SUCCESS;
        }
        return ActionResult.FAILURE;
    }
}
