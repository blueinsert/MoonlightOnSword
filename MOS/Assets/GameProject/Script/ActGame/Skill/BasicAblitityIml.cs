using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAblitityIml : IBasicAblitity {

    public AnimComp m_animComp;
    public MoveComp m_moveComp;
    public InputComp m_inputComp;
    public BehaviorSkillComp m_skillComp;
    public ColliderComp m_colliderComp;
    public HitDetectionComp m_hitDetectionComp;
    public SkillPlayer m_skillPlayer;

    public void Initialize(SkillPlayer skillPlayer, EntityComp entity)
    {
        m_skillPlayer = skillPlayer;
        m_moveComp = entity.GetComp<MoveComp>();
        m_animComp = entity.GetComp<AnimComp>();
        m_inputComp = entity.GetComp<InputComp>();
        m_skillComp = entity.GetComp<BehaviorSkillComp>();
        m_colliderComp = entity.GetComp<ColliderComp>();
        m_hitDetectionComp = entity.GetComponent<HitDetectionComp>();
    }

    #region IBasicAbility
    public void PlayAnim(string name)
    {
        m_animComp.PlayAnim(name);
    }

    public Vector2 GetVel()
    {
        return m_moveComp.VelPreferHorizon;
    }

    public void SetVelH(Vector2 vel)
    {
        m_moveComp.SetPreferVelHorizon(vel.x, vel.y);
    }

    public void SetAnimSpeed(float speed)
    {
        m_animComp.SetSpeed(speed);
    }

    public bool IsAttackingClick()
    {
        return m_inputComp.IsAttackClick;
    }

    public void PlaySkill(int id)
    {
        m_skillPlayer.SetNextSkill(id);
    }

    public Vector2 GetFacing()
    {
        return m_moveComp.Facing;
    }

    public void SetVelV(float v)
    {
        m_moveComp.SetPreferVelVertical(v);
    }

    public void SetCollider(string name, bool isEnable)
    {
        m_colliderComp.SetColliderEnable(name, isEnable);
    }

    public void SetHitDef(HitDef hitDef)
    {
        m_hitDetectionComp.SetHitDef(hitDef);
    }
    #endregion
}
