using Game.AIBehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DistCompareType
{
    Less,
    Greater,
    Equal,
}

public class ConditionDistToEnemy : BNodeCondition
{
    [SerializeField]
    public DistCompareType m_compareType = DistCompareType.Less;
    [SerializeField]
    public float m_distance;

    public ConditionDistToEnemy() : base()
    {
        this.m_strName = "ConditionDistToEnemy";
    }

    private string DistCompareTypeToString(DistCompareType compareType)
    {
        switch (compareType)
        {
            case DistCompareType.Equal:
                return "=";
            case DistCompareType.Greater:
                return ">";
            case DistCompareType.Less:
                return "<";
        }
        return "";
    }

    public override string GetDesc()
    {
        return string.Format("{0}{1}", DistCompareTypeToString(m_compareType), m_distance);
    }

    public override void OnEnter(BInput input)
    {
        Debug.Log(string.Format("ConditionDistToEnemy:OnEnter"));
        base.OnEnter(input);
    }

    public override ActionResult Excute(BInput input)
    {
        
        var aiInput = input as AIInput;
        var dist = aiInput.DistToEnemy();
        Debug.Log(string.Format("ConditionDistToEnemy:Excute dist:{0}", dist));
        if (m_compareType == DistCompareType.Less)
        {
            if (dist < m_distance)
                return ActionResult.SUCCESS;
        }else if(m_compareType == DistCompareType.Greater)
        {
            if (dist > m_distance)
                return ActionResult.SUCCESS;
        }else if(m_compareType == DistCompareType.Equal)
        {
            if (dist == m_distance)
                return ActionResult.SUCCESS;
        }
        return ActionResult.FAILURE;
    }
}
