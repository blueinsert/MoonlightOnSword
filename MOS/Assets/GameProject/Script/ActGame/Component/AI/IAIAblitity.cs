using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIAblitity {
    bool IsEnemyValid();
    bool IsMoving();
    bool IsNearTo(Vector3 position, float range);
    bool CanMove();

    bool FindAndCacheEnemy(float range);
    float DistToEnemy();
    void AbandonEnemy();
    bool Attack();
    void MoveTo(Vector3 targetPos, float speed);
    void PlaySkill(int id);
    bool MoveToEnemy(float speed);
    void StopMove();

}
