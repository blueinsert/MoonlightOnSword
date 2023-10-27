using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionEnemyValid : BNodeCondition
{
    public ConditionEnemyValid() : base()
    {
        this.m_strName = "ConditionEnemyValid";
    }

    public override void OnEnter(BInput input)
    {
        base.OnEnter(input);
    }

    public override ActionResult Excute(BInput input)
    {
        var aiInput = input as AIInput;
        if (aiInput.IsEnemyValid())
        {
            return ActionResult.SUCCESS;
        }
        return ActionResult.FAILURE;
    }
}
