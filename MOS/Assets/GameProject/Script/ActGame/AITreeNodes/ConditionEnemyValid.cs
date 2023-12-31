﻿using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionEnemyValid : BNodeCondition
{
    public ConditionEnemyValid() : base()
    {
        this.m_strName = "IsEnemyValid";
    }

    public override void OnEnter(BInput input)
    {
        Debug.Log("ConditionEnemyValid:OnEnter");
        base.OnEnter(input);
    }

    public override ActionResult Excute(BInput input)
    {
        var aiInput = input as AIInput;
        bool isValid = aiInput.IsEnemyValid();
        Debug.Log(string.Format("ConditionEnemyValid:Excute isValid:{0}",isValid));
        if (isValid)
        {
            return ActionResult.SUCCESS;
        }
        return ActionResult.FAILURE;
    }
}
