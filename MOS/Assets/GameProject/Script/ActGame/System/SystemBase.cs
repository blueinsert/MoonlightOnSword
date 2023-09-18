using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// System的作用是批次更新组件
/// 以及处理组件间相互有联系的逻辑
/// </summary>
public abstract class SystemBase : BehaviourBase{

	public List<ComponentBase> m_compList = new List<ComponentBase>();

    private List<ComponentBase> m_newAdds = new List<ComponentBase>();

    public void Awake()
    {
        ActGame.Instance.RegisterSystem(this);
    }

    public override void Tick()
    {
        foreach(var comp in m_compList)
        {
            if(comp.IsEnable)
                comp.Tick();
        }
        foreach(var comp in m_newAdds)
        {
            m_compList.Add(comp);
        }
        m_newAdds.Clear();
    }

    public void AddComp(ComponentBase comp)
    {
        m_newAdds.Add(comp);
    }

}
