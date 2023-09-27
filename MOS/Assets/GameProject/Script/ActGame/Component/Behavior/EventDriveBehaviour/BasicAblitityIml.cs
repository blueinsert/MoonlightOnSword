using System;
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

    public void Initialize(EntityComp entity)
    {
        m_moveComp = entity.GetComp<MoveComp>();
        m_animComp = entity.GetComp<AnimComp>();
        m_inputComp = entity.GetComp<InputComp>();
        m_skillComp = entity.GetComp<BehaviorSkillComp>();
        m_colliderComp = entity.GetComp<ColliderComp>();
        m_hitDetectionComp = entity.GetComponent<HitDetectionComp>();
    }

    #region IBasicAbility
    public void PlayAnim(string name,bool useTrigger)
    {
        m_animComp.PlayAnim(name,useTrigger);
    }

    public Vector2 GetVel()
    {
        return m_moveComp.VelPreferHorizon;
    }

    public void SetVelH(Vector2 vel, bool? isSelfDrive)
    {
        m_moveComp.SetPreferVelHorizon(vel.x, vel.y, isSelfDrive);
    }

    public void SetAnimSpeed(float speed)
    {
        m_animComp.SetSpeed(speed);
    }

    public bool IsAttackingClick()
    {
        return m_inputComp.IsAttackClick;
    }

    public virtual void PlaySkill(int id)
    {
        throw new NotImplementedException();
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

    public void FreezeAnim()
    {
        m_animComp.Freeze();
    }

    public void UnFreezeAnim()
    {
        m_animComp.UnFreeze();
    }

    public void EnableMove()
    {
        m_moveComp.m_isEnable = true;
    }

    public void DisableMove()
    {
        m_moveComp.m_isEnable = false;
    }
    #endregion
}
