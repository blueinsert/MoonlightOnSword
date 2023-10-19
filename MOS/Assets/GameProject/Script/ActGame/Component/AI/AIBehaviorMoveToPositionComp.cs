using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviorMoveToPositionComp : BehaviorCompBase {

    public AIBehaviorMoveToPositionComp() : base(BehaviorType.Moving)
    {

    }

    public bool IsMoving { get { return m_isMoving; } }

    private bool m_isMoving = false;
    public MoveComp m_moveComp = null;
    public AnimComp m_animComp = null;

    public Vector3 m_targetPos = Vector3.zero;
    public float m_speed = 0;

	// Use this for initialization
	void Start () {
        m_moveComp = GetComp<MoveComp>();
        m_animComp = GetComp<AnimComp>();
	}

    public override void Tick()
    {
        if (m_isMoving)
        {
            var dir = m_targetPos - this.transform.position;
            var dist = dir.magnitude;
            dir.y = 0;
            dir.Normalize();

            var speed = m_speed;
            var preferVel = dir * speed;
            m_moveComp.SetPreferVelHorizon(preferVel.x, preferVel.z, true);
            m_animComp.Walk(m_moveComp.Speed, m_moveComp.RotateValue);
        }
    }

    public void MoveTo(Vector3 position, float speed)
    {
        m_isMoving = true;
        m_targetPos = position;
        m_speed = speed;
    }

    public void Stop()
    {
        m_isMoving = false;
    }

    public override bool IsBehaviorActive()
    {
        return m_isMoving;
    }

    public override void OnOtherBehaviorEnter(BehaviorType other)
    {
        if(other == BehaviorType.GetHit || other == BehaviorType.Guard || other == BehaviorType.Skill)
        {
            Stop();
        }
    }
}
