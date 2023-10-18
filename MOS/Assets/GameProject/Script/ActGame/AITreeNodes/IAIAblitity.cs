using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIAblitity {

	bool Attack();
    void MoveTo(Vector3 targetPos);
    bool IsNearTo(Vector3 position, float range);
}
