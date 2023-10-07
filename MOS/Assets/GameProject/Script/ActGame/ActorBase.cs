using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBase : MonoBehaviour {

	public bool m_isMunual;
	public int m_configId;
	public CampType m_campType;

	public ActorConfig m_config;

	protected List<ComponentBase> m_componentList = new List<ComponentBase>();
	protected Dictionary<Type, ComponentBase> m_componentDic = new Dictionary<Type, ComponentBase>();
	protected CharacterController m_characterController;

	protected ColliderHandlerComp m_hitComp;
	protected MotionComp m_motionComp;
	protected MoveComp m_moveComponent;
	protected FsmComp m_fsmComponent;
	protected AnimComp m_animComponent;
	protected InputComp m_playerInputCom;
	protected CmdComp m_cmdComp;
	protected PropertyComp m_propertyComp;
	public CharacterController CharacterController { get { return m_characterController; } }

	public T GetOrCreateComponent<T>() where T:Component
	{
		var component = GetComponent<T>();
		if (component == null)
		{
			component = this.gameObject.AddComponent<T>();
		}
		if(component is ComponentBase)
		{
			m_componentDic.Add(typeof(T), component as ComponentBase);
			m_componentList.Add(component as ComponentBase);
		}
		return component;
	}

	private void LoadModel()
	{
		if (m_config != null)
		{
			var model = m_config.Model;
			var prefab = ResourceManager.Instance.LoadAsset<GameObject>(model);
			var go = GameObject.Instantiate(prefab, this.transform);
			go.transform.localPosition = Vector3.zero;
			go.transform.rotation = Quaternion.identity;
		}
	}

	// Use this for initialization
	void Start () {
		Init();
		OnInitComplete();
	}

	protected virtual void OnInitComplete()
	{
		ActGame.Instance.AddActor(this);
	}

	protected virtual void Init() {
		m_config = ConfigDataManager.Instance.GetConfigDataActorConfig(m_configId);

		LoadModel();
		InitComponents();
		foreach (var pair in m_componentDic)
		{
			pair.Value.Init(this);
		}
		foreach (var pair in m_componentDic)
		{
			pair.Value.PostInit();
		}
	}

	protected virtual void InitComponents()
	{
		m_characterController = GetOrCreateComponent<CharacterController>();
		m_characterController.gameObject.layer = m_campType == CampType.Player1 ? LayerMask.NameToLayer("Player") : LayerMask.NameToLayer("Enemy");
		m_fsmComponent = GetOrCreateComponent<FsmComp>();
		m_moveComponent = GetOrCreateComponent<MoveComp>();
		m_animComponent = GetOrCreateComponent<AnimComp>();
		if(m_campType == CampType.Player1)
		   m_playerInputCom = GetOrCreateComponent<InputComp>();
		m_cmdComp = GetOrCreateComponent<CmdComp>();
		m_propertyComp = GetOrCreateComponent<PropertyComp>();
		var cfg = ConfigDataManager.Instance.GetConfigDataActorPropertyConfig(m_config.PropertyID);
		m_propertyComp.SetParams(cfg.WalkSpeed, cfg.RunSpeed, cfg.TurnSpeed);
		m_motionComp = GetOrCreateComponent<MotionComp>();
		m_hitComp = GetOrCreateComponent<ColliderHandlerComp>();
	}

	public void Tick() {
		foreach(var comp in m_componentList)
		{
			if (comp.enabled)
			{
				comp.Tick();
			}
		}
		/*
		if (m_playerInputCom != null && m_playerInputCom.enabled) m_playerInputCom.Tick();
		if (m_cmdComp.enabled) m_cmdComp.Tick();
		if (m_fsmComponent.enabled) m_fsmComponent.Tick();
		if (m_animComponent.enabled) m_animComponent.Tick();
		if (m_motionComp.enabled) m_motionComp.Tick();
		if (m_moveComponent.enabled) m_moveComponent.Tick();
		if (m_propertyComp.enabled) m_propertyComp.Tick();
		if (m_hitComp.enabled) m_hitComp.Tick();
		*/
	}

    #region 公共方法

	public void OnHitTarget(HitEffectConfig hitDef)
	{
		m_fsmComponent.OnHitTarget(hitDef);
	}

	public void OnBeHit(HitEffectConfig hitDef, DamageCalcResult damageRes)
	{
		m_fsmComponent.OnBeHit(hitDef, damageRes);
	}

    public CampType GetCampType()
	{
		return m_campType;
	}

	public ColliderHandlerComp GetHitComp()
	{
		return m_hitComp;
	}

	public PropertyComp GetPropertyComp()
	{
		return m_propertyComp;
	}
    #endregion

}
