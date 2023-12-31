﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasicAblitity {
    void FreezeAnim();
    void UnFreezeAnim();

    void EnableMove();
    void DisableMove();


    void SetHitDef(HitDef hitDef);

    void SetCollider(string name, bool isEnable);

    void PlaySkill(int id);

    bool IsAttackingClick();

    void PlayAnim(string animName,bool useTrigger=false);

    void SetAnimSpeed(float speed);

    Vector2 GetVel();

    Vector2 GetFacing();

    void SetVelH(Vector2 vel,bool? isSelfDrive = null);

    void SetVelV(float v);

    void Initialize(EntityComp entity);
}
