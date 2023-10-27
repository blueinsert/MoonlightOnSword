using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMoveToEnemy : BNodeAction
{
    [SerializeField]
    public float m_speed;
    [SerializeField]
    public float m_range;

    private const float Duration = 2f;
    private float m_startTime;

    public ActionMoveToEnemy()
        : base()
    {
        this.m_strName = "ActionMoveToEnemy";
    }

    public override void OnEnter(BInput input)
    {
        AIInput aiInput = input as AIInput;
        var res = aiInput.MoveToEnemy(m_speed);
        this.m_startTime = TimeManger.Instance.CurTime;
        Debug.Log(string.Format("ActionMoveToEnemy:OnEnter"));
    }

    //excute
    public override ActionResult Excute(BInput input)
    {
        Debug.Log(string.Format("ActionMoveToEnemy:Excute"));
        return ActionResult.SUCCESS;
        /*
        var cur = TimeManger.Instance.CurTime;
        if (cur > m_startTime + Duration)
            return ActionResult.SUCCESS;
        var aiInput = input as AIInput;
        var dist = aiInput.DistToEnemy();
        Debug.Log(string.Format("ActionMoveToEnemy:Excute dist:{0}", dist));
        if (dist < 0.5f)
        {
            return ActionResult.SUCCESS;
        }
        if (aiInput.IsMoving())
            return ActionResult.RUNNING;
        else
            return ActionResult.FAILURE;
        */
    }
}
