using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ActGame : MonoBehaviour {
	public static ActGame Instance;

	public GameObject m_systemRoot;

	private CameraManager m_cameraSystem;
	private DamageCalc m_dmgCalc = new DamageCalc();

    public int m_maxEntityId = 0;
    //compType - compIns
    private Dictionary<object, Dictionary<Type, ComponentBase>> m_compInsDic = new Dictionary<object, Dictionary<Type, ComponentBase>>();
	//compType - systemIns
	private Dictionary<Type, SystemBase> m_systemDic = new Dictionary<Type, SystemBase>();

    private List<SystemBase> m_systemList = new List<SystemBase>();

	public void RegisterSystem(SystemBase system)
	{
        var attributes = system.GetType().GetCustomAttributes(true);
        Type caredCompType = null;
        foreach(var attribute in attributes)
        {
            if(attribute is CaredCompTypeAttribute)
            {
                var caredCompTypeAttribute = attribute as CaredCompTypeAttribute;
                caredCompType = caredCompTypeAttribute.Type;
            }
        }
        if (caredCompType == null)
        {
            Debug.LogError(string.Format("ActGame:RegisterSystem {0} failed! CaredCompTypeAttribute is null", system.GetType().Name));
        }
		Debug.Log(string.Format("ActGame:RegisterSystem {0} success,care comp type:{1}", system.GetType().Name, caredCompType.Name));
		m_systemDic.Add(caredCompType, system);
        m_systemList.Add(system);
	}

	public void RegisterComponent(ComponentBase comp)
	{
		var type = comp.GetType();
		var owner = comp.GetOwner();
		if (!m_compInsDic.ContainsKey(owner))
		{
			m_compInsDic.Add(owner, new Dictionary<Type, ComponentBase>());
        }
		m_compInsDic[owner].Add(type, comp);

		if (m_systemDic.ContainsKey(type))
		{
			var system = m_systemDic[type];
			system.AddComp(comp);
        }
        else
        {
            Debug.LogWarning(string.Format("ActGame:RegisterComponent the comp {0} is not add to any system!", type.Name));
        }
    }

    public T GetComp<T>(object owner) where T : ComponentBase
    {
        var type = typeof(T);
        if (m_compInsDic.ContainsKey(owner))
        {
            if (m_compInsDic[owner].ContainsKey(type))
            {
                return m_compInsDic[owner][type] as T;
            }
        }
        return null;
    }

    public int AllocateEntityId()
    {
        return m_maxEntityId++;
    }

    void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for(int i=0;i<m_systemList.Count;i++)
        {
            var system = m_systemList[i];
            system.Tick();
        }
		TimeManger.Instance.Tick();
	}

	public void AddActor(ActorBase actor)
	{
		if (actor.m_isMunual)
		{
			CameraManager.Instance.BindTarget(actor.gameObject);
		}
	}

	private void ProcessHit(ActorBase attacker, ActorBase hiter, HitEffectConfig hitDef)
	{
		attacker.OnHitTarget(hitDef);
		var damageRes = m_dmgCalc.CalcDamage(attacker.GetPropertyComp(), hiter.GetPropertyComp(), hitDef);
		hiter.OnBeHit(hitDef, damageRes);
	}

	public void OnHitTarget(ActorBase attacker,ActorBase hiter, HitEffectConfig hitDef)
	{
		Debug.Log(string.Format("ActGame:OnHitTarget from:{0} to:{1}", attacker, hiter));
		ProcessHit(attacker, hiter, hitDef);
	}
}
