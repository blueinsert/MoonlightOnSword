using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.AIBehaviorTree;

public class ActionAttack : BNodeAction {

    private bool over;
    private float m_ftime;

    public ActionAttack()
        : base()
    {
        this.m_strName = "Attack";
    }

    public override void OnEnter(BInput input)
    {
        AIInput tinput = input as AIInput;
        tinput.Attack();
        this.m_ftime = Time.time;
        this.over = false;
        Debug.Log("AI attack");
    }

    //excute
    public override ActionResult Excute(BInput input)
    {
        if (Time.time - this.m_ftime > 2f)
            this.over = true;
        if (this.over)
            return ActionResult.SUCCESS;
        return ActionResult.RUNNING;
    }

}
