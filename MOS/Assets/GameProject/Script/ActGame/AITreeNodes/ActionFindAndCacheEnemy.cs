using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFindAndCacheEnemy : BNodeAction
{
    [SerializeField]
    public float m_range;

    public ActionFindAndCacheEnemy()
       : base()
    {
        this.m_strName = "ActionFindAndCacheEnemy";
    }

    public override string GetDesc()
    {
        return string.Format("range:{0}", m_range);
    }

    public override void OnEnter(BInput input)
    {
        Debug.Log(string.Format("ActionFindAndCacheEnemy:OnEnter"));
        AIInput aiInput = input as AIInput;
        aiInput.FindAndCacheEnemy(m_range);
    }

    //excute
    public override ActionResult Excute(BInput input)
    {
        Debug.Log(string.Format("ActionFindAndCacheEnemy:Excute"));
        if (IsFinish())
            return ActionResult.SUCCESS;
        return ActionResult.RUNNING;
    }

}
