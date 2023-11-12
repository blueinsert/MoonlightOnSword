using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BNodeProbality : BNodeDecorator
{
    [SerializeField]
    public float m_value;

    private float m_v;

    public BNodeProbality()
        : base()
    {
        this.m_strName = "Probality";
    }

    public override string GetDesc()
    {
        return string.Format("{0}%", (int)(m_value * 100));
    }

    //onenter
    public override void OnEnter(BInput input)
    {
        var r = Random.Range(0, 100);
        m_v = r / 100f;
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
        if (m_v < m_value) {
            ActionResult res = this.m_lstChildren[0].RunNode(input);
            if (res != ActionResult.RUNNING)
                return res;

            return ActionResult.RUNNING;
        }
        return ActionResult.FAILURE;
    }

}
