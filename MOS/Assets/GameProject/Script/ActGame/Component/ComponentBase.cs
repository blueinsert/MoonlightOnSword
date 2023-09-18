using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentBase : BehaviourBase {

    protected ActorBase m_owner;

    public bool IsEnable { get { return m_isEnable; } }
    public bool m_isEnable = true;

    public bool m_isDestroying = false;

    public void Awake()
    {
        ActGame.Instance.RegisterComponent(this);
    }

    public object GetOwner()
    {
        return this.gameObject;
    }

	public virtual void Init(ActorBase actor)
    {
        m_owner = actor;
    }

    public virtual void PostInit()
    {

    }

    public T GetComp<T>() where T : ComponentBase
    {
        return ActGame.Instance.GetComp<T>(GetOwner());
    }
}
