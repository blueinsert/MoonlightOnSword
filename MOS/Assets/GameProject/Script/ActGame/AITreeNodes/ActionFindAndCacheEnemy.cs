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

    public override void OnEnter(BInput input)
    {
        Debug.Log(string.Format("ActionFindAndCacheEnemy:OnEnter"));
    }

    //excute
    public override ActionResult Excute(BInput input)
    {
        AIInput aiInput = input as AIInput;
        Debug.Log(string.Format("ActionFindAndCacheEnemy:Excute"));
        if (aiInput.FindAndCacheEnemy(m_range))
            return ActionResult.SUCCESS;
        return ActionResult.RUNNING;
    }

}
