﻿using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMoveToPosition : BNodeAction
{
    public Vector3 m_targetPosition;
    public const float MaxDuration = 20f;
    private float m_startTime;

    public ActionMoveToPosition()
        : base()
    {
        this.m_strName = "MoveToPosition";
    }

    public override void OnEnter(BInput input)
    {
        AIInput tinput = input as AIInput;
        tinput.MoveTo(m_targetPosition);
        this.m_startTime = TimeManger.Instance.CurTime;
        Debug.Log("MoveToPosition");
    }

    //excute
    public override ActionResult Excute(BInput input)
    {
        var cur = TimeManger.Instance.CurTime;
        if (cur > m_startTime + MaxDuration)
            return ActionResult.FAILURE;
        var aiInput = input as AIInput;
        if (aiInput.IsNearTo(m_targetPosition, 0.5f)){
            return ActionResult.SUCCESS;
        }
        return ActionResult.RUNNING;
    }
}
