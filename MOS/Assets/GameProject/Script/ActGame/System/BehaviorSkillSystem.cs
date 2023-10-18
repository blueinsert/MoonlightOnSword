using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CaredCompType(typeof(BehaviorSkillComp))]
public class BehaviorSkillSystem : SystemBase
{
    private void TickSkill(BehaviorSkillComp comp)
    {
        //Debug.Log(string.Format("BehaviorSkillSystem:TickSkill"));
        var input = comp.GetComp<InputComp>();
        var move = comp.GetComp<MoveComp>();
        var anim = comp.GetComp<AnimComp>();

        if (input != null && input.IsAttackClick)
        {
            comp.TryAttack();
        }
    }

    public override void Tick()
    {
        //Debug.Log(string.Format("BehaviorSkillSystem:Tick comp Count:{0}", m_compList.Count));
        //
        foreach (var comp in m_compList)
        {
            if (comp.IsEnable && comp is BehaviorSkillComp)
            {
                TickSkill(comp as BehaviorSkillComp);
            }
        }
        base.Tick();
    }
}
