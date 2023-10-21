using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIAblitity {

    bool CanMove();
	bool Attack();
    void MoveTo(Vector3 targetPos, float speed);
    bool IsMoving();
    bool IsNearTo(Vector3 position, float range);
}
