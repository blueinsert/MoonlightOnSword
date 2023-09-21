using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CaredCompType(typeof(BehaviorMoveComp))]
public class BehaviorMoveSystem : SystemBase
{
    /// <summary>
    /// 需要综合多个comp的逻辑放在system,保持comp的纯数据性
    /// </summary>
    /// <param name="comp"></param>
    private void TickMove(BehaviorMoveComp comp)
    {
        var fsm = comp.GetComp<BehaviorFsmComp>();
        if (!fsm.CanMove())
        {
            return; //在播放技能时，停止move逻辑
        }
        var input = comp.GetComp<InputComp>();
        var move = comp.GetComp<MoveComp>();
        var anim = comp.GetComp<AnimComp>();
        float speed = 5f;
        var forward = CameraManager.Instance.GetForward();//摄像机朝向
        var inputDir = input.Dir;//摇杆输入
        //if (input.Dir.x != 0 || input.Dir.y != 0)
        {
            var left = -Vector3.Cross(forward, Vector2.up);
            left = left.normalized;
            //举例：摇杆方向(1,0)使得向左边运动,左边指相对摄像机朝向的左边
            var preferDir = forward * inputDir.y + left * inputDir.x;
            preferDir = preferDir.normalized;
            var preferVel = preferDir * speed * inputDir.magnitude;
            move.SetPreferVelHorizon(preferVel.x, preferVel.z);

            anim.Walk(move.Speed, move.RotateValue);
        }
    }

    public override void Tick()
    {
        //Debug.Log(string.Format("BehaviorMoveSystem:Tick comp Count:{0}", m_compList.Count));
        //
        foreach(var comp in m_compList)
        {
            if(comp.IsEnable && comp is BehaviorMoveComp)
            {
                TickMove(comp as BehaviorMoveComp);
            }
        }
        base.Tick();
    }
}

