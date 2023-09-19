using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasicAblitity {
    void PlayAnim(string animName);

    Vector2 GetVel();

    void SetVel(Vector2 vel);
}
