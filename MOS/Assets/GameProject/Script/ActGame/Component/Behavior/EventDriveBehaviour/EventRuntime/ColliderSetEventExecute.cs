using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ColliderSetEventExecute : EventExecuteBase
{
    private ColliderSetEvent m_e;

    public override void Setup(EventBase e)
    {
        m_e = e as ColliderSetEvent;
        base.Setup(e);
    }

    public override void OnStart()
    {
        m_basicAblity.SetCollider(m_e.Name, true);
    }

    public override void OnEnd()
    {
        m_basicAblity.SetCollider(m_e.Name, false);
    }

    protected override void OnTick(float deltaTime)
    {

    }
}
