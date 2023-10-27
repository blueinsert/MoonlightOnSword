using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 只是表示具有根据输入进行移动的能力
/// 逻辑在SystemBehaviorMove
/// </summary>
public class BehaviorMoveComp : BehaviorCompBase {

    public BehaviorMoveComp() : base(BehaviorType.Moving)
    {

    }

    public override bool IsBehaviorActive()
    {
        throw new System.NotImplementedException();
    }

    public override void OnOtherBehaviorEnter(BehaviorType other)
    {
        throw new System.NotImplementedException();
    }

    public override void Tick()
    {
        base.Tick();
    }
}
