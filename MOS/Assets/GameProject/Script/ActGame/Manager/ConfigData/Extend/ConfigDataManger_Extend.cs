using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ConfigDataManager : MonoBehaviour { 

    public List<CommonActionEvent> ParseCommonActionEvents(string content)
    {
        List<CommonActionEvent> res = new List<CommonActionEvent>();
        if (string.IsNullOrEmpty(content))
            return res;
        var splits = content.Split(new char[] { '|' });
        foreach (var split in splits)
        {
            var eles = split.Split(new char[] { '&' });
            CommonActionEvent ae = new CommonActionEvent();
            ae.m_startTime = int.Parse(eles[0]);
            ae.m_endTime = int.Parse(eles[1]);
            ae.m_cfgID = int.Parse(eles[2]);
            res.Add(ae);
        }
        return res;
    }
    public void PostInit()
    {
        foreach(var cfg in GetAllConfigDataActionConfig())
        {
            //cfg.m_actionEventDisplacements = ParseCommonActionEvents(cfg.Displacements);
            cfg.m_actionEventMotions = ParseCommonActionEvents(cfg.Motions);
            cfg.m_actionEventHitDefs = ParseCommonActionEvents(cfg.HitDefs);
        }
        foreach(var cfg in GetAllConfigDataActionTransferConditionConfig())
        {
            var type = (StateTransferConditionType)Enum.Parse(typeof(StateTransferConditionType), cfg.Type);
            cfg.m_transferCondition = new StateTransferCondition(type, cfg.Param1, cfg.Param2, cfg.Param3, cfg.Param4, cfg.Param5);
        }
        foreach(var cfg in GetAllConfigDataActionTransferConfig())
        {
            int from = cfg.From;
            int to = cfg.To;
            var fromAction = GetConfigDataActionConfig(from);
            if (!fromAction.m_nextActionDic.ContainsKey(to))
            {
                fromAction.m_nextActionDic.Add(to, new List<StateTransferCondition>());
            }
            List<int> conditions = new List<int>();
            conditions.Add(cfg.Condition1);
            conditions.Add(cfg.Condition2);
            conditions.Add(cfg.Condition3);
            conditions.Add(cfg.Condition4);
            conditions.Add(cfg.Condition5);
            for(int i = 0; i < conditions.Count; i++)
            {
                var conditionId = conditions[i];
                if (conditionId != 0)
                {
                    var conditionCfg = GetConfigDataActionTransferConditionConfig(conditionId);
                    fromAction.m_nextActionDic[to].Add(conditionCfg.m_transferCondition);
                }
            }
        }
    }
}

public partial class ActionTransferConditionConfig : ConfigDataBase {
    public StateTransferCondition m_transferCondition;
}

public partial class ActionConfig : ConfigDataBase
{
    public Dictionary<int, List<StateTransferCondition>> m_nextActionDic = new Dictionary<int, List<StateTransferCondition>>();
    public List<ActionEventDisplacementDS> m_actionEventDisplacements = new List<ActionEventDisplacementDS>();
    public List<CommonActionEvent> m_actionEventMotions = new List<CommonActionEvent>();
    public List<CommonActionEvent> m_actionEventHitDefs = new List<CommonActionEvent>();
}

