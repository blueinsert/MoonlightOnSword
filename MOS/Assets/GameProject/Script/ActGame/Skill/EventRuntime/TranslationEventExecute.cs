using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TranslationEventExecute : EventRuntimeBase
{

    private TranslationEvent m_e;

    public void Setup(TranslationEvent e)
    {
        m_e = e;
        var start = e.StartTime;
        var end = e.EndTime;
        SetStartTime(start);
        SetEndTime(end);
    }

    public override void OnStart()
    {

    }

    public override void OnEnd()
    {

    }

    private bool CheckCondition(TranslateCondition condition)
    {
        if(condition.Type.ToLower() == "cmd")
        {
            if(condition.Value.ToLower() == "a")
            {
                if (m_basicAblity.IsAttackingClick())
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected override void OnTick(float deltaTime)
    {
        bool isConditionAllPass = true;
        foreach(var condi in m_e.ConditionList)
        {
            if (!CheckCondition(condi))
            {
                isConditionAllPass = false;
                break;
            }
        }
        if (isConditionAllPass)
        {
            m_basicAblity.PlaySkill(m_e.To);
        }
    }
}
