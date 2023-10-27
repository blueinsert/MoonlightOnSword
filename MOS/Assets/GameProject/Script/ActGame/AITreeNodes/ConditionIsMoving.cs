using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ConditionIsMoving : BNodeCondition
{
    public ConditionIsMoving() : base()
    {
        this.m_strName = "IsMoveing";
    }

    public override void OnEnter(BInput input)
    {
        base.OnEnter(input);
        Debug.Log(string.Format("ConditionIsMoving:OnEnter"));
    }

    public override ActionResult Excute(BInput input)
    {
        var aiInput = input as AIInput;
        Debug.Log(string.Format("ConditionIsMoving:Excute"));
        if (aiInput.IsMoving())
        {
            return ActionResult.SUCCESS;
        }
        return ActionResult.FAILURE;
    }
}