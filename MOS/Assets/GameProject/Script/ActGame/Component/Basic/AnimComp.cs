using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimComp : ComponentBase {

	public Animator m_animator;

	public float m_lastSpeed = 1;

	public void Start()
	{
		PostInit();
    }

	public override void PostInit()
	{
        m_animator = this.gameObject.GetComponentInChildren<Animator>();
	}

	public void SetSpeed(float speed)
	{
        m_animator.speed = speed;
    }

	public void PlayAnim(string animName, bool useTrigger = false)
	{
        if(useTrigger)
        {
            m_animator.SetTrigger(animName);
        }
        else
        {
            m_animator.Play(animName);
        }
	}

	public void PlayAnimFrom(string animName, float normalizedTime = 0f)
	{
		m_animator.Play(animName, -1, normalizedTime:normalizedTime);
	}

	public void Walk(float speed, float rotateValue)
	{
		m_animator.SetFloat("MoveSpeed", speed);
        m_animator.SetFloat("RotateValue", rotateValue);
    }

	public void SetTurnProgress(float v)
	{
		m_animator.SetFloat("TurnProgress", v);
	}

	public void Freeze()
	{
		m_lastSpeed = m_animator.speed;
        m_animator.speed = 0;
	}

	public void UnFreeze()
	{
		m_animator.speed = m_lastSpeed;
	}

}
