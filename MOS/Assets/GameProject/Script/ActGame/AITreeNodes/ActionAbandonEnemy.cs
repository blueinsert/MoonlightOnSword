﻿using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAbandonEnemy : BNodeAction
{

    public ActionAbandonEnemy()
        : base()
    {
        this.m_strName = "AbandonEnemy";
    }

    public override void OnEnter(BInput input)
    {
        Debug.Log(string.Format("ActionAbandonEnemy:OnEnter"));
        base.OnEnter(input);
        AIInput tinput = input as AIInput;
        tinput.AbandonEnemy();
    }

    //excute
    public override ActionResult Excute(BInput input)
    {
        Debug.Log(string.Format("ActionAbandonEnemy:Excute"));
        if(IsFinish())
            return ActionResult.SUCCESS;
        return ActionResult.RUNNING;
    }
}
