using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CaredCompType(typeof(AnimComp))]
public class AnimSystem : SystemBase
{

    public override void Tick()
    {
        Debug.Log(string.Format("AnimSystem:Tick comp Count:{0}", m_compList.Count));
        base.Tick();
    }
}
