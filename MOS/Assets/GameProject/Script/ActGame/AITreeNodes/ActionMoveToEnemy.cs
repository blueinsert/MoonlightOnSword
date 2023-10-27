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

    private const float MaxDuration = 20f;
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
        Debug.Log("MoveToEnemy");
    }

    //excute
    public override ActionResult Excute(BInput input)
    {
        var cur = TimeManger.Instance.CurTime;
        if (cur > m_startTime + MaxDuration)
            return ActionResult.FAILURE;
        var aiInput = input as AIInput;
        if (aiInput.DistToEnemy() < 0.5f)
        {
            return ActionResult.SUCCESS;
        }
        if (aiInput.IsMoving())
            return ActionResult.RUNNING;
        else
            return ActionResult.FAILURE;
    }
}
