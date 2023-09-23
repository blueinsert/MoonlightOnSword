using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class HitDefSetEventExecute : EventExecuteBase
{
    private HitDefSetEvent m_e;

    public override void Setup(EventBase e)
    {
        m_e = e as HitDefSetEvent;
        base.Setup(e);
    }

    public override void OnStart()
    {
        m_basicAblity.SetHitDef(m_e.HitDef);
    }

    public override void OnEnd()
    {
        m_basicAblity.SetHitDef(null);
    }

    protected override void OnTick(float deltaTime)
    {
       
    }
}

