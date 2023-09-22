using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasicAblitity {
    void PlaySkill(int id);

    bool IsAttackingClick();

    void PlayAnim(string animName);

    void SetAnimSpeed(float speed);

    Vector2 GetVel();

    Vector2 GetFacing();

    void SetVelH(Vector2 vel);

    void SetVelV(float v);
}
