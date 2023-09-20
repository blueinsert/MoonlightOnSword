using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasicAblitity {
    void PlayAnim(string animName);

    void SetAnimSpeed(float speed);

    Vector2 GetVel();

    void SetVel(Vector2 vel);
}
