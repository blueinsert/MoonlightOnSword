using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//取反操作
public class BNodeNot : BNodeDecorator
{

    public BNodeNot()
           : base()
    {
        this.m_strName = "Not";
    }

    //onenter
    public override void OnEnter(BInput input)
    {
    }

    //exceute
    public override ActionResult Excute(BInput input)
    {
        if (m_lstChildren.Count == 0)
            return ActionResult.SUCCESS;
        if (m_lstChildren.Count > 1)
        {
            //not support
            return ActionResult.FAILURE;
        }
        ActionResult res = this.m_lstChildren[0].RunNode(input);
        if (res == ActionResult.SUCCESS)
            return ActionResult.FAILURE;
        else if(res == ActionResult.FAILURE)
        {
            return ActionResult.SUCCESS;
        }
        return ActionResult.RUNNING;
    }
}
