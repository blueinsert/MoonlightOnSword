using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CaredCompType(typeof(BehaviorBlockComp))]
public class BehaviorBlockSystem : SystemBase
{
    /// <summary>
    /// 需要综合多个comp的逻辑放在system,保持comp的纯数据性
    /// </summary>
    /// <param name="comp"></param>
    private void TickBlock(BehaviorBlockComp comp)
    {
        //Debug.Log(string.Format("BehaviorBlockSystem:TickBlock"));
        var fsm = comp.GetComp<BehaviorFsmComp>();
        var anim = comp.GetComp<AnimComp>();
        if (!fsm.CanBlock())
        {
            anim.Blocking(false);
            return; //在播放技能时，停止move逻辑
        }
        var input = comp.GetComp<InputComp>();
        var move = comp.GetComp<MoveComp>();
        //var anim = comp.GetComp<AnimComp>();
        var block = comp.GetComp<BehaviorBlockComp>();
        if (input == null || !input.IsEnable) {
            return;
        }

        if (input.IsBlcokHoldon)
        {
            if (!block.IsInBlocking)
            {
                block.StartBlock();
                move.SetPreferVelHorizon(0, 0);
                anim.Blocking(true);
            }
            else
            {
                block.Refresh();
            }
        }
        else
        {
            if (block.m_status == BlockStatus.BlockEnd)
            {
                anim.Blocking(false);
            }
        }
    }

    public override void Tick()
    {
        //Debug.Log(string.Format("BehaviorMoveSystem:Tick comp Count:{0}", m_compList.Count));
        //
        foreach (var comp in m_compList)
        {
            if (comp.IsEnable)
            {
                TickBlock(comp as BehaviorBlockComp);
            }
        }
        base.Tick();
    }
}
