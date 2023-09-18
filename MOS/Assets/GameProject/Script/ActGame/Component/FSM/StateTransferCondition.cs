using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTransferConditionType { 
	Input,
	ActionTime,
	Finish,
}

public class StateTransferCondition  {
	public StateTransferConditionType m_type;
	public string m_commandName;
	public float m_actionTimeStart;
	public float m_actionTimeEnd;

	public StateTransferCondition(StateTransferConditionType type, string param1, string param2, string param3, string param4, string param5)
	{
		m_type = type;
		if(m_type == StateTransferConditionType.Input)
		{
			m_commandName = param1;
		}else if(m_type == StateTransferConditionType.ActionTime)
		{
			m_actionTimeStart = int.Parse(param1) / 1000f;
			m_actionTimeEnd = int.Parse(param2) / 1000f;
		}else if(m_type == StateTransferConditionType.Finish)
		{

		}
	}

	private bool CheckInput(ActorBase actor)
	{
		var cmdComp = actor.GetComponent<CmdComp>();
		var res = cmdComp.IsCmdActive(m_commandName);
		return res;
	}

	private bool CheckActionTime(ActorBase actor)
	{
		var fsmComp = actor.GetComponent<FsmComp>();
		if (!fsmComp.IsActionPlaying())
		{
			return false;
		}
		var actionTime = fsmComp.GetActionTime();
		if(actionTime > m_actionTimeStart && actionTime > m_actionTimeEnd)
		{
			return true;
		}
		return false;
	}

	private bool CheckIsFinish(ActorBase actor)
	{
		var fsmComp = actor.GetComponent<FsmComp>();
		var res = fsmComp.IsActionFinish();
		return res;
	}

	public bool CheckCondition(ActorBase actor)
	{
		if(m_type == StateTransferConditionType.Input)
		{
			return CheckInput(actor);
		}
		if(m_type == StateTransferConditionType.ActionTime)
		{
			return CheckActionTime(actor);
		}
		if(m_type == StateTransferConditionType.Finish)
		{
			return CheckIsFinish(actor);
		}
		return false;
	}

}
