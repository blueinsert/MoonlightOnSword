using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CaredCompType(typeof(MoveComp))]
public class MoveSystem : SystemBase {

    public override void Tick()
    {
        Debug.Log(string.Format("MoveSystem:Tick comp Count:{0}", m_compList.Count));
        base.Tick();
    }
}
