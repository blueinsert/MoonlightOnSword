using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionCanMove : BNodeCondition
{

	public ConditionCanMove() : base()
    {
        this.m_strName = "CanMove";
    }

    public override void OnEnter(BInput input)
    {
        base.OnEnter(input);
        Debug.Log(string.Format("ConditionMove:OnEnter"));
    }

    public override ActionResult Excute(BInput input)
    {
        var aiInput = input as AIInput;
        Debug.Log(string.Format("ConditionMove:Excute"));
        if (aiInput.CanMove())
        {
            return ActionResult.SUCCESS;
        }
        return ActionResult.FAILURE;
    }
}
