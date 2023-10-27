using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStopMove : BNodeAction
{

    public ActionStopMove()
        : base()
    {
        this.m_strName = "StopMove";
    }

    public override void OnEnter(BInput input)
    {
        Debug.Log(string.Format("ActionStopMove:OnEnter"));
    }

    //excute
    public override ActionResult Excute(BInput input)
    {
        Debug.Log(string.Format("ActionStopMove:Excute"));
        AIInput tinput = input as AIInput;
        tinput.StopMove();
        return ActionResult.SUCCESS;
    }
}
