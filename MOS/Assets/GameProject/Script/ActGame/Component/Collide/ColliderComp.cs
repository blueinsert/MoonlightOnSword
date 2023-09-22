using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责初始化collider (CollidersDesc AttackColliderDesc DefenseColliderDesc)
/// 及碰撞查询
/// 不需要tick
/// </summary>
public class ColliderComp : ComponentBase {

    public CollidersDesc m_collidersDesc;

    public List<AttackColliderDesc> m_attackColliders = new List<AttackColliderDesc>();
    public List<DefenseColliderDesc> m_defenseColliders = new List<DefenseColliderDesc>();

    public List<ColliderTriggerInfo> m_cacheTriggerInfoList = new List<ColliderTriggerInfo>();

    public void Start()
	{
        var layerMask1 = LayerMask.NameToLayer("PlayerAttackCollider");
        var layerMask2 = LayerMask.NameToLayer("EnemyAttackCollider");
        var layerMask3 = LayerMask.NameToLayer("PlayerDefenseCollider");
        var layerMask4 = LayerMask.NameToLayer("EnemyDefenseCollider");
        m_collidersDesc = this.GetComponentInChildren<CollidersDesc>();
        var entityComp = GetComp<EntityComp>();
        var campType = entityComp.CampType;

        foreach (var coll in m_collidersDesc.m_attackBoxList)
        {
            var c =coll.gameObject.AddComponent<AttackColliderDesc>();
            c.Init();
            m_attackColliders.Add(c);
            c.gameObject.layer = campType == CampType.Player ? layerMask1 : layerMask2;
        }
        foreach (var coll in m_collidersDesc.m_defenceBoxList)
        {
            var c = coll.gameObject.AddComponent<DefenseColliderDesc>();
            c.Init();
            m_defenseColliders.Add(c);
            c.gameObject.layer = campType == CampType.Player ? layerMask3 : layerMask4;
        }
    }

    public void ClearTriggerInfos()
    {
        foreach(var attackCollider in m_attackColliders)
        {
            attackCollider.ClearTriggersInfo();
        }
    }

    public List<ColliderTriggerInfo> GetTriggerInfos()
    {
        m_cacheTriggerInfoList.Clear();
        foreach (var attackCollider in m_attackColliders)
        {
            for(int i = 0; i < attackCollider.m_triggersLen; i++)
            {
                m_cacheTriggerInfoList.Add(attackCollider.m_triggerInfos[i]);
            }
        }
        return m_cacheTriggerInfoList;
    }

    public void EnableCollider(string name)
    {

    }

    public void DisableCollider(string name)
    {

    }
}
